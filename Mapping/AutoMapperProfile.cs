using AutoMapper;
using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Models;

namespace Hospital_Clinic_Appointment_System.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Doctor, DoctorDto>().ReverseMap();

            CreateMap<DoctorRequestDto, Doctor>().ReverseMap();

            





        }

    }
}
