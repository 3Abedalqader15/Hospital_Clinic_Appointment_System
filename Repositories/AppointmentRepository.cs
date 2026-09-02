using Hospital_Clinic_Appointment_System.App_Context;
using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Clinic_Appointment_System.Repositories;

public class AppointmentRepository(DBContext context) : GenericRepository<Appointment>(context), IAppointmentRepository
{
    public Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
    {
        return _context.Appointments
            .Where(a => a.DoctorId == doctorId)
            .ToListAsync()
            .ContinueWith(t => t.Result.AsEnumerable());
    }

    public Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
    {
        return _context.Appointments
            .Where(a => a.PatientId == patientId)
            .ToListAsync()
            .ContinueWith(t => t.Result.AsEnumerable());
    }

    public Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date)
    {
        return _context.Appointments
            .Where(a => a.AppointmentDate.Date == date.Date)
            .ToListAsync()
            .ContinueWith(t => t.Result.AsEnumerable());
    }

    public Task<IEnumerable<Appointment>> GetAppointmentsByStatusAsync(string status)
    {
        return _context.Appointments
            .Where(a => a.Status == status)
            .ToListAsync()
            .ContinueWith(t => t.Result.AsEnumerable());
    }


    public Task<IEnumerable<Appointment>> GetAllAppointmentsWithDetailsAsync()
    {
        return _context.Appointments
            .Include(a => a.doctor)
                .ThenInclude(d => d.user)
            .Include(a => a.patient)
                .ThenInclude(p => p.user)
            .ToListAsync()
            .ContinueWith(t => t.Result.AsEnumerable());
    }

    public async Task<bool> CancelAppointmentAsync(int appointmentId)
    {
        var appointment = await _context.Appointments.FindAsync(appointmentId);
        if (appointment == null)
        {
            return false;
        }
        appointment.Status = "Cancelled";
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CompleteAppointmentAsync(int appointmentId, string? notes = null)
    {
        var appointment = await _context.Appointments.FindAsync(appointmentId);
        if (appointment == null)
        {
            return false;
        }
        appointment.Status = "Completed";
        if (!string.IsNullOrEmpty(notes))
        {
            appointment.Notes = notes;
        }
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkAsNoShowAsync(int appointmentId)
    {
        var appointment = await _context.Appointments.FindAsync(appointmentId);
        if (appointment == null || appointment.Status == "Completed")
        {
            return false;
        }
        appointment.Status = "No-Show";
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RescheduleNewAppointmentAsync(int appointmentId, DateTime newAppointmentDate)
    {
        var appointment = await _context.Appointments.FindAsync(appointmentId);
        if (appointment == null || appointment.Status == "Completed")
        {
            return false;
        }
        appointment.AppointmentDate = newAppointmentDate;
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> MarkReminderAsSentAsync(int appointmentId)
    {
        var appointment = await _context.Appointments.FindAsync(appointmentId);
        if (appointment == null)
        {
            return false;
        }
        appointment.ReminderSent = true;
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
        return true;
    }
}
