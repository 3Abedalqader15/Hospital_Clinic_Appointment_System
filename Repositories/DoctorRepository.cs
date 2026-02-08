using Hospital_Clinic_Appointment_System.App_Context;

using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Repositories;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
{
    private readonly DBContext context;

    public DoctorRepository(DBContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Doctor>> GetActiveDoctorsAsync()
    {
        return await context.Doctors
            .Include(d=>d.user)
            .Where(d => d.isActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Doctor>> GetDoctorsBySpecializationAsync(string specialization)
    {
        return await context.Doctors
            .Where(d => d.isActive && d.Specialization == specialization)
            .ToListAsync();
    }

    public async Task<Doctor?> GetDoctorWithAppointmentsAsync(int doctorId)
    {
        return await context.Doctors
            .Include(d => d.user)                       
            .Include(d => d.Appointments)
                .ThenInclude(a => a.patient)
                    .ThenInclude(p => p.user)         
            .FirstOrDefaultAsync(d => d.Id == doctorId);
    }

    public async Task<Doctor?> GetDoctorWithTimeSlotsAsync(int doctorId)
    {
        return await context.Doctors
            .Include(d=>d.user)
            .Include(d => d.TimeSlots)
            .FirstOrDefaultAsync(d => d.Id == doctorId);
    }

    public async Task<bool> IsDoctorAvailableAsync(int doctorId, DateTime appointmentDate)
    {
      
        var hasAppointment = await context.Appointments
            .AnyAsync(a =>
                a.DoctorId == doctorId &&
                a.AppointmentDate == appointmentDate &&
                a.Status == "Scheduled");

        return !hasAppointment;
    }
}
