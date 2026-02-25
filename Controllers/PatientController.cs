using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        // GET: api/Patient/All
        [HttpGet("All")]
        [Authorize(Policy = "DoctorOrAdmin")]
        public async Task<ActionResult<IEnumerable<PatientShortDto>>> GetAllPatientsAsync()
        {
            var patients = await patientRepository.GetAllWithIncludesAsync(p => p.user);

            var dtos = patients.Select(p => new PatientShortDto
            {
                Id = p.Id,
                Name = p.user?.Name ?? p.Name,
                Email = p.user?.Email,
                Phone_Number = p.user?.Phone_Number,
                MedicalHistory = p.MedicalHistory,
                EmergencyNumber = p.EmergencyNumber
            });

            return Ok(dtos);
        }

        // GET: api/Patient/{id}
        [HttpGet("{id}")]
        [Authorize] // Patient can view own data, Doctor/Admin can view any
        public async Task<ActionResult<PatientShortDto>> GetPatientByIdAsync(int id)
        {
            var patient = await patientRepository.FirstOrDefaultWithIncludesAsync(
                p => p.Id == id,
                p => p.user
            );

            if (patient == null)
                return NotFound();

            var dto = new PatientShortDto
            {
                Id = patient.Id,
                Name = patient.user?.Name ?? patient.Name,
                Email = patient.user?.Email,
                Phone_Number = patient.user?.Phone_Number,
                MedicalHistory = patient.MedicalHistory,
                EmergencyNumber = patient.EmergencyNumber
            };

            return Ok(dto);
        }

        // GET: api/Patient/{id}/MedicalHistory
        [HttpGet("{id}/MedicalHistory")]
        [Authorize(Policy = "DoctorOrAdmin")]
        public async Task<ActionResult<PatientShortDto>> GetMedicalHistoryAsync(int id)
        {
            var patient = await patientRepository.GetMedicalHistoryByPatientIdAsync(id);

            if (patient == null)
                return NotFound();

            var dto = new PatientShortDto
            {
                Id = patient.Id,
                Name = patient.user?.Name ?? patient.Name,
                Email = patient.user?.Email,
                Phone_Number = patient.user?.Phone_Number,
                MedicalHistory = patient.MedicalHistory,
                EmergencyNumber = patient.EmergencyNumber
            };

            return Ok(dto);
        }

        // GET: api/Patient/{id}/Appointments
        [HttpGet("{id}/Appointments")]
        [Authorize] // Patient can view own, Doctor/Admin can view any
        public async Task<ActionResult<IEnumerable<AppointmentShortDto>>> GetPatientAppointmentsAsync(int id)
        {
            var appointments = await patientRepository.GetAppointmentDatesByPatientIdAsync(id);

            if (appointments == null || !appointments.Any())
                return Ok(new List<AppointmentShortDto>());

            return Ok(appointments);
        }

        // POST: api/Patient/Add
        [HttpPost("Add")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<PatientShortDto>> AddPatientAsync([FromBody] CreatePatientDto createDto)
        {
            var patient = new Patient
            {
                User_Id = createDto.UserId,
                Name = createDto.Name,
                MedicalHistory = createDto.MedicalHistory,
                EmergencyNumber = createDto.EmergencyNumber,
                IsActive = createDto.IsActive
            };

            await patientRepository.AddAsync(patient);
            await patientRepository.SaveChangesAsync();

            var dto = new PatientShortDto
            {
                Id = patient.Id,
                Name = patient.Name,
                MedicalHistory = patient.MedicalHistory,
                EmergencyNumber = patient.EmergencyNumber
            };

            return CreatedAtAction(nameof(GetPatientByIdAsync), new { id = patient.Id }, dto);
        }

        // PUT: api/Patient/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = "PatientOrAdmin")]
        public async Task<IActionResult> UpdatePatientAsync(int id, [FromBody] PatientShortDto updateDto)
        {
            var patient = await patientRepository.GetByIdAsync(id);
            if (patient == null)
                return NotFound();

            // Update only provided fields
            if (!string.IsNullOrWhiteSpace(updateDto.Name))
                patient.Name = updateDto.Name;

            if (updateDto.MedicalHistory != null)
                patient.MedicalHistory = updateDto.MedicalHistory;

            if (!string.IsNullOrWhiteSpace(updateDto.EmergencyNumber))
                patient.EmergencyNumber = updateDto.EmergencyNumber;

            patientRepository.Update(patient);
            await patientRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Patient/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeletePatientAsync(int id)
        {
            var patient = await patientRepository.GetByIdAsync(id);
            if (patient == null)
                return NotFound();

            // Check for appointments
            var appointments = await patientRepository.GetAppointmentDatesByPatientIdAsync(id);
            if (appointments != null && appointments.Any())
            {
                return BadRequest(new
                {
                    Message = "Cannot delete patient with existing appointments",
                    AppointmentCount = appointments.Count()
                });
            }

            patientRepository.Delete(patient);
            await patientRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}