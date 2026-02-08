using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository appointmentRepository;
        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }
        [HttpPost("add")] // Post : api/Appointment/add
        public async Task<ActionResult<AppointmentDto>> AddAppointmentAsync(CreateAppointmentDto createAppointment)
        {
            var appointment = new Appointment
            {
                
                DoctorId = createAppointment.DoctorId,
                PatientId = createAppointment.PatientId,
                AppointmentDate = createAppointment.AppointmentDate,
                Status = createAppointment.Status,
                Reason = createAppointment.Reason,
                Notes = createAppointment.Notes,
                ReminderSent = createAppointment.ReminderSent,

            };
            await appointmentRepository.AddAsync(appointment);
            await appointmentRepository.SaveChangesAsync();
            var Dto = new AppointmentDto
            {
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
                Reason = appointment.Reason,
                Notes = appointment.Notes,
                ReminderSent = appointment.ReminderSent,
            };
            return CreatedAtRoute("GetAppointmentById", new { id = Dto.DoctorId }, Dto); // Return 201 Created 
        }



        [HttpGet("Doctor/{doctorId}")] // Get : api/Appointment/Doctor/{doctorId}
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            var appointments = await appointmentRepository.GetAppointmentsByDoctorIdAsync(doctorId);
            var Dto = appointments.Select(a => new AppointmentDto
            {
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                AppointmentDate = a.AppointmentDate,
                Status = a.Status,
                Reason = a.Reason,
                Notes = a.Notes,
                ReminderSent = a.ReminderSent,
            }).ToList();
            return Ok(Dto);
        }
        [HttpGet("Patient/{patientId}")] // Get : api/Appointment/Patient/{patientId}
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            var appointments = await appointmentRepository.GetAppointmentsByPatientIdAsync(patientId);
            var Dto = appointments.Select(a => new AppointmentDto
            {
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                AppointmentDate = a.AppointmentDate,
                Status = a.Status,
                Reason = a.Reason,
                Notes = a.Notes,
                ReminderSent = a.ReminderSent,
            }).ToList();
            return Ok(Dto);
        }
            [HttpGet("Date/{date}")] // Get : api/Appointment/Date/{date}
            public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByDateAsync(DateTime date) 
            {
                var appointments = await appointmentRepository.GetAppointmentsByDateAsync(date);
                var Dto = appointments.Select(a => new AppointmentDto
                {
                    DoctorId = a.DoctorId,
                    PatientId = a.PatientId,
                    AppointmentDate = a.AppointmentDate,
                    Status = a.Status,
                    Reason = a.Reason,
                    Notes = a.Notes,
                    ReminderSent = a.ReminderSent,
                }).ToList();
                return Ok(Dto);

        }
    }
}
