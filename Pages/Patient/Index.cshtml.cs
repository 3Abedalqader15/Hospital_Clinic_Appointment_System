using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Pages.Shared;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Hospital_Clinic_Appointment_System.Pages.Patient;

[Authorize(Roles = "Patient")]
public class IndexModel(IApiClient apiClient) : AuthenticatedPageModel(apiClient)
{
    public PatientShortDto? Patient { get; private set; }
    public List<AppointmentShortDto> Appointments { get; private set; } = [];
        public string? ErrorMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {

            var patientResult = await ApiClient.GetAsync<PatientShortDto>("/api/Patient/Me");
            if (!patientResult.Success || patientResult.Data == null)
            {
                ErrorMessage = patientResult.Error ?? "Unable to load patient profile.";
                return Page();
            }

            Patient = patientResult.Data;
            var appointmentsResult = await ApiClient.GetAsync<List<AppointmentShortDto>>($"/api/Patient/{Patient.Id}/Appointments");
            if (appointmentsResult.Success && appointmentsResult.Data != null)
            {
                Appointments = [.. appointmentsResult.Data.OrderBy(a => a.AppointmentDate).Take(10)];
            }
            else if (!appointmentsResult.Success)
            {
                ErrorMessage = appointmentsResult.Error ?? "Unable to load appointments.";
            }

            return Page();
        }
    }
