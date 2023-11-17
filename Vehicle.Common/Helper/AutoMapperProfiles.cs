using AutoMapper;
using VehicleProject.Common.DTOs;
using VehicleProject.Model;

namespace VehicleProject.Common.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<VehicleMake, VehicleMakeDTO>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelDTO>().ReverseMap();
        } 


    }
}
