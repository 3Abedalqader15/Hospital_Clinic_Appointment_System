using Hospital_Clinic_Appointment_System.Entities;

using Microsoft.EntityFrameworkCore;

namespace Hospital_Clinic_Appointment_System.App_Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        // DbSets for your entities

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<TimeSlot> TimeSlots { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.user)
                .WithOne(u => u.Doctor)
                .HasForeignKey<Doctor>(d => d.User_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.user)
                .WithOne(u => u.Patient)
                .HasForeignKey<Patient>(p => p.User_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId) 
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId) 
                
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TimeSlot>()
                .HasOne(ts => ts.doctor)
                .WithMany(d => d.TimeSlots)
                .HasForeignKey(ts => ts.DoctorId) 
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.User_Id, ur.Role_Id });


            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.user)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.User_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.Role_Id)
                .OnDelete(DeleteBehavior.Cascade);


        

               modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Doctor" },
                new Role { Id = 2, Name = "Patient" },
                new Role { Id = 3, Name = "Admin" }
                );


            modelBuilder.Entity<User>().HasData
                (

                new User { Id = 1000, Name = "Abood321", Password = "123456789@", Email = "Abood@Gmail.com", BirthDay = new DateTime(2003, 5, 1), IsEmailConfirmed = true, Created_At = new DateTime(2026, 1, 30), isActive = true, Phone_Number = "0779875103", Updated_At = new DateTime(2026, 1, 30) },

                new User { Id = 1001, Name = "Dr.Ahmad Ali", Password = "AhmadAli@123", Email = "ahmadali@Gmail.com", BirthDay = new DateTime(1999, 2, 1), IsEmailConfirmed = true, Created_At = new DateTime(2026, 1, 30), isActive = true, Phone_Number = "0779875103", Updated_At = new DateTime(2026, 1, 30) },

                new User { Id = 1002, Name = "Dr.Ali Ahmad", Password = "AliAhmad@1234", Email = "alihmad@Gmail.com", BirthDay = new DateTime(1998, 2, 1), IsEmailConfirmed = true, Created_At = new DateTime(2026, 1, 30), isActive = true, Phone_Number = "0779875103", Updated_At = new DateTime(2026, 1, 30) }


                );

            modelBuilder.Entity<UserRole>().HasData
                (

                new UserRole { User_Id = 1000, Role_Id = 3 }, // Abood321 is Patient 
                new UserRole { User_Id = 1001, Role_Id = 2 }, // Dr.Ahmad Ali is Doctor
                new UserRole { User_Id = 1002, Role_Id = 2 }  // Dr.Ali Ahmad is Doctor



                );

            modelBuilder.Entity<Doctor>().HasData(
    new Doctor
    {
        Id = 1,
        User_Id = 1001,           // يشير للـ User اللي عملناه
        Name = "Dr. Ahmad Ali",  // اسم الدكتور نفسه
        Specialization = "Cardiology",
        LicenseNumber = "DOC-001",
        ExperienceYears = 10,
        Bio = "Cardiac surgeon",
        profilePictureUrl = "/images/doctors/ahmad.jpg",
        isActive = true,
        Created_At = new DateTime(2026, 1, 30)
    },
    new Doctor
    {
        Id = 2,
        User_Id = 1002,
        Name = "Dr. Ali Ahmad",
        Specialization = "Orthopedics",
        LicenseNumber = "DOC-002",
        ExperienceYears = 8,
        Bio = "orthopedic specialist",
        profilePictureUrl = "/images/doctors/ali.jpg",
        isActive = true,
        Created_At = new DateTime(2026, 1, 30)
    }
);








        }
    }
}
