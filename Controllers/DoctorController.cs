using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository doctorRepository;

        public DoctorController(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        // GET: api/Doctor/All
        [HttpGet("All")]
        [AllowAnonymous] // Anyone can view doctors
        public async Task<ActionResult<IEnumerable<DoctorListDto>>> GetAllDoctorsAsync()
        {
            var doctors = await doctorRepository.GetAllWithIncludesAsync(d => d.user);

            var dtos = doctors.Select(d => new DoctorListDto
            {
                Id = d.Id,
                Name = d.user?.Name ?? d.Name,
                Specialization = d.Specialization,
                ExperienceYears = d.ExperienceYears,
                isActive = d.isActive,
                Email = d.user?.Email ?? string.Empty,
                Phone_Number = d.user?.Phone_Number ?? string.Empty
            });

            return Ok(dtos);
        }

        // GET: api/Doctor/{id}
        [HttpGet("{id}")]
        [AllowAnonymous] // Anyone can view doctor details
        public async Task<ActionResult<DoctorDetailsDto>> GetDoctorByIdAsync(int id)
        {
            var doctor = await doctorRepository.FirstOrDefaultWithIncludesAsync(
                d => d.Id == id,
                d => d.user
            );

            if (doctor == null)
                return NotFound();

            var dto = new DoctorDetailsDto
            {
                Id = doctor.Id,
                User_Id = doctor.User_Id,
                Name = doctor.user?.Name ?? doctor.Name,
                Email = doctor.user?.Email ?? string.Empty,
                Phone_Number = doctor.user?.Phone_Number ?? string.Empty,
                Specialization = doctor.Specialization,
                LicenseNumber = doctor.LicenseNumber,
                ExperienceYears = doctor.ExperienceYears,
                Bio = doctor.Bio,
                profilePictureUrl = doctor.profilePictureUrl,
                IsActive = doctor.isActive
            };

            return Ok(dto);
        }

        // GET: api/Doctor/Active
        [HttpGet("Active")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DoctorListDto>>> GetActiveDoctorsAsync()
        {
            var doctors = await doctorRepository.GetActiveDoctorsAsync();

            var dtos = doctors.Select(d => new DoctorListDto
            {
                Id = d.Id,
                Name = d.user?.Name ?? d.Name,
                Specialization = d.Specialization,
                ExperienceYears = d.ExperienceYears,
                isActive = d.isActive,
                Email = d.user?.Email ?? string.Empty,
                Phone_Number = d.user?.Phone_Number ?? string.Empty
            });

            return Ok(dtos);
        }

        // GET: api/Doctor/Specialization/{specialization}
        [HttpGet("Specialization/{specialization}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DoctorListDto>>> GetDoctorsBySpecializationAsync(string specialization)
        {
            var doctors = await doctorRepository.GetDoctorsBySpecializationAsync(specialization);

            var dtos = doctors.Select(d => new DoctorListDto
            {
                Id = d.Id,
                Name = d.user?.Name ?? d.Name,
                Specialization = d.Specialization,
                ExperienceYears = d.ExperienceYears,
                isActive = d.isActive,
                Email = d.user?.Email ?? string.Empty,
                Phone_Number = d.user?.Phone_Number ?? string.Empty
            });

            return Ok(dtos);
        }

        // GET: api/Doctor/{id}/Appointments
        [HttpGet("{id}/Appointments")]
        [Authorize(Policy = "DoctorOrAdmin")]
        public async Task<ActionResult<DoctorAppointmentShortDto>> GetDoctorWithAppointmentsAsync(int id)
        {
            var doctor = await doctorRepository.GetDoctorWithAppointmentsAsync(id);

            if (doctor == null)
                return NotFound();

            var dto = new DoctorAppointmentShortDto
            {
                Id = doctor.Id,
                Name = doctor.user?.Name ?? doctor.Name,
                Email = doctor.user?.Email,
                Phone_Number = doctor.user?.Phone_Number,
                Specialization = doctor.Specialization,
                isActive = doctor.isActive,
                Appointments = doctor.Appointments?.Select(a => new AppointmentShortDto
                {
                    Id = a.Id,
                    AppointmentDate = a.AppointmentDate,
                    Status = a.Status,
                    Patient = a.patient == null ? null : new PatientShortDto
                    {
                        Id = a.patient.Id,
                        Name = a.patient.user?.Name ?? a.patient.Name,
                        Email = a.patient.user?.Email,
                        Phone_Number = a.patient.user?.Phone_Number
                    }
                }).ToList() ?? new List<AppointmentShortDto>()
            };

            return Ok(dto);
        }

        // GET: api/Doctor/{id}/TimeSlots
        [HttpGet("{id}/TimeSlots")]
        [AllowAnonymous]
        public async Task<ActionResult<DoctorDetailsDto>> GetDoctorWithTimeSlotsAsync(int id)
        {
            var doctor = await doctorRepository.GetDoctorWithTimeSlotsAsync(id);

            if (doctor == null)
                return NotFound();

            var dto = new DoctorDetailsDto
            {
                Id = doctor.Id,
                User_Id = doctor.User_Id,
                Name = doctor.user?.Name ?? doctor.Name,
                Email = doctor.user?.Email ?? string.Empty,
                Phone_Number = doctor.user?.Phone_Number ?? string.Empty,
                Specialization = doctor.Specialization,
                LicenseNumber = doctor.LicenseNumber,
                ExperienceYears = doctor.ExperienceYears,
                Bio = doctor.Bio,
                profilePictureUrl = doctor.profilePictureUrl,
                IsActive = doctor.isActive,
                TimeSlots = doctor.TimeSlots?.Select(t => new TimeSloteShortDto
                {
                    DayOfWeek = t.DayOfWeek,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    SlotDuration = t.SlotDuration
                }).ToList() ?? new List<TimeSloteShortDto>()
            };

            return Ok(dto);
        }

        // GET: api/Doctor/{id}/Available
        [HttpGet("{id}/Available")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> CheckDoctorAvailabilityAsync(int id, [FromQuery] DateTime appointmentDate)
        {
            var isAvailable = await doctorRepository.IsDoctorAvailableAsync(id, appointmentDate);
            return Ok(new { IsAvailable = isAvailable });
        }

        // POST: api/Doctor/Add
        [HttpPost("Add")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<DoctorDetailsDto>> AddDoctorAsync([FromBody] CreateDoctorDto createDto)
        {
            var doctor = new Doctor
            {
                User_Id = createDto.User_Id,
                Name = createDto.Name,
                Specialization = createDto.Specialization,
                LicenseNumber = createDto.LicenseNumber,
                ExperienceYears = createDto.ExperienceYears,
                Bio = createDto.Bio,
                profilePictureUrl = createDto.profilePictureUrl,
                isActive = createDto.isActive
            };

            await doctorRepository.AddAsync(doctor);
            await doctorRepository.SaveChangesAsync();

            var dto = new DoctorDetailsDto
            {
                Id = doctor.Id,
                User_Id = doctor.User_Id,
                Name = doctor.Name,
                Specialization = doctor.Specialization,
                LicenseNumber = doctor.LicenseNumber,
                ExperienceYears = doctor.ExperienceYears,
                Bio = doctor.Bio,
                profilePictureUrl = doctor.profilePictureUrl,
                IsActive = doctor.isActive
            };

            return CreatedAtAction(nameof(GetDoctorByIdAsync), new { id = doctor.Id }, dto);
        }

        // PUT: api/Doctor/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateDoctorAsync(int id, [FromBody] updateDoctorDto updateDto)
        {
            var doctor = await doctorRepository.GetByIdAsync(id);
            if (doctor == null)
                return NotFound();

            // Update only provided fields
            if (!string.IsNullOrWhiteSpace(updateDto.Name))
                doctor.Name = updateDto.Name;

            if (!string.IsNullOrWhiteSpace(updateDto.Specialization))
                doctor.Specialization = updateDto.Specialization;

            if (!string.IsNullOrWhiteSpace(updateDto.LicenseNumber))
                doctor.LicenseNumber = updateDto.LicenseNumber;

            if (updateDto.ExperienceYears > 0)
                doctor.ExperienceYears = updateDto.ExperienceYears;

            if (!string.IsNullOrWhiteSpace(updateDto.Bio))
                doctor.Bio = updateDto.Bio;

            if (!string.IsNullOrWhiteSpace(updateDto.profilePictureUrl))
                doctor.profilePictureUrl = updateDto.profilePictureUrl;

            doctor.isActive = updateDto.isActive;

            doctorRepository.Update(doctor);
            await doctorRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Doctor/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteDoctorAsync(int id)
        {
            var doctor = await doctorRepository.GetByIdAsync(id);
            if (doctor == null)
                return NotFound();

            // Check for appointments
            var doctorWithAppointments = await doctorRepository.GetDoctorWithAppointmentsAsync(id);
            if (doctorWithAppointments?.Appointments?.Any() == true)
            {
                return BadRequest(new
                {
                    Message = "Cannot delete doctor with existing appointments",
                    AppointmentCount = doctorWithAppointments.Appointments.Count
                });
            }

            doctorRepository.Delete(doctor);
            await doctorRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}