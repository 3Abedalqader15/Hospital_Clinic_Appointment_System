using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class PatientcController : ControllerBase
    {
        private readonly PatientRepository patientRepository;
        public PatientcController(PatientRepository patientRepository) 
        {
            this.patientRepository = patientRepository;
        }


        [HttpGet("All")] // Get : api/Patient/All
        public async Task<ActionResult<IEnumerable<PatientShortDto>>> GetAllPatientsAsync()
        {
            var patients = await patientRepository.GetAllWithIncludesAsync(p => p.user, p => p.Appointments);

            var dto = patients.Select(p => new PatientShortDto
            {
                Id = p.Id,
                Name = p.user?.Name ?? p.Name,
                Email = p.user?.Email ?? p.Name,
                Phone_Number = p.user?.Phone_Number,
                MedicalHistory = p.MedicalHistory,
                EmergencyNumber = p.EmergencyNumber




            });

            return Ok(dto);
        }

        [HttpGet("{id}")] // Get : api/Patient/{id}
        public async Task<ActionResult<PatientShortDto>> GetPatientByIdAsync([FromRoute] int PateintId)
        {
            var patient = await patientRepository.FirstOrDefaultWithIncludesAsync(p => p.Id == PateintId, p => p.user, p => p.Appointments);
            if (patient == null)
            {
                return NotFound();
            }
            var dto = new PatientShortDto
            {
                Id = patient.Id,
                Name = patient.user?.Name ?? patient.Name,
                Email = patient.user?.Email ?? patient.Name,
                Phone_Number = patient.user?.Phone_Number,
                MedicalHistory = patient.MedicalHistory,
                EmergencyNumber = patient.EmergencyNumber
            };
            return Ok(dto);

        }

        [HttpGet("{id}")] // Get : api/Patient/{id}
        public async Task<ActionResult<string>> GetMedicalHistoryByPatientIdAsync([FromRoute] int PateintId)
        {
            var patient = await patientRepository.GetMedicalHistoryByPatientIdAsync(PateintId);
            if (patient == null)
            {
                return NotFound();
            }
            var dto = new PatientShortDto
            {

                Name = patient.user?.Name ?? patient.Name,
                Email = patient.user?.Email ?? patient.Name,

                MedicalHistory = patient.MedicalHistory

            };
            return Ok(dto);
        }

        [HttpGet("{Id}")] // Get : api/Patient/{Id}
        public async Task<ActionResult<IEnumerable<PatientShortDto>>> GetAppointmentDateByPatientIdAsync([FromRoute] int PateintId)
        {
            var patients = await patientRepository.GetAppointmentDateByPatientIdAsync(PateintId);
            if (patients == null || !patients.Any())
            {
                return NotFound();
            }

            var patientsList = patients.Where(p => p != null).ToList();

            var dto = patientsList.Select(p => new PatientShortDto
            {
                Id = p.Id,
                Name = p.user?.Name ?? p.Name,
                Email = p.user?.Email ?? p.Name,
                Phone_Number = p.user?.Phone_Number,
                Appointments = (p.Appointments
                    .Where(a => a != null) // guard against null entries
                    .Select(a => new AppointmentShortDto
                    {
                        Id = a.Id,
                        AppointmentDate = a.AppointmentDate,
                        Status = a.Status ?? string.Empty
                    })
                    .ToList()) ?? new List<AppointmentShortDto>()
            }).ToList();

            return Ok(dto);
        }

        [HttpDelete("{id}")] // Delete : api/Patient/{id}
                  
        public async Task<IActionResult> DeletePatientAsync([FromRoute] int PateintId)
        {
            var patient = await patientRepository.GetByIdAsync(PateintId);
            if (patient == null)
            {
                return NotFound();
            }
            patientRepository.Delete(patient);
            await patientRepository.SaveChangesAsync();

            return NoContent();

            }

        [HttpPut("{id}")] // Put : api/Patient/{id}]
        public async Task<IActionResult> UpdatePatientAsync([FromRoute] int PateintId, [FromBody] PatientShortDto patientDto)
        {
            var patient = await patientRepository.GetByIdAsync(PateintId);
            if (patient == null)
            {
                return NotFound();
            }
            patient.Name = patientDto.Name ?? patient.Name;
            patient.MedicalHistory = patientDto.MedicalHistory ?? patient.MedicalHistory;
            patient.EmergencyNumber = patientDto.EmergencyNumber ?? patient.EmergencyNumber;
            patientRepository.Update(patient);
            await patientRepository.SaveChangesAsync();
            return NoContent();
        }







































        }
    }

