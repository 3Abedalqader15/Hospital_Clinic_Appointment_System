using Hospital_Clinic_Appointment_System.App_Context;
using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Clinic_Appointment_System.Repositories
{
    public class PatientRepository : GenericReository<Patient> , IPatientRepository
    {
        private readonly DBContext context;

       
        public PatientRepository(DBContext context) : base(context) {
        
            this.context = context;
        }


        public async Task<IEnumerable<Patient>> GetAppointmentDateByPatientIdAsync(int patientId) 
        {
            return await context.Patients
                .Include(p => p.Id == patientId)
                .Include(p => p.Appointments)
                .ToListAsync();

        }

        public async Task<Patient?> GetMedicalHistoryByPatientIdAsync(int patientId)
        {
            return await context.Patients.FindAsync(patientId);



        }


































    }
}
