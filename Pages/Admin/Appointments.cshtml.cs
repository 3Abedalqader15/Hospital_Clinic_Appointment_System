using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Pages.Shared;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class AppointmentsModel : AuthenticatedPageModel
    {
        public AppointmentsModel(IApiClient apiClient) : base(apiClient)
        {
        }

        public List<AppointmentAdminDto> Appointments { get; private set; } = new();
        public string? ErrorMessage { get; private set; }
        public string? StatusMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {

            await LoadAppointmentsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCancelAsync(int appointmentId)
        {
            return await RunActionAsync($"/api/Appointment/{appointmentId}/Cancel", "Appointment cancelled successfully.");
        }

        public async Task<IActionResult> OnPostCompleteAsync(int appointmentId, string? notes)
        {
            var result = await ApiClient.PostAsync($"/api/Appointment/{appointmentId}/Complete", notes ?? string.Empty);
            return await HandleActionResultAsync(result, "Appointment completed successfully.");
        }

        public async Task<IActionResult> OnPostNoShowAsync(int appointmentId)
        {
            return await RunActionAsync($"/api/Appointment/{appointmentId}/NoShow", "Appointment marked as no-show.");
        }

        public async Task<IActionResult> OnPostReminderAsync(int appointmentId)
        {
            return await RunActionAsync($"/api/Appointment/{appointmentId}/SendReminder", "Reminder sent.");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int appointmentId)
        {

            var result = await ApiClient.DeleteAsync($"/api/Appointment/{appointmentId}");
            return await HandleActionResultAsync(result, "Appointment deleted successfully.");
        }

        public async Task<IActionResult> OnPostRescheduleAsync(int appointmentId, DateTime newAppointmentDate)
        {

            var payload = new RescheduleAppointmentDto { NewAppointmentDate = newAppointmentDate };
            var result = await ApiClient.PostAsync($"/api/Appointment/{appointmentId}/Reschedule", payload);
            return await HandleActionResultAsync(result, "Appointment rescheduled successfully.");
        }

        private async Task<IActionResult> RunActionAsync(string url, string successMessage)
        {

            var result = await ApiClient.PostAsync(url, new { });
            return await HandleActionResultAsync(result, successMessage);
        }

        private async Task<IActionResult> HandleActionResultAsync(ApiResult result, string successMessage)
        {
            StatusMessage = result.Success ? successMessage : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Action failed.";
            await LoadAppointmentsAsync();
            return Page();
        }

        private async Task LoadAppointmentsAsync()
        {
            var result = await ApiClient.GetAsync<List<AppointmentAdminDto>>("/api/Appointment/All");
            if (result.Success && result.Data != null)
            {
                Appointments = result.Data.OrderByDescending(a => a.AppointmentDate).ToList();
            }
            else
            {
                ErrorMessage = result.Error ?? "Unable to load appointments.";
            }
        }
    }
}
