using Hospital_Clinic_Appointment_System.Entities;
using System.Threading.Tasks;

namespace Hospital_Clinic_Appointment_System.Repositories.IRepositories
{
    public interface ITimeSlotRepository : IGenericRepository<TimeSlot>
    {

        Task<IEnumerable<TimeSlot>> GetTimeSlotsByDoctorIdAsync(int doctorId);
        Task<IEnumerable<TimeSlot>> GetActiveTimeSlotsByDoctorIdAsync(int doctorId);

        Task<IEnumerable<TimeSlot>> GetTimeSlotsByDoctorAndDayAsync(int doctorId, string dayOfWeek);

        Task<bool> HasConflictingTimeSlotAsync(int doctorId, string dayOfWeek, TimeSpan startTime, TimeSpan endTime, int? excludeTimeSlotId = null);

        Task<int> DeleteTimeSlotsByDoctorAndDayAsync(int doctorId, string dayOfWeek);

    }
}
