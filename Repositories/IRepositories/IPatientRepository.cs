using Hospital_Clinic_Appointment_System.Entities;

namespace Hospital_Clinic_Appointment_System.Repositories.IRepositories
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<IEnumerable<Patient>> GetAppointmentDateByPatientIdAsync(int patientId);

        Task<Patient?> GetMedicalHistoryByPatientIdAsync(int patientId);






    }
}
