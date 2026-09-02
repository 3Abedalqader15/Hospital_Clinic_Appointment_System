using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Pages.Shared;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Pages.Admin;

[Authorize(Roles = "Admin")]
public class TimeSlotsModel(IApiClient apiClient) : AuthenticatedPageModel(apiClient)
{
    public List<TimeSloteShortDto> TimeSlots { get; private set; } = [];
    public List<DoctorListDto> Doctors { get; private set; } = [];

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

            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please review the time slot details and try again.";
                await LoadDataAsync();
                return Page();
            }

            var result = await ApiClient.PostAsync<TimeSloteShortDto>("/api/TimeSlot/Add", CreateTimeSlot);
            StatusMessage = result.Success ? "Time slot created successfully." : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Unable to create time slot.";

            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {

            var result = await ApiClient.PutAsync($"/api/TimeSlot/{UpdateTimeSlotId}", UpdateTimeSlot);
            StatusMessage = result.Success ? "Time slot updated successfully." : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Unable to update time slot.";

            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {

            var result = await ApiClient.DeleteAsync($"/api/TimeSlot/{DeleteTimeSlotId}");
            StatusMessage = result.Success ? "Time slot deleted successfully." : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Unable to delete time slot.";

            await LoadDataAsync();
            return Page();
        }

        private async Task LoadDataAsync()
        {
            var slotsResult = await ApiClient.GetAsync<List<TimeSloteShortDto>>("/api/TimeSlot/all");
            var doctorsResult = await ApiClient.GetAsync<List<DoctorListDto>>("/api/Doctor/All");

            if (slotsResult.Success && slotsResult.Data != null)
            {
                TimeSlots = slotsResult.Data;
            }
            else
            {
                ErrorMessage = slotsResult.Error ?? "Unable to load time slots.";
            }

            if (doctorsResult.Success && doctorsResult.Data != null)
            {
                Doctors = doctorsResult.Data;
            }
        }
    }
