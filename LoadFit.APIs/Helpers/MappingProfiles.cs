using AutoMapper;
using LoadFit.APIs.DTOs;
using LoadFit.Core.Entities;
using LoadFit.Core.Order_Aggregate;

namespace LoadFit.APIs.Helpers
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {

            CreateMap<Vehicle, VehicleDto>()
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Name)) 
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>())
                .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.DriverId))
                .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.Length))  
                .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Width))    
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height)); 

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Address>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.Vehicle, o => o.MapFrom(s => s.Vehicle.Name))
                .ForMember(d => d.VehicleCost, o => o.MapFrom(s => s.Vehicle.price));

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Core.Entities.Identity.Address, UserAddressDto>();

            CreateMap<RegisterVehicleDto, Vehicle>();

        }

    }
}
