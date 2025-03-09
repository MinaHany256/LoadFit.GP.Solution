using AdminDashboard.Models;
using AutoMapper;
using LoadFit.Core.Entities;

namespace AdminDashboard.Helpers
{
    public class MapsProfile : Profile
    {
        public MapsProfile()
        {
            CreateMap<Vehicle, VehicleViewModel>().ReverseMap();
        }
    }
}
