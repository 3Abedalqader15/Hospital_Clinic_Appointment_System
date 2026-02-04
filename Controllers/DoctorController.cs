
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

        // Post : api/Doctor/Add
        [HttpPost("Add")]
        public async Task<ActionResult<CreateDoctorDto>> AddDoctorAsync([FromBody]CreateDoctorDto createDoctorDto)
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
            return CreatedAtAction(nameof(GetDoctorByIdAsync), new { id = doctor.Id }, createDoctorDto); // Return 201 Created with the location of the new resource
        }

        //Put : api/Doctor/Update/{id}
        [HttpPut("Update/{id:int}")]
        public async Task<ActionResult> updateDoctorAsync([FromRoute]int doctorId, [FromBody]updateDoctorDto updateDoctorDto)
        { 
        
            var existingDoctor = await doctorRepository.FirstOrDefaultWithIncludesAsync(e => e.Id == doctorId, e => e.user); 

            if (existingDoctor == null)
            {
                return NotFound();
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

            

            return Ok(existingDoctor); 


        }
        // Delete : api/Doctor/Delete/{id}
        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult> DeleteDoctorAsync([FromRoute]int id)
        {
            var existingDoctor = await doctorRepository.GetByIdAsync(id);
            if (existingDoctor == null)
                return NotFound();
            doctorRepository.Delete(existingDoctor);
            await doctorRepository.SaveChangesAsync();
            return NoContent(); // 204 No Content is Ok for delete operations
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

        [HttpGet("TimeSlote/{doctorId}")]

        public async Task<ActionResult<DoctorDetailsDto?>> GetDoctorWithTimeSlotsAsync(int doctorId) 
        {

            var doctor = await doctorRepository.GetDoctorWithTimeSlotsAsync(doctorId);

            if (doctor == null)
            {
                return NotFound();
            }

            var dto = new DoctorDetailsDto
            {
                Id = doctor.Id,
                User_Id = doctor.User_Id, // ensure DTO gets user id
                Name = doctor.user?.Name ?? doctor.Name,
                Email = doctor.user?.Email ?? string.Empty,
                Phone_Number = doctor.user?.Phone_Number ?? string.Empty,
               
                Specialization = doctor.Specialization,
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



























    }
}
