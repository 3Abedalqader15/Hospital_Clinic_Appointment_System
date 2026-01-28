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

           
            



        }
    }
}
