using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Pages.Shared;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class PatientsModel : AuthenticatedPageModel
    {
        public PatientsModel(IApiClient apiClient) : base(apiClient)
        {
        }

        public List<PatientShortDto> Patients { get; private set; } = new();
        public List<UserDto> Users { get; private set; } = new();

        [BindProperty]
        public CreatePatientDto CreatePatient { get; set; } = new();

        [BindProperty]
        public PatientShortDto UpdatePatient { get; set; } = new();

        [BindProperty]
        public int UpdatePatientId { get; set; }

        [BindProperty]
        public int DeletePatientId { get; set; }

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
                ErrorMessage = "Please review the patient details and try again.";
                await LoadDataAsync();
                return Page();
            }

            var result = await ApiClient.PostAsync<PatientShortDto>("/api/Patient/Add", CreatePatient);
            StatusMessage = result.Success ? "Patient created successfully." : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Unable to create patient.";

            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {

            var result = await ApiClient.PutAsync($"/api/Patient/{UpdatePatientId}", UpdatePatient);
            StatusMessage = result.Success ? "Patient updated successfully." : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Unable to update patient.";

            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {

            var result = await ApiClient.DeleteAsync($"/api/Patient/{DeletePatientId}");
            StatusMessage = result.Success ? "Patient deleted successfully." : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Unable to delete patient.";

            await LoadDataAsync();
            return Page();
        }

        private async Task LoadDataAsync()
        {
            var patientsResult = await ApiClient.GetAsync<List<PatientShortDto>>("/api/Patient/All");
            var usersResult = await ApiClient.GetAsync<List<UserDto>>("/api/User/All");

            if (patientsResult.Success && patientsResult.Data != null)
            {
                Patients = patientsResult.Data;
            }
            else
            {
                ErrorMessage = patientsResult.Error ?? "Unable to load patients.";
            }

            if (usersResult.Success && usersResult.Data != null)
            {
                Users = usersResult.Data;
            }
        }
    }
}
