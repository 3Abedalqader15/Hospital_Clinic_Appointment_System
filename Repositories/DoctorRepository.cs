using AutoMapper;
using Hospital_Clinic_Appointment_System.App_Context;
using Hospital_Clinic_Appointment_System.Entities;

namespace Hospital_Clinic_Appointment_System.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DBContext conext;

        private readonly IMapper mapper; 

        public DoctorRepository(DBContext conext, IMapper mapper)
        {
            this.conext = conext;
            this.mapper = mapper;
        }

        




    }
}
