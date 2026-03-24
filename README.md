# Hospital Clinic Appointment System

## 🏥 Overview
A comprehensive web-based application for managing appointments in hospitals and clinics. This system streamlines the appointment scheduling process for patients, doctors, and administrators.

## ✨ Features

### For Patients
- User registration and authentication
- Browse available doctors by specialization
- Book appointments with preferred time slots
- View appointment history and status
- Receive appointment reminders

### For Doctors
- Manage professional profile and specialization
- Set availability and time slots
- View scheduled appointments
- Access patient medical history
- Update appointment status

### For Administrators
- User and role management
- Doctor profile management
- Patient management
- Appointment oversight
- System reporting and analytics

## 🛠️ Technology Stack

- **Backend:** ASP.NET Core 10.0
- **Language:** C#
- **Database:** SQL Server
- **Authentication:** JWT (JSON Web Tokens)
- **ORM:** Entity Framework Core 10.0.3
- **Password Security:** BCrypt.Net-Next 4.1.0
- **API Documentation:** Swagger/Swashbuckle
- **Testing:** Postman

## 📁 Project Structure

```
Hospital_Clinic_Appointment_System/
├── App_Context/              # Entity Framework DbContext
├── Controllers/              # API Controllers
├── CustomActionFilter/       # Custom validation filters
├── Entities/                 # Database entities
├── Models/                   # View models
├── Pages/                    # Razor pages
├── Repositories/             # Data access layer
├── Services/                 # Business logic services
├── Migrations/               # Database migrations
├── Properties/               # Project properties
├── appsettings.json         # Configuration settings
├── appsettings.Development.json
├── Program.cs               # Application startup configuration
└── SQLQuery2.sql            # Sample data script
```

## 📋 Prerequisites

- .NET 10.0 SDK or later
- SQL Server 2019 or later
- Visual Studio 2022 / VS Code
- Git

## 🚀 Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/3Abedalqader15/Hospital_Clinic_Appointment_System.git
cd Hospital_Clinic_Appointment_System
```

### 2. Update Database Connection
Edit `appsettings.json` and update the connection string:
```json
"ConnectionStrings": {
  "HospitalConnectionString": "Server=YOUR_SERVER;Database=Hospital_System_DB;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
}
```

### 3. Configure JWT Settings
```json
"Jwt": {
  "Key": "Your-256-bit-secret-key-at-least-32-characters",
  "Issuer": "HospitalSystemAPI",
  "Audience": "HospitalSystemClient",
  "ExpiryInHours": 8
}
```

### 4. Run Database Migrations
```bash
dotnet ef database update
```

### 5. Load Sample Data (Optional)
Execute `SQLQuery2.sql` in SQL Server Management Studio to populate sample data:
- 1 Admin account
- 4 Sample doctors
- 10 Sample patients
- Doctor availability schedules
- Sample appointments

### 6. Build and Run
```bash
dotnet build
dotnet run
```

The application will be available at `https://localhost:5001`

## 🔐 Authentication & Authorization

### JWT Authentication
- Users receive a JWT token upon login (valid for 8 hours)
- Include token in request header: `Authorization: Bearer <token>`

### User Roles
- **Admin:** Full system access and management
- **Doctor:** Manage availability and view appointments
- **Patient:** Book and manage appointments

### Authorization Policies
- `AdminOnly` - Admin only access
- `DoctorOnly` - Doctor only access
- `PatientOnly` - Patient only access
- `DoctorOrAdmin` - Doctors and admins
- `PatientOrAdmin` - Patients and admins

## 📡 API Endpoints

### Authentication
```
POST   /api/auth/register          Register new user
POST   /api/auth/login             User login
```

### Appointments
```
GET    /api/appointments           Get all appointments
GET    /api/appointments/{id}      Get appointment details
POST   /api/appointments           Create new appointment
PUT    /api/appointments/{id}      Update appointment
DELETE /api/appointments/{id}      Cancel appointment
```

### Doctors
```
GET    /api/doctors                Get all doctors
GET    /api/doctors/{id}           Get doctor details
POST   /api/doctors                Create doctor (Admin)
PUT    /api/doctors/{id}           Update doctor (Admin)
```

### Patients
```
GET    /api/patients               Get all patients
GET    /api/patients/{id}          Get patient details
POST   /api/patients               Create patient
PUT    /api/patients/{id}          Update patient
```

### Time Slots
```
GET    /api/timeslots              Get available time slots
POST   /api/timeslots              Create time slot (Doctor)
PUT    /api/timeslots/{id}         Update time slot
DELETE /api/timeslots/{id}         Delete time slot
```

## 🗄️ Database Schema

### Key Tables
- **Users** - User accounts with credentials and roles
- **Doctors** - Doctor information (specialization, license, bio)
- **Patients** - Patient information (medical history, emergency contact)
- **Appointments** - Appointment records with status tracking
- **TimeSlots** - Doctor availability (day, time, duration)
- **UserRoles** - Role assignments for users

## 📊 Sample Data Included

The `SQLQuery2.sql` script populates:
- Admin user: `Abood321`
- Doctors:
  - Dr. Ahmad Ali (Cardiology) - 10 years experience
  - Dr. Ali Ahmad (Orthopedics) - 8 years experience
  - Dr. Abdalhkeem (General Medicine) - 5 years experience
  - Dr. Abood (Pediatrics) - 7 years experience
- 10 Sample patients
- Doctor availability schedules
- 8 Sample appointments

## 🧪 Testing

### Using Postman
1. Import the Postman collection from `/postman` directory
2. Configure environment variables:
   - `base_url`: http://localhost:5001
   - `token`: (obtained after login)
3. Test endpoints against the running application

### Manual Testing Flow
1. Register a new user account
2. Login and receive JWT token
3. Browse available doctors
4. Book an appointment
5. View appointment history
6. Cancel appointment (if needed)

## 🔧 Configuration Files

- `appsettings.json` - Default application settings
- `appsettings.Development.json` - Development-specific settings
- `Program.cs` - Startup configuration and service registration

## 🤝 Contributing

We welcome contributions! Please follow these guidelines:

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/your-feature-name`
3. Make your changes and commit: `git commit -m 'Add your feature'`
4. Push to the branch: `git push origin feature/your-feature-name`
5. Submit a Pull Request with a clear description

## 📝 Code Standards
- Follow C# naming conventions
- Use meaningful variable and method names
- Add comments for complex logic
- Write unit tests for new features

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👨‍💻 Author

**Abedalqader** - [GitHub](https://github.com/3Abedalqader15)

## 📞 Support & Contact

For issues, bug reports, or feature requests, please:
1. Check existing [Issues](https://github.com/3Abedalqader15/Hospital_Clinic_Appointment_System/issues)
2. Create a new issue with detailed information
3. Contact the author directly

## 🔗 Quick Links

- **Repository:** [Hospital_Clinic_Appointment_System](https://github.com/3Abedalqader15/Hospital_Clinic_Appointment_System)
- **Issues:** [Report Issues](https://github.com/3Abedalqader15/Hospital_Clinic_Appointment_System/issues)
- **Wiki:** [Documentation](https://github.com/3Abedalqader15/Hospital_Clinic_Appointment_System/wiki)

## 📅 Project Status

- **Status:** Active Development
- **Last Updated:** March 24, 2026
- **Version:** 1.0.0

---

**Enjoy using the Hospital Clinic Appointment System!** 🏥✨
