using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Repositories;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All endpoints require authentication by default
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository appointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }

        // POST: api/Appointment/Create
        [HttpPost("Create")]
        [Authorize(Policy = "PatientOrAdmin")]
        public async Task<ActionResult<AppointmentDto>> CreateAppointmentAsync([FromBody] CreateAppointmentDto createDto)
        {
            var appointment = new Appointment
            {
                DoctorId = createDto.DoctorId,
                PatientId = createDto.PatientId,
                AppointmentDate = createDto.AppointmentDate,
                Status = "Scheduled", // Always start as Scheduled
                Reason = createDto.Reason,
                Notes = createDto.Notes
            };

            await appointmentRepository.AddAsync(appointment);
            await appointmentRepository.SaveChangesAsync();

            var dto = new AppointmentDto
            {
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
                Reason = appointment.Reason,
                Notes = appointment.Notes
            };

            return CreatedAtRoute("GetAppointmentById", new { id = appointment.Id }, dto);
        }

        // GET: api/Appointment/{id}
        [HttpGet("{id}", Name = "GetAppointmentById")]
        [Authorize] // Any authenticated user
        public async Task<ActionResult<AppointmentDto>> GetAppointmentByIdAsync(int id)
        {
            var appointment = await appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
                return NotFound();

            var dto = new AppointmentDto
            {
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
                Reason = appointment.Reason,
                Notes = appointment.Notes,
                ReminderSent = appointment.ReminderSent
            };

            return Ok(dto);
        }

        // GET: api/Appointment/Doctor/{doctorId}
        [HttpGet("Doctor/{doctorId}")]
        [Authorize(Policy = "DoctorOrAdmin")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByDoctorAsync(int doctorId)
        {
            var appointments = await appointmentRepository.GetAppointmentsByDoctorIdAsync(doctorId);

            var dtos = appointments.Select(a => new AppointmentDto
            {
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                AppointmentDate = a.AppointmentDate,
                Status = a.Status,
                Reason = a.Reason,
                Notes = a.Notes,
                ReminderSent = a.ReminderSent
            });

            return Ok(dtos);
        }

        // GET: api/Appointment/Patient/{patientId}
        [HttpGet("Patient/{patientId}")]
        [Authorize(Policy = "PatientOrAdmin")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByPatientAsync(int patientId)
        {
            var appointments = await appointmentRepository.GetAppointmentsByPatientIdAsync(patientId);

            var dtos = appointments.Select(a => new AppointmentDto
            {
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                AppointmentDate = a.AppointmentDate,
                Status = a.Status,
                Reason = a.Reason,
                Notes = a.Notes,
                ReminderSent = a.ReminderSent
            });

            return Ok(dtos);
        }

        // GET: api/Appointment/Date/{date}
        [HttpGet("Date/{date}")]
        [Authorize(Policy = "DoctorOrAdmin")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByDateAsync(DateTime date)
        {
            var appointments = await appointmentRepository.GetAppointmentsByDateAsync(date);

            var dtos = appointments.Select(a => new AppointmentDto
            {
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                AppointmentDate = a.AppointmentDate,
                Status = a.Status,
                Reason = a.Reason,
                Notes = a.Notes,
                ReminderSent = a.ReminderSent
            });

            return Ok(dtos);
        }

        // GET: api/Appointment/Status/{status}
        [HttpGet("Status/{status}")]
        [Authorize(Policy = "DoctorOrAdmin")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByStatusAsync(string status)
        {
            var appointments = await appointmentRepository.GetAppointmentsByStatusAsync(status);

            var dtos = appointments.Select(a => new AppointmentDto
            {
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                AppointmentDate = a.AppointmentDate,
                Status = a.Status,
                Reason = a.Reason,
                Notes = a.Notes,
                ReminderSent = a.ReminderSent
            });

            return Ok(dtos);
        }

        // PUT: api/Appointment/{id}
        [HttpPut("{id}")] // Put : api/Appointment/{id}
        [Authorize(Policy = "DoctorOrAdmin")]
        public async Task<IActionResult> UpdateAppointmentAsync(int id, UpdateAppointmentDto updateAppointment)
        {
            var appointment = await appointmentRepository.GetByIdAsync(id);
            if (appointment == null) return NotFound();

            appointment.DoctorId = updateAppointment.DoctorId;
            appointment.PatientId = updateAppointment.PatientId;
            appointment.AppointmentDate = updateAppointment.AppointmentDate;
            appointment.Status = updateAppointment.Status;
            appointment.Reason = updateAppointment.Reason;
            appointment.Notes = updateAppointment.Notes;

            appointmentRepository.Update(appointment);
            await appointmentRepository.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Appointment/{id}/Cancel
        [HttpPost("{id}/Cancel")]
        [Authorize] // Patient or Doctor can cancel
        public async Task<ActionResult> CancelAppointmentAsync(int id)
        {
            var result = await appointmentRepository.CancelAppointmentAsync(id);
            if (!result)
                return NotFound(new { Message = "Appointment not found" });

            return Ok(new { Message = "Appointment cancelled successfully" });
        }

        // POST: api/Appointment/{id}/Complete
        [HttpPost("{id}/Complete")]
        [Authorize(Policy = "DoctorOrAdmin")]
        public async Task<ActionResult> CompleteAppointmentAsync(int id, [FromBody] string? notes = null)
        {
            var result = await appointmentRepository.CompleteAppointmentAsync(id, notes);
            if (!result)
                return NotFound(new { Message = "Appointment not found" });

            return Ok(new { Message = "Appointment completed successfully" });
        }

        // POST: api/Appointment/{id}/NoShow
        [HttpPost("{id}/NoShow")]
        [Authorize(Policy = "DoctorOrAdmin")]
        public async Task<ActionResult> MarkAsNoShowAsync(int id)
        {
            var result = await appointmentRepository.MarkAsNoShowAsync(id);
            if (!result)
                return NotFound(new { Message = "Appointment not found" });

            return Ok(new { Message = "Appointment marked as No-Show" });
        }

        // POST: api/Appointment/{id}/Reschedule
        [HttpPost("{id}/Reschedule")]
        [Authorize] // Patient or Doctor can reschedule
        public async Task<ActionResult> RescheduleAppointmentAsync(int id, [FromBody] RescheduleAppointmentDto dto)
        {
            if (dto == null || dto.NewAppointmentDate == default)
                return BadRequest(new { Message = "New appointment date is required" });

            var success = await appointmentRepository.RescheduleNewAppointmentAsync(id, dto.NewAppointmentDate);
            if (!success)
                return NotFound(new { Message = "Appointment not found" });

            return Ok(new
            {
                Message = "Appointment rescheduled successfully",
                NewDate = dto.NewAppointmentDate
            });
        }

        // POST: api/Appointment/{id}/SendReminder
        [HttpPost("{id}/SendReminder")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> SendReminderAsync(int id)
        {
            var result = await appointmentRepository.MarkReminderAsSentAsync(id);
            if (!result)
                return NotFound(new { Message = "Appointment not found" });

            return Ok(new { Message = "Reminder sent successfully" });
        }

        // DELETE: api/Appointment/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteAppointmentAsync(int id)
        {
            var appointment = await appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
                return NotFound();

            appointmentRepository.Delete(appointment);
            await appointmentRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}