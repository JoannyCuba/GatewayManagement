using AutoMapper;
using GatewayManagementAPI.Infraestructure.Dtos;

namespace GatewayManagementAPI.Models
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Models.Gateway, GatewayManagementCore.Entities.Gateway>();
            CreateMap<GatewayDto, GatewayManagementCore.Entities.Gateway>();


            CreateMap<PeripheralDeviceDto, PeripheralDevice>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.UID, opt => opt.MapFrom(src => src.uId))
            .ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => src.vendor))
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.dateCreated))
            .ForMember(dest => dest.IsOnline, opt => opt.MapFrom(src => src.isOnline))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.isActive))
            .ForMember(dest => dest.GatewayId, opt => opt.MapFrom(src => src.gatewayId));

            CreateMap<PeripheralDevice, PeripheralDeviceDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.uId, opt => opt.MapFrom(src => src.UID))
                .ForMember(dest => dest.vendor, opt => opt.MapFrom(src => src.Vendor))
                .ForMember(dest => dest.dateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.isOnline, opt => opt.MapFrom(src => src.IsOnline))
                .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.gatewayId, opt => opt.MapFrom(src => src.GatewayId));

        }
    }
}
