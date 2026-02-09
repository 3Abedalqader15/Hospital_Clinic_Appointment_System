using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotController : ControllerBase
    {
        private readonly ITimeSlotRepository timeSlotRepository;
        public TimeSlotController(ITimeSlotRepository timeSlotRepository)
        {
            this.timeSlotRepository = timeSlotRepository;
        }

        [HttpPost("Add")] // Post : api/TimeSlot/Add
        public async Task<ActionResult<TimeSloteShortDto>> AddTimeSlotAsync(CreateTimeSlot addtimeSlot)
        {
            var timeslot = new TimeSlot
            {

                DoctorId = addtimeSlot.DoctorId,
                DayOfWeek = addtimeSlot.DayOfWeek,
                StartTime = addtimeSlot.StartTime,
                EndTime = addtimeSlot.EndTime,
                SlotDuration = addtimeSlot.SlotDuration

            };

            await timeSlotRepository.AddAsync(timeslot);
            await timeSlotRepository.SaveChangesAsync();
            var dto = new TimeSloteShortDto
            {
                DayOfWeek = timeslot.DayOfWeek,
                StartTime = timeslot.StartTime,
                EndTime = timeslot.EndTime,
                SlotDuration = timeslot.SlotDuration



            };

            return CreatedAtRoute("GetTimeByDoctorId", new { doctorId = timeslot.DoctorId }, dto);

        }

        [HttpGet("all")] // Get : api/TimeSlot/all
        public async Task<ActionResult<IEnumerable<TimeSloteShortDto>>> GetallAsync()
        {
            var timeslots = await timeSlotRepository.GetAllWithIncludesAsync();

            var Dto = timeslots.Select(
                t => new TimeSloteShortDto
                {
                    DoctorId = t.DoctorId,
                    DayOfWeek = t.DayOfWeek,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    SlotDuration = t.SlotDuration,
                    IsActive = t.IsActive


                }).ToList();


            return Ok(Dto);

        }

        [HttpGet("{doctorId:int}/Doctor" , Name = "GetTimeByDoctorId")] // // Get : api/TimeSlot/all

        public async Task<ActionResult<TimeSloteShortDto>> GetTimeSlotByDoctorId([FromRoute] int doctorId)
        {
            var timeslot = await timeSlotRepository.GetActiveTimeSlotsByDoctorIdAsync(doctorId);

            var dto = timeslot.Select(t => new TimeSloteShortDto
            {
                DayOfWeek = t.DayOfWeek,
                StartTime = t.StartTime,
                EndTime = t.EndTime,
                SlotDuration = t.SlotDuration,
                IsActive = t.IsActive

            });
            return Ok(timeslot);

        }
        [HttpGet("{patientId:int}/Doctor")] // Get : api/TimeSlot/patient/{patientId}

        public async Task<ActionResult<TimeSloteShortDto>> GetTimeSlotByPatientId([FromRoute] int patientId)
        {
            var timeslot = await timeSlotRepository.GetTimeSlotsByDoctorAndDayAsync(patientId, DateTime.Today.DayOfWeek.ToString());
            var dto = timeslot.Select(t => new TimeSloteShortDto
            {
                DayOfWeek = t.DayOfWeek,
                StartTime = t.StartTime,
                EndTime = t.EndTime,
                SlotDuration = t.SlotDuration,
                IsActive = t.IsActive
            });
            return Ok(timeslot);
        }

        [HttpDelete("{doctorId:int}/{dayOfWeek}")] // Delete : api/TimeSlot/{doctorId}/{dayOfWeek}
        public async Task<ActionResult> DeleteTimeSlotsByDoctorAndDay([FromRoute] int doctorId, [FromRoute] string dayOfWeek)
        {
            var deletedCount = await timeSlotRepository.DeleteTimeSlotsByDoctorAndDayAsync(doctorId, dayOfWeek);
            if (deletedCount > 0)
            {
                return NoContent(); // Return 204 No Content ... successful deletion
            }
            else
            {
                return NotFound(); // Return 404 
            }
        }
        [HttpPut("{id:int}")] // Put : api/TimeSlot/{id}
        public async Task<ActionResult> UpdateTimeSlotAsync([FromRoute] int id, UpdateTimeSlot updateTimeSlot)
        {
            var existingTimeSlot = await timeSlotRepository.GetByIdAsync(id);
            if (existingTimeSlot == null)
            {
                return NotFound(); // Return 404 
            }
            existingTimeSlot.DayOfWeek = updateTimeSlot.DayOfWeek;
            existingTimeSlot.StartTime = updateTimeSlot.StartTime;
            existingTimeSlot.EndTime = updateTimeSlot.EndTime;
            existingTimeSlot.SlotDuration = updateTimeSlot.SlotDuration;
            existingTimeSlot.IsActive = updateTimeSlot.IsActive;
            timeSlotRepository.Update(existingTimeSlot);
            await timeSlotRepository.SaveChangesAsync();
            return NoContent(); // Return 204 No Content ... successful update

        }
        [HttpGet("conflicts/{doctorId:int}/{dayOfWeek}/{startTime}/{endTime}")] // Get : api/TimeSlot/conflicts/{doctorId}/{dayOfWeek}/{startTime}/{endTime}
        public async Task<ActionResult<bool>> CheckForConflictingTimeSlot([FromRoute] int doctorId, [FromRoute] string dayOfWeek, [FromRoute] TimeSpan startTime, [FromRoute] TimeSpan endTime, [FromQuery] int? excludeTimeSlotId = null) // Optional query parameter to exclude a specific time slot (useful for updates)
        {
            var hasConflict = await timeSlotRepository.HasConflictingTimeSlotAsync(doctorId, dayOfWeek, startTime, endTime, excludeTimeSlotId);
            if (hasConflict) 
            {
                return Conflict("The specified time slot conflicts with an existing time slot for the doctor."); // Return 409 
            }
            return Ok(hasConflict); // Return 200 OK with the result
        }
        [HttpGet("doctor/{doctorId:int}/{dayOfWeek}")] // Get : api/TimeSlot/doctor/{doctorId}/{dayOfWeek}
        public async Task<ActionResult<IEnumerable<TimeSloteShortDto>>> GetTimeSlotsByDoctorAndDay([FromRoute] int doctorId, [FromRoute] string dayOfWeek)
        {
            var timeSlots = await timeSlotRepository.GetTimeSlotsByDoctorAndDayAsync(doctorId, dayOfWeek);
            var dto = timeSlots.Select(t => new TimeSloteShortDto
            {
                DayOfWeek = t.DayOfWeek,
                StartTime = t.StartTime,
                EndTime = t.EndTime,
                SlotDuration = t.SlotDuration,
                IsActive = t.IsActive
            });
            return Ok(dto);
        }
        [HttpGet("active/{doctorId:int}")] // Get : api/TimeSlot/active/{doctorId}
        public async Task<ActionResult<IEnumerable<TimeSloteShortDto>>> GetActiveTimeSlotsByDoctorId([FromRoute] int doctorId)
        {
            var timeSlots = await timeSlotRepository.GetActiveTimeSlotsByDoctorIdAsync(doctorId);
            var dto = timeSlots.Select(t => new TimeSloteShortDto
            {
                DayOfWeek = t.DayOfWeek,
                StartTime = t.StartTime,
                EndTime = t.EndTime,
                SlotDuration = t.SlotDuration,
                IsActive = t.IsActive
            });
            return Ok(dto);
        }
        [HttpDelete("{id:int}")] // Delete : api/TimeSlot/{id}
        public async Task<ActionResult> DeleteTimeSlotById([FromRoute] int id)
        {
            var existingTimeSlot = await timeSlotRepository.GetByIdAsync(id);
            if (existingTimeSlot == null)
            {
                return NotFound(); // Return 404 
            }
            timeSlotRepository.Delete(existingTimeSlot);
            await timeSlotRepository.SaveChangesAsync();
            return NoContent(); // Return 204 No Content ... successful deletion
        }




        }

    }
