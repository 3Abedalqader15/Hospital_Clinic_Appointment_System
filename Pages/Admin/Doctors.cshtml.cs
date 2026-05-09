using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Pages.Shared;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Pages.Admin
{
    public class DoctorsModel : AuthenticatedPageModel
    {
        public DoctorsModel(IApiClient apiClient) : base(apiClient)
        {
        }

        public List<DoctorListDto> Doctors { get; private set; } = new();
        public List<UserDto> Users { get; private set; } = new();

        [BindProperty]
        public CreateDoctorDto CreateDoctor { get; set; } = new() { isActive = true };

        [BindProperty]
        public updateDoctorDto UpdateDoctor { get; set; } = new();

        [BindProperty]
        public int UpdateDoctorId { get; set; }

        [BindProperty]
        public int DeleteDoctorId { get; set; }

        public string? ErrorMessage { get; private set; }
        public string? StatusMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var guard = RequireAuthentication("Admin");
            if (guard != null)
            {
                return guard;
            }

            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            var guard = RequireAuthentication("Admin");
            if (guard != null)
            {
                return guard;
            }

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please check the doctor details and try again.";
                await LoadDataAsync();
                return Page();
            }

            var result = await ApiClient.PostAsync<DoctorDetailsDto>("/api/Doctor/Add", CreateDoctor);
            StatusMessage = result.Success ? "Doctor created successfully." : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Unable to create doctor.";

            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            var guard = RequireAuthentication("Admin");
            if (guard != null)
            {
                return guard;
            }

            var result = await ApiClient.PutAsync($"/api/Doctor/{UpdateDoctorId}", UpdateDoctor);
            StatusMessage = result.Success ? "Doctor updated successfully." : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Unable to update doctor.";

            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var guard = RequireAuthentication("Admin");
            if (guard != null)
            {
                return guard;
            }

            var result = await ApiClient.DeleteAsync($"/api/Doctor/{DeleteDoctorId}");
            StatusMessage = result.Success ? "Doctor deleted successfully." : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Unable to delete doctor.";

            await LoadDataAsync();
            return Page();
        }

        private async Task LoadDataAsync()
        {
            var doctorsResult = await ApiClient.GetAsync<List<DoctorListDto>>("/api/Doctor/All");
            var usersResult = await ApiClient.GetAsync<List<UserDto>>("/api/User/All");

            if (doctorsResult.Success && doctorsResult.Data != null)
            {
                Doctors = doctorsResult.Data;
            }
            else
            {
                ErrorMessage = doctorsResult.Error ?? "Unable to load doctors.";
            }

            if (usersResult.Success && usersResult.Data != null)
            {
                Users = usersResult.Data;
            }
        }
    }
}
