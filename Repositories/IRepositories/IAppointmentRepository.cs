using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Models;

namespace Hospital_Clinic_Appointment_System.Repositories.IRepositories
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);

        Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);

        Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date);
    
        Task<IEnumerable<Appointment>> GetAppointmentsByStatusAsync(string status);
    
        Task<IEnumerable<Appointment>> GetTodaysAppointmentsAsync (int doctorId);

        Task<IEnumerable<Appointment>> GetThisWeekAppointmentsAsync();

        Task<bool> CancelAppointmentAsync(int appointmentId);

        Task<bool> CompleteAppointmentAsync(int appointmentId, string? notes = null);

        Task<bool> MarkAsNoShowAsync(int appointmentId);

        Task<bool> RescheduleNewAppointmentAsync(int appointmentId, DateTime newAppointmentDate);

        Task<int> DeleteOldCancelledAppointmentsAsync(DateTime beforeDate);

        Task<bool> MarkReminderAsSentAsync(int appointmentId);








    }
}
