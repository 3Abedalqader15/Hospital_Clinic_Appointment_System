using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Pages.Shared;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : AuthenticatedPageModel
    {
        public IndexModel(IApiClient apiClient) : base(apiClient)
        {
        }

        public int UsersCount { get; private set; }
        public int DoctorsCount { get; private set; }
        public int PatientsCount { get; private set; }
        public int AppointmentsCount { get; private set; }
        public List<AppointmentAdminDto> RecentAppointments { get; private set; } = new();
        public string? ErrorMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {


            var usersResult = await ApiClient.GetAsync<List<UserDto>>("/api/User/All");
            var doctorsResult = await ApiClient.GetAsync<List<DoctorListDto>>("/api/Doctor/All");
            var patientsResult = await ApiClient.GetAsync<List<PatientShortDto>>("/api/Patient/All");
            var appointmentsResult = await ApiClient.GetAsync<List<AppointmentAdminDto>>("/api/Appointment/All");

            if (!usersResult.Success || !doctorsResult.Success || !patientsResult.Success || !appointmentsResult.Success)
            {
                ErrorMessage = "Some dashboard data could not be loaded. Please refresh or try again.";
            }

            UsersCount = usersResult.Data?.Count ?? 0;
            DoctorsCount = doctorsResult.Data?.Count ?? 0;
            PatientsCount = patientsResult.Data?.Count ?? 0;
            AppointmentsCount = appointmentsResult.Data?.Count ?? 0;

            RecentAppointments = appointmentsResult.Data?
                .OrderByDescending(a => a.AppointmentDate)
                .Take(8)
                .ToList() ?? new List<AppointmentAdminDto>();

            return Page();
        }
    }
}
