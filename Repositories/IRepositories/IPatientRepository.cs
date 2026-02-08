using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Models;

namespace Hospital_Clinic_Appointment_System.Repositories.IRepositories
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<IEnumerable<AppointmentShortDto>> GetAppointmentDatesByPatientIdAsync(int patientId);

        Task<Patient?> GetMedicalHistoryByPatientIdAsync(int patientId);






    }
}
