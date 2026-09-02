using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Pages.Shared;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Hospital_Clinic_Appointment_System.Pages.Doctor
{
    [Authorize(Roles = "Doctor")]
    public class TimeSlotsModel : AuthenticatedPageModel
    {
        public TimeSlotsModel(IApiClient apiClient) : base(apiClient)
        {
        }

        public List<TimeSloteShortDto> TimeSlots { get; private set; } = new();

        [BindProperty]
        public CreateTimeSlot CreateTimeSlot { get; set; } = new();

        [BindProperty]
        public UpdateTimeSlot UpdateTimeSlot { get; set; } = new();

        [BindProperty]
        public int UpdateTimeSlotId { get; set; }

        [BindProperty]
        public int DeleteTimeSlotId { get; set; }

        public string? ErrorMessage { get; private set; }
        public string? StatusMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {

            await LoadTimeSlotsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {

            var doctorId = await GetDoctorIdAsync();
            if (doctorId == null)
            {
                ErrorMessage = "Unable to load doctor profile.";
                await LoadTimeSlotsAsync();
                return Page();
            }

            CreateTimeSlot.DoctorId = doctorId.Value;

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please review the time slot details and try again.";
                await LoadTimeSlotsAsync();
                return Page();
            }

            var result = await ApiClient.PostAsync<TimeSloteShortDto>("/api/TimeSlot/Add", CreateTimeSlot);
            StatusMessage = result.Success ? "Time slot created successfully." : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Unable to create time slot.";

            await LoadTimeSlotsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {

            var result = await ApiClient.PutAsync($"/api/TimeSlot/{UpdateTimeSlotId}", UpdateTimeSlot);
            StatusMessage = result.Success ? "Time slot updated successfully." : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Unable to update time slot.";

            await LoadTimeSlotsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {

            var result = await ApiClient.DeleteAsync($"/api/TimeSlot/{DeleteTimeSlotId}");
            StatusMessage = result.Success ? "Time slot deleted successfully." : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Unable to delete time slot.";

            await LoadTimeSlotsAsync();
            return Page();
        }

        private async Task LoadTimeSlotsAsync()
        {
            var doctorId = await GetDoctorIdAsync();
            if (doctorId == null)
            {
                ErrorMessage = "Unable to load doctor profile.";
                return;
            }

            var result = await ApiClient.GetAsync<List<TimeSloteShortDto>>($"/api/TimeSlot/{doctorId}/Doctor");
            if (result.Success && result.Data != null)
            {
                TimeSlots = result.Data.OrderBy(t => t.DayOfWeek).ThenBy(t => t.StartTime).ToList();
            }
            else
            {
                ErrorMessage = result.Error ?? "Unable to load time slots.";
            }
        }

        private async Task<int?> GetDoctorIdAsync()
        {
            var doctorResult = await ApiClient.GetAsync<DoctorDetailsDto>("/api/Doctor/Me");
            return doctorResult.Success ? doctorResult.Data?.Id : null;
        }
    }
}
