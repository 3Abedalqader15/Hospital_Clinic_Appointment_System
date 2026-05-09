using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Pages.Shared;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Pages.Patient
{
    public class DoctorsModel : AuthenticatedPageModel
    {
        public DoctorsModel(IApiClient apiClient) : base(apiClient)
        {
        }

        public List<DoctorListDto> Doctors { get; private set; } = new();

        [BindProperty]
        public CreateAppointmentDto CreateAppointment { get; set; } = new();

        public string? ErrorMessage { get; private set; }
        public string? StatusMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var guard = RequireAuthentication("Patient");
            if (guard != null)
            {
                return guard;
            }

            await LoadDoctorsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostBookAsync()
        {
            var guard = RequireAuthentication("Patient");
            if (guard != null)
            {
                return guard;
            }

            var patientId = await GetPatientIdAsync();
            if (patientId == null)
            {
                ErrorMessage = "Unable to load patient profile.";
                await LoadDoctorsAsync();
                return Page();
            }

            CreateAppointment.PatientId = patientId.Value;
            CreateAppointment.Status = "Scheduled";
            CreateAppointment.Notes ??= string.Empty;

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please review your booking details and try again.";
                await LoadDoctorsAsync();
                return Page();
            }

            var result = await ApiClient.PostAsync<AppointmentDto>("/api/Appointment/Create", CreateAppointment);
            StatusMessage = result.Success ? "Appointment booked successfully." : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Unable to book appointment.";

            await LoadDoctorsAsync();
            return Page();
        }

        private async Task LoadDoctorsAsync()
        {
            var doctorsResult = await ApiClient.GetAsync<List<DoctorListDto>>("/api/Doctor/Active");
            if (doctorsResult.Success && doctorsResult.Data != null)
            {
                Doctors = doctorsResult.Data;
            }
            else
            {
                ErrorMessage = doctorsResult.Error ?? "Unable to load doctors.";
            }
        }

        private async Task<int?> GetPatientIdAsync()
        {
            var patientResult = await ApiClient.GetAsync<PatientShortDto>("/api/Patient/Me");
            return patientResult.Success ? patientResult.Data?.Id : null;
        }
    }
}
