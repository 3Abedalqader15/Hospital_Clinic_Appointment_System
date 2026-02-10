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
                Notes = createAppointment.Notes
               
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
                Notes = appointment.Notes
                
            };

            // return with the actual appointment id
            return CreatedAtRoute("GetAppointmentById", new { id = appointment.Id }, Dto);
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

        [HttpGet("status/{status}")] // Get : api/Appointment/status/{status}
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByStatusAsync(string status)
        {
            var appointments = await appointmentRepository.GetAppointmentsByStatusAsync(status);
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

        [HttpGet("today/{doctorId}")] // Get : api/Appointment/today/{doctorId}
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetTodaysAppointmentsAsync(int doctorId)
        {
            var appointments = await appointmentRepository.GetTodaysAppointmentsAsync(doctorId);
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

        [HttpGet("week")] // Get : api/Appointment/week
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetThisWeekAppointmentsAsync()
        {
            var appointments = await appointmentRepository.GetThisWeekAppointmentsAsync();
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

        [HttpDelete("{id}")] // Delete : api/Appointment/{id}
        public async Task<IActionResult> DeleteAppointmentAsync(int id)
        {
            var appointment = await appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            appointmentRepository.Delete(appointment);
            await appointmentRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")] // Put : api/Appointment/{id}
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

        [HttpGet("{id}", Name = "GetAppointmentById")] // Get : api/Appointment/{id}
        public async Task<ActionResult<AppointmentDto>> GetAppointmentByIdAsync(int id)
        {
            var appointment = await appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

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

            return Ok(Dto);
        }

        [HttpPost("{appointmentId}/send-reminder")] // Post : api/Appointment/{appointmentId}/send-reminder
        public async Task<ActionResult> MarkReminderAsSentAsync(int appointmentId)
        {
            var result = await appointmentRepository.MarkReminderAsSentAsync(appointmentId);
            if (!result)
            {
                return NotFound(new { Message = "Appointment not found or couldn't mark reminder." });
            }
            return Ok(new { Message = "Reminder sent successfully." });
        }

        [HttpPost("{appointmentId}/cancel")] // Post : api/Appointment/{appointmentId}/cancel
        public async Task<ActionResult> CancelAppointmentAsync(int appointmentId)
        {
            var result = await appointmentRepository.CancelAppointmentAsync(appointmentId);
            if (!result)
            { 
                return NotFound(new { Message = "Appointment not found or couldn't cancel." }); 
            }

            return Ok(new { Message = "Appointment cancelled successfully." });
        }

        [HttpPost("{appointmentId}/complete")] // Post : api/Appointment/{appointmentId}/complete
        public async Task<ActionResult> CompleteAppointmentAsync(int appointmentId, [FromBody] string? notes = null)
        {
            var result = await appointmentRepository.CompleteAppointmentAsync(appointmentId, notes);
            if (!result)
            {
                return NotFound(new { Message = "Appointment not found or couldn't complete." });
            }
            return Ok(new { Message = "Appointment marked as completed successfully." });
        }

        [HttpPost("{appointmentId}/no-show")] // Post : api/Appointment/{appointmentId}/no-show
        public async Task<ActionResult> MarkAsNoShowAsync(int appointmentId)
        {
            var result = await appointmentRepository.MarkAsNoShowAsync(appointmentId);
            if (!result)
            {
                return NotFound(new { Message = "Appointment not found or couldn't mark as no-show." });
            }

            return Ok(new { Message = "Appointment marked as No-Show successfully." });
        }

        [HttpPost("{appointmentId}/reschedule")] // Post : api/Appointment/{appointmentId}/reschedule
        public async Task<ActionResult> RescheduleAppointmentAsync(int appointmentId, [FromBody] RescheduleAppointmentDto request)
        {
            if (request == null || request.NewAppointmentDate == default)
            {
                return BadRequest(new { Message = "NewAppointmentDate is required." });
            }



            var success = await appointmentRepository.RescheduleNewAppointmentAsync(appointmentId, request.NewAppointmentDate); 


            if (!success)
            {
                return NotFound(new { Message = "Appointment not found or couldn't reschedule." });
            }
            


            return Ok(new { Message = "Appointment rescheduled successfully.", NewDate = request.NewAppointmentDate }); 
        }


        [HttpDelete("cleanup/cancelled")] // Delete : api/Appointment/cleanup/cancelled?beforeDate=2026-01-01
        public async Task<ActionResult<int>> DeleteOldCancelledAppointmentsAsync([FromQuery] DateTime beforeDate)
        {
            if (beforeDate == default)
            {
                return BadRequest(new { Message = "beforeDate query parameter is required." });
            }

            var deletedCount = await appointmentRepository.DeleteOldCancelledAppointmentsAsync(beforeDate);
            return Ok(new { Deleted = deletedCount });
        }
    }
}
