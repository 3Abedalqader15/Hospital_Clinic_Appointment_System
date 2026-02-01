using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Models;

namespace Hospital_Clinic_Appointment_System.Repositories.IRepositories
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {

        Task<IEnumerable<Doctor>> GetActiveDoctorsAsync();
        Task<IEnumerable<Doctor>> GetDoctorsBySpecializationAsync(string specialization);

        Task<Doctor?> GetDoctorWithAppointmentsAsync(int doctorId);

        Task<Doctor?> GetDoctorWithTimeSlotsAsync(int doctorId);        


        Task<bool> IsDoctorAvailableAsync(int doctorId, DateTime appointmentDate);






    }
}
