using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Pages.Shared;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Hospital_Clinic_Appointment_System.Pages.Patient
{
    [Authorize(Roles = "Patient")]
    public class AppointmentsModel : AuthenticatedPageModel
    {
        public AppointmentsModel(IApiClient apiClient) : base(apiClient)
        {
        }

        public List<AppointmentShortDto> Appointments { get; private set; } = new();
        public string? ErrorMessage { get; private set; }
        public string? StatusMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {

            await LoadAppointmentsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCancelAsync(int appointmentId)
        {
            return await RunActionAsync($"/api/Appointment/{appointmentId}/Cancel", "Appointment cancelled.");
        }

        public async Task<IActionResult> OnPostRescheduleAsync(int appointmentId, DateTime newAppointmentDate)
        {

            var payload = new RescheduleAppointmentDto { NewAppointmentDate = newAppointmentDate };
            var result = await ApiClient.PostAsync($"/api/Appointment/{appointmentId}/Reschedule", payload);
            return await HandleActionResultAsync(result, "Appointment rescheduled.");
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
            var patientId = await GetPatientIdAsync();
            if (patientId == null)
            {
                ErrorMessage = "Unable to load patient profile.";
                return;
            }

            var result = await ApiClient.GetAsync<List<AppointmentShortDto>>($"/api/Patient/{patientId}/Appointments");
            if (result.Success && result.Data != null)
            {
                Appointments = result.Data.OrderBy(a => a.AppointmentDate).ToList();
            }
            else
            {
                ErrorMessage = result.Error ?? "Unable to load appointments.";
            }
        }

        private async Task<int?> GetPatientIdAsync()
        {
            var patientResult = await ApiClient.GetAsync<PatientShortDto>("/api/Patient/Me");
            return patientResult.Success ? patientResult.Data?.Id : null;
        }
    }
}
