
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

        [HttpGet("Active")] // GET: api/Doctor/active
        public async Task<ActionResult<DoctorListDto>> GetActiveDoctorsAsync()
        {
            var doctors = await doctorRepository.GetActiveDoctorsAsync();

            var doctorDtos = doctors.Select(d => new DoctorListDto
            {
                Id = d.Id,
                Name = d.user.Name,
                Specialization = d.Specialization,
                Email = d.user.Email,
                Phone_Number = d.user.Phone_Number
            });




            return Ok(doctorDtos);

        }

        public async ActionResult<DoctorDetailsDto>




















    }
}
