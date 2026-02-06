using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Repositories;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Clinic_Appointment_System.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("Dashboard/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository patientRepository;
        public PatientController(IPatientRepository patientRepository) 
        {
            this.patientRepository = patientRepository;
        }

        [HttpPost("add")] // Post: Dashboard/patient/add

        public async Task<ActionResult<PatientShortDto>> AddPatientAsync(CreatePatientDto createPatient)
        {
            var patient = new Patient
            {
                Id = createPatient.Id,
                Name = createPatient.Name ,
                Email = createPatient.Email ,
                Phone_Number = createPatient.Phone_Number,
                EmergencyNumber = createPatient.EmergencyNumber,
                MedicalHistory = createPatient.MedicalHistory,
                IsActive = createPatient.IsActive


            };
            await patientRepository.AddAsync(patient);
            await patientRepository.SaveChangesAsync();

            var dto = new PatientShortDto
            {
                Id = patient.Id,
                Name = patient.Name,
                Email = patient.Email,
                Phone_Number = patient.Phone_Number,
                MedicalHistory = patient.MedicalHistory,
                EmergencyNumber = patient.EmergencyNumber,
                IsActive = patient.IsActive



            };

            return CreatedAtAction(nameof(GetPatientByIdAsync), new { id = dto.Id }, dto); // Return 201 Created 


        }



        [HttpGet("All")] // Get : Dashboard/Patient/All
        public async Task<ActionResult<IEnumerable<PatientShortDto>>> GetAllPatientsAsync()
        {
            var patients = await patientRepository.GetAllWithIncludesAsync(p => p.user);

            var dto = patients.Select(p => new PatientShortDto
            {
                Id = p.Id,
                Name = p.user?.Name ?? p.Name,
                Email = p.user?.Email ?? p.Name,
                Phone_Number = p.user?.Phone_Number,
                MedicalHistory = p.MedicalHistory,
                EmergencyNumber = p.EmergencyNumber




            });

            return Ok(dto); // Return 200 OK with the list of patients
        }

        [HttpGet("{id:int}")] // Get : Dashboard/Patient/{id}
        public async Task<ActionResult<PatientShortDto>> GetPatientByIdAsync([FromRoute] int id)
        {
            var patient = await patientRepository.FirstOrDefaultWithIncludesAsync(p => p.Id == id, p => p.user);
            if (patient == null)
            {
                return NotFound();
            }
            var dto = new PatientShortDto
            {
                Id = patient.Id,
                Name = patient.user?.Name ?? patient.Name,
                Email = patient.user?.Email ?? patient.Name,
                Phone_Number = patient.user?.Phone_Number,
                MedicalHistory = patient.MedicalHistory,
                EmergencyNumber = patient.EmergencyNumber
            };
            return Ok(dto); // Return 200 OK 

        }

        // Get: Dashboard/Patient/{id}/MedicalHistory
        [HttpGet("{id:int}/MedicalHistory")]
        public async Task<ActionResult<PatientShortDto>> GetMedicalHistoryByPatientIdAsync([FromRoute] int id)
        {
            var patient = await patientRepository.GetMedicalHistoryByPatientIdAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            var dto = new PatientShortDto
            {
                Id = patient.Id,
                Name = patient.user?.Name ?? patient.Name,
                Email = patient.user?.Email ?? string.Empty,
                Phone_Number = patient.user?.Phone_Number,
                MedicalHistory = patient.MedicalHistory,
                EmergencyNumber = patient.EmergencyNumber
            };

            return Ok(dto);
        }

        // Get: Dashboard/Patient/AppointmentDate/{id}
        [HttpGet("AppointmentDate/{id:int}")]
        public async Task<ActionResult<IEnumerable<AppointmentShortDto>>> GetAppointmentDateByPatientIdAsync([FromRoute] int id)
        {
            var appointments = await patientRepository.GetAppointmentDatesByPatientIdAsync(id);
            if (appointments == null || !appointments.Any())
            {
                return NotFound();
            }

            return Ok(appointments);
        }

        // Delete: Dashboard/Patient/{id}/Delete

        [HttpDelete("{id:int}/Delete")]
        public async Task<IActionResult> DeletePatientAsync([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid patient ID");
            }

            var patient = await patientRepository.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound($"Patient with ID {id} not found");
            }

            try
            {
                
                var appointments = await patientRepository.GetAppointmentDatesByPatientIdAsync(id);
                if (appointments != null && appointments.Any())
                {
                    return BadRequest(new
                    {
                        message = "Cannot delete patient with existing appointments",
                        appointmentCount = appointments.Count()
                    });
                }

                patientRepository.Delete(patient);
                await patientRepository.SaveChangesAsync();

                return Ok(new { message = $"Patient with ID {id} deleted successfully" });
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, new
                {
                    message = "Database error while deleting patient",
                    details = dbEx.InnerException?.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error deleting patient",
                    details = ex.Message
                });
            }
        }
        // Update: Dashboard/Patient/{id}/update

        [HttpPut("{id:int}/update")]
        public async Task<IActionResult> UpdatePatientAsync([FromRoute] int id, [FromBody] PatientShortDto patientDto)
        {
           
            if (id <= 0)
            {
                return BadRequest("Invalid patient ID");
            }

            var patient = await patientRepository.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound($"Patient with ID {id} not found");
            }

            
            patient.Name = patientDto.Name ?? patient.Name;
            patient.MedicalHistory = patientDto.MedicalHistory ?? patient.MedicalHistory;
            patient.EmergencyNumber = patientDto.EmergencyNumber ?? patient.EmergencyNumber;

            patientRepository.Update(patient);
            await patientRepository.SaveChangesAsync();

            var dto = new PatientShortDto
            {
                Id =patient.Id,
                Name = patient.Name,
                Email = patient.Email,
                Phone_Number = patient.Phone_Number,
                MedicalHistory = patient.MedicalHistory,
                EmergencyNumber = patient.EmergencyNumber,
                IsActive = patient.IsActive
            };

            return Ok(dto); // Return 200 OK with updated DTO
        }

    }
    }

