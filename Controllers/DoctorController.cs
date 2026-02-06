using Hospital_Clinic_Appointment_System.App_Context;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Clinic_Appointment_System.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("Dashboard/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository doctorRepository;
      
        public DoctorController(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
           
        }

        // CRUD Operations and other methods From Generic Repository can be added here

        // Post : Dashboard/Doctor/Add
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

        // Get : Dashboard/Doctor/All
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

        // Get : Dashboard/Doctor/{DoctorId}
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
                ExperienceYears = doctor.ExperienceYears,
                isActive=doctor.isActive,
                Email = doctor.user?.Email ?? string.Empty,
                Phone_Number = doctor.user?.Phone_Number ?? string.Empty
            };
            return Ok(doctorDto);
        }



        [HttpGet("Active")] // GET: Dashboard/Doctor/active
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

       

       


        [HttpGet("Specialization")] // Get :Dashboard/Doctor/Specialization

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




        // Get : Dashboard/Doctor/Appointments/{doctorId}
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




        // Get : Dashboard/Doctor/TimeSlote/{doctorId}

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


        // Get : Dashboard/Doctor/Availability/{doctorId}

        [HttpGet("Availability/{doctorId:int}")]
        public async Task<ActionResult<bool>> IsDoctorAvailableAsync(int doctorId, DateTime appointmentDate)
        {
            var isAvailable = await doctorRepository.IsDoctorAvailableAsync(doctorId, appointmentDate);

            return Ok(isAvailable);
        }


        //Put : Dashboard/Doctor/{id}/Update
        [HttpPut("{id:int}/Update")]  
        public async Task<ActionResult> UpdateDoctorAsync([FromRoute] int id, [FromBody] updateDoctorDto updateDoctorDto)
        {
            // Validate ID
            if (id <= 0)
            {
                return BadRequest("Invalid doctor ID");
            }

            // Validate DTO
            if (updateDoctorDto == null)
            {
                return BadRequest("Doctor data is required");
            }

            try
            {
                var existingDoctor = await doctorRepository.GetByIdAsync(id);

                if (existingDoctor == null)
                {
                    return NotFound($"Doctor with ID {id} not found");
                }


               
                if (!string.IsNullOrWhiteSpace(updateDoctorDto.Name))
                    existingDoctor.Name = updateDoctorDto.Name;

                if (!string.IsNullOrWhiteSpace(updateDoctorDto.Specialization))
                    existingDoctor.Specialization = updateDoctorDto.Specialization;

                if (!string.IsNullOrWhiteSpace(updateDoctorDto.LicenseNumber))
                    existingDoctor.LicenseNumber = updateDoctorDto.LicenseNumber;

                if (updateDoctorDto.ExperienceYears > 0)
                    existingDoctor.ExperienceYears = updateDoctorDto.ExperienceYears;

                if (!string.IsNullOrWhiteSpace(updateDoctorDto.Bio))
                    existingDoctor.Bio = updateDoctorDto.Bio;

                if (!string.IsNullOrWhiteSpace(updateDoctorDto.profilePictureUrl))
                    existingDoctor.profilePictureUrl = updateDoctorDto.profilePictureUrl;

                existingDoctor.isActive = updateDoctorDto.isActive;

                
                doctorRepository.Update(existingDoctor);
                await doctorRepository.SaveChangesAsync();

                // Fetch updated doctor with includes
                var updatedDoctor = await doctorRepository.FirstOrDefaultWithIncludesAsync(
                    d => d.Id == id,
                    d => d.user
                );

                if (updatedDoctor == null) 
                {
                    return NotFound($"Doctor with ID {id} not found after update");
                }

                var dto = new DoctorDetailsDto
                {
                    Id = updatedDoctor.Id,
                    User_Id = updatedDoctor.User_Id,
                    Name = updatedDoctor.Name,
                    Email = updatedDoctor.user?.Email ?? string.Empty,
                    Phone_Number = updatedDoctor.user?.Phone_Number ?? string.Empty,
                    Specialization = updatedDoctor.Specialization,
                    LicenseNumber = updatedDoctor.LicenseNumber,
                    ExperienceYears = updatedDoctor.ExperienceYears,
                    Bio = updatedDoctor.Bio,
                    profilePictureUrl = updatedDoctor.profilePictureUrl,
                    IsActive = updatedDoctor.isActive
                };

                return Ok(new
                {
                    message = $"Doctor with ID {id} updated successfully",
                    data = dto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error updating doctor",
                    details = ex.Message
                });
            }
        }
        // Delete : Dashboard/Doctor/{id}/Delete 
        [HttpDelete("{id:int}/Delete")]  
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
