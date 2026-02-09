namespace Hospital_Clinic_Appointment_System.Entities
{
    
        public class Notification : IAuditableEntity
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string Title { get; set; } 
            public string Message { get; set; } 
            public string Type { get; set; } // "AppointmentReminder", "AppointmentConfirmation", "Cancellation"
            public bool IsRead { get; set; } = false;
            public DateTime? ReadAt { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }

            // Relationships
            public User User { get; set; }
        }
    }

