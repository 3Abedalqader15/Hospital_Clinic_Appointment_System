using Hospital_Clinic_Appointment_System.App_Context;
using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Clinic_Appointment_System.Repositories
{
    public class PatientRepository : GenericRepository<Patient> , IPatientRepository
    {
        private readonly DBContext context;

       
        public PatientRepository(DBContext context) : base(context) {
        
            this.context = context;
        }


        public async Task<IEnumerable<AppointmentShortDto>> GetAppointmentDatesByPatientIdAsync(int patientId)
        {
            return await context.Appointments
                .AsNoTracking()
                .Where(a => a.PatientId == patientId)
                .Select(a => new AppointmentShortDto
                {
                    Id = a.Id,
                    AppointmentDate = a.AppointmentDate,
                    Status = a.Status
                })
                .ToListAsync();
        }

        public async Task<Patient?> GetMedicalHistoryByPatientIdAsync(int patientId)
        {
            return await context.Patients
                .Include(p => p.user)
                .FirstOrDefaultAsync(p => p.Id == patientId);
        }


































    }
}
