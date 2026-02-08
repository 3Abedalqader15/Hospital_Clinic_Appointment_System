using Hospital_Clinic_Appointment_System.App_Context;
using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Clinic_Appointment_System.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly DBContext context;
        public AppointmentRepository(DBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            return await context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();

        }


        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId) 
        {
            return await context.Appointments
                .Where(a => a.PatientId == patientId)
                .ToListAsync();


        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date) 
        {
            return await context.Appointments
                .Where(a => a.AppointmentDate.Date == date.Date)
                .ToListAsync();
        }


        public async Task<IEnumerable<Appointment>> GetAppointmentsByStatusAsync(string status) 
        {
            return await context.Appointments
                .Where(a => a.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetTodaysAppointmentsAsync(int doctorId) 
        {
            var today = DateTime.Today;

            return await context.Appointments
                .Where(a => a.DoctorId == doctorId && a.AppointmentDate.Date == today)
                .ToListAsync();


        }

        public async Task<IEnumerable<Appointment>> GetThisWeekAppointmentsAsync() 
        {
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);
            return await context.Appointments
                .Where(a => a.AppointmentDate.Date >= startOfWeek && a.AppointmentDate.Date < endOfWeek)
                .ToListAsync();
        }

        public async Task<bool> CancelAppointmentAsync(int appointmentId) 
        {
            var appointment = await context.Appointments.FindAsync(appointmentId);
            if (appointment == null)
            {
                return false;
            }
            appointment.Status = "Cancelled";
            context.Appointments.Update(appointment);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CompleteAppointmentAsync(int appointmentId, string? notes = null) 
        {
            var appointment = await context.Appointments.FindAsync(appointmentId);
            if (appointment == null)
            {
                return false;
            }
            appointment.Status = "Completed";
            if (!string.IsNullOrEmpty(notes))
            {
                appointment.Notes = notes;
            }
            context.Appointments.Update(appointment);
            await context.SaveChangesAsync();
            return true;


        }


        public async Task<bool> MarkAsNoShowAsync(int appointmentId) 
        {
            var appointment = await context.Appointments.FindAsync(appointmentId);
            if (appointment == null || appointment.Status == "Completed" )
            {
                return false;
            }
            appointment.Status = "No-Show";
            context.Appointments.Update(appointment);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RescheduleNewAppointmentAsync(int appointmentId, DateTime newAppointmentDate) 
        {
            var appointment = context.Appointments.Find(appointmentId);
            if (appointment == null || appointment.Status == "Completed")
            {
                return false;
            }
            appointment.AppointmentDate = newAppointmentDate;
            context.Appointments.Update(appointment);
            await  context.SaveChangesAsync().ContinueWith(t => t.IsCompletedSuccessfully);
            return true;
        }


        public async Task<int> DeleteOldCancelledAppointmentsAsync(DateTime beforeDate) 
        {
            var oldCancelledAppointments = await context.Appointments
                .Where(a => a.Status == "Cancelled" && a.AppointmentDate < beforeDate)
                .ToListAsync();
            context.Appointments.RemoveRange(oldCancelledAppointments);
            await context.SaveChangesAsync();
            return oldCancelledAppointments.Count;

        }

        public async Task<bool> MarkReminderAsSentAsync(int appointmentId) 
        {
            var appointment = await context.Appointments.FindAsync(appointmentId);
            if (appointment == null)
            {
                return false;
            }
            appointment.ReminderSent = true;
            context.Appointments.Update(appointment);
            await context.SaveChangesAsync();
            return true;


        }

        
    }
}
