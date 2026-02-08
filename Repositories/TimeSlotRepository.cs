using Hospital_Clinic_Appointment_System.App_Context;
using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Clinic_Appointment_System.Repositories
{
    public class TimeSlotRepository : GenericRepository<TimeSlot> , ITimeSlotRepository
    {
        private readonly DBContext context ; 
        public TimeSlotRepository(DBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TimeSlot>> GetTimeSlotsByDoctorIdAsync(int doctorId) 
        {
            return await context.TimeSlots
                .Where(t=>t.DoctorId == doctorId)
                .ToListAsync();
        
        }
        public async Task<IEnumerable<TimeSlot>> GetActiveTimeSlotsByDoctorIdAsync(int doctorId) 
        {
            return await context.TimeSlots
                .Where(t => t.DoctorId == doctorId)
                .Include(t => t.doctor)
                .Where(d => d.IsActive)
                .ToListAsync();
                
                    
                
        }

        public async  Task<IEnumerable<TimeSlot>> GetTimeSlotsByDoctorAndDayAsync(int doctorId, string dayOfWeek) 
        {
            return await context.TimeSlots
                .Where(t=>t.DoctorId==doctorId && t.DayOfWeek== dayOfWeek)
                .ToListAsync();

        
        
        }

        public async Task<bool> HasConflictingTimeSlotAsync(int doctorId, string dayOfWeek, TimeSpan startTime, TimeSpan endTime, int? excludeTimeSlotId = null) 
        {
            return await context.TimeSlots
                .Where(t => t.DoctorId == doctorId && t.DayOfWeek == dayOfWeek)
                .Where(t => excludeTimeSlotId == null || t.Id != excludeTimeSlotId)
                .AnyAsync(t =>
                    (startTime < t.EndTime && endTime > t.StartTime));




        }

        public async Task<int> DeleteTimeSlotsByDoctorAndDayAsync(int doctorId, string dayOfWeek) 
        {
            var timeSlotsToDelete = await context.TimeSlots
                .Where(t => t.DoctorId == doctorId && t.DayOfWeek == dayOfWeek)
                .ToListAsync();
            context.TimeSlots.RemoveRange(timeSlotsToDelete);
            await context.SaveChangesAsync();
            return timeSlotsToDelete.Count;
        }


    }
}
