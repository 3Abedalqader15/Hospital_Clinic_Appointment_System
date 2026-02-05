using Hospital_Clinic_Appointment_System.App_Context;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Clinic_Appointment_System.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository doctorRepository;
      
        public DoctorController(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
           
        }

        // CRUD Operations and other methods From Generic Repository can be added here

        // Post : api/Doctor/Add
        [HttpPost("Add")]
        public async Task<ActionResult<CreateDoctorDto>> AddDoctorAsync([FromBody] CreateDoctorDto createDoctorDto)
        {
            var doctor = new Entities.Doctor
            {
                User_Id = createDoctorDto.User_Id,
                Name = createDoctorDto.Name,
                Specialization = createDoctorDto.Specialization,
                LicenseNumber = createDoctorDto.LicenseNumber,
                ExperienceYears = createDoctorDto.ExperienceYears,
                Bio = createDoctorDto.Bio,
                profilePictureUrl = createDoctorDto.profilePictureUrl,
                isActive = createDoctorDto.isActive
            };
            await doctorRepository.AddAsync(doctor);
            await doctorRepository.SaveChangesAsync();
            var dto = new DoctorDetailsDto
            {
                Id = doctor.Id,
                User_Id = doctor.User_Id,
                Name = doctor.Name,
                Email = doctor.user?.Email ?? string.Empty,
                Phone_Number = doctor.user?.Phone_Number ?? string.Empty,
                Specialization = doctor.Specialization,
                LicenseNumber = doctor.LicenseNumber,
                ExperienceYears = doctor.ExperienceYears,
                Bio = doctor.Bio,
                profilePictureUrl = doctor.profilePictureUrl,
                IsActive = doctor.isActive


            };

            return CreatedAtAction(nameof(GetDoctorByIdAsync), new { id = doctor.Id }, createDoctorDto); // Return 201 Created with the location of the new resource
        }

        // Get : api/Doctor/All
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<DoctorListDto>>> GetAllDoctorsAsync()
        {
            var doctors = await doctorRepository.GetAllWithIncludesAsync(d => d.user);

            var doctorDtos = doctors.Select(d => new DoctorListDto
            {
                Id = d.Id,
                Name = d.user?.Name ?? d.Name,
                Specialization = d.Specialization,
                isActive = d.isActive,
                ExperienceYears =d.ExperienceYears,
                Email = d.user?.Email ?? string.Empty,
                Phone_Number = d.user?.Phone_Number ?? string.Empty
            }).ToList();

            return Ok(doctorDtos);
        }

        // Get : api/Doctor/{DoctorId}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DoctorListDto>> GetDoctorByIdAsync([FromRoute]int id)
        {
            var doctor = await doctorRepository.FirstOrDefaultWithIncludesAsync(d=>d.Id == id, d=>d.user);
            if (doctor == null)
            {
                return NotFound();
            }
            var doctorDto = new DoctorListDto
            {
                Id = doctor.Id,
                Name = doctor.user?.Name?? doctor.Name,
                Specialization = doctor.Specialization,
                isActive=doctor.isActive,
                Email = doctor.user?.Email ?? string.Empty,
                Phone_Number = doctor.user?.Phone_Number ?? string.Empty
            };
            return Ok(doctorDto);
        }



        [HttpGet("Active")] // GET: api/Doctor/active
        public async Task<ActionResult<DoctorListDto>> GetActiveDoctorsAsync()
        {
            var doctors = await doctorRepository.GetActiveDoctorsAsync();

            var doctorDtos = doctors.Select(d => new DoctorListDto
            {
                Id = d.Id,
                Name = d.user.Name?? d.Name ,
                Specialization = d.Specialization,
                isActive=d.isActive,
                Email = d.user?.Email ?? string.Empty,
                Phone_Number = d.user?.Phone_Number ?? string.Empty
            });




            return Ok(doctorDtos);

        }

       

       


        [HttpGet("Specialization")] // Get :api/Doctor/Specialization

        public async Task<ActionResult<DoctorDetailsDto>> GetDoctorsBySpecializationAsync(string specialization) 
        {

            var Doctors = await doctorRepository.GetDoctorsBySpecializationAsync(specialization);


            var DoctorDto = Doctors.Select(d => new DoctorDetailsDto
            {
                Name = d.Name ,
                Specialization = d.Specialization

            });


            return Ok(DoctorDto);

        }



        
        // Get : api/Doctor/Appointments/{doctorId}
        [HttpGet("Appointments/{doctorId:int}")]
        public async Task<ActionResult<DoctorAppointmentShortDto?>> GetDoctorWithAppointmentsAsync(int doctorId)
        {
            var doctor = await doctorRepository.GetDoctorWithAppointmentsAsync(doctorId);

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




        // Get : api/Doctor/TimeSlote/{doctorId}

        [HttpGet("TimeSlot/{doctorId:int}")]
        public async Task<ActionResult<DoctorDetailsDto?>> GetDoctorWithTimeSlotsAsync([FromRoute] int doctorId)
        {
            var doctor = await doctorRepository.GetDoctorWithTimeSlotsAsync(doctorId);

            if (doctor == null)
            {
                return NotFound($"Doctor with ID {doctorId} not found");
            }

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


        // Get : api/Doctor/Availability/{doctorId}

        [HttpGet("Availability/{doctorId:int}")]
        public async Task<ActionResult<bool>> IsDoctorAvailableAsync(int doctorId, DateTime appointmentDate)
        {
            var isAvailable = await doctorRepository.IsDoctorAvailableAsync(doctorId, appointmentDate);

            return Ok(isAvailable);
        }
        //Put : api/Doctor/Update/{id}
        [HttpPut("Update/{id:int}")]
        public async Task<ActionResult> updateDoctorAsync([FromRoute] int doctorId, [FromBody] updateDoctorDto updateDoctorDto)
        {
            if (doctorId <= 0)
            {
                return BadRequest("Invalid patient ID");
            }
            var existingDoctor = await doctorRepository.FirstOrDefaultWithIncludesAsync(e => e.Id == doctorId, e => e.user);

            if (existingDoctor == null)
            {
                return NotFound($"Patient with ID {doctorId} not found");
            }
            existingDoctor.User_Id = updateDoctorDto.User_Id;
            existingDoctor.Name = updateDoctorDto.Name;
            existingDoctor.Specialization = updateDoctorDto.Specialization;
            existingDoctor.LicenseNumber = updateDoctorDto.LicenseNumber;
            existingDoctor.ExperienceYears = updateDoctorDto.ExperienceYears;
            existingDoctor.Bio = updateDoctorDto.Bio;
            existingDoctor.profilePictureUrl = updateDoctorDto.profilePictureUrl;
            existingDoctor.isActive = updateDoctorDto.isActive;
            doctorRepository.Update(existingDoctor);
            await doctorRepository.SaveChangesAsync();

            var Dto = new DoctorDetailsDto
            {
                Id = existingDoctor.Id,
                User_Id = existingDoctor.User_Id,
                Name = existingDoctor.Name,
                Email = existingDoctor.user?.Email ?? string.Empty,
                Phone_Number = existingDoctor.user?.Phone_Number ?? string.Empty,
                Specialization = existingDoctor.Specialization,
                LicenseNumber = existingDoctor.LicenseNumber,
                ExperienceYears = existingDoctor.ExperienceYears,
                Bio = existingDoctor.Bio,
                profilePictureUrl = existingDoctor.profilePictureUrl,
                IsActive = existingDoctor.isActive
            };



            return Ok(Dto);


        }
        // Delete : api/Doctor/Delete/{id}
        [HttpDelete("{id:int}")]  // Delete : api/Doctor/{id}
        public async Task<ActionResult> DeleteDoctorAsync([FromRoute] int id)
        {
            // Validate ID
            if (id <= 0)
            {
                return BadRequest("Invalid doctor ID");
            }

            var existingDoctor = await doctorRepository.GetByIdAsync(id);
            if (existingDoctor == null)
            {
                return NotFound($"Doctor with ID {id} not found");
            }

            try
            {
                // Check for related appointments before deletion
                var doctorWithAppointments = await doctorRepository.GetDoctorWithAppointmentsAsync(id);
                if (doctorWithAppointments?.Appointments?.Any() == true)
                {
                    return BadRequest(new 
                    { 
                        message = "Cannot delete doctor with existing appointments",
                        appointmentCount = doctorWithAppointments.Appointments.Count
                    });
                }

                // Check for time slots
                var doctorWithTimeSlots = await doctorRepository.GetDoctorWithTimeSlotsAsync(id);
                if (doctorWithTimeSlots?.TimeSlots?.Any() == true)
                {
                    return BadRequest(new 
                    { 
                        message = "Cannot delete doctor with existing time slots",
                        timeSlotCount = doctorWithTimeSlots.TimeSlots.Count
                    });
                }

                doctorRepository.Delete(existingDoctor);
                await doctorRepository.SaveChangesAsync();

                return Ok(new { message = $"Doctor with ID {id} deleted successfully" });
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, new 
                { 
                    message = "Database error while deleting doctor",
                    details = dbEx.InnerException?.Message 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new 
                { 
                    message = "Error deleting doctor",
                    details = ex.Message 
                });
            }
        }



























    }
}
