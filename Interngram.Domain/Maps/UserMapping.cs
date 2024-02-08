using AutoMapper;
using Interngram.Domain.DTOs;
using Interngram.Domain.Filters;
using Interngram.Repository.Models;

namespace Interngram.Domain.Maps
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDTO>()
                .ForMember(source => source.Id, opt => opt.MapFrom(dest => dest.Id))
                .ForMember(source => source.FullName, opt => opt.MapFrom(dest => dest.FullName))
                .ForMember(source => source.NickName, opt => opt.MapFrom(dest => dest.NickName))
                .ForMember(source => source.Email, opt => opt.MapFrom(dest => dest.Email.ToLower()))
                .ForMember(source => source.Phone, opt => opt.MapFrom(dest => dest.Phone))
                .ForMember(source => source.City, opt => opt.MapFrom(dest => dest.City))
                .ForMember(source => source.BirthDay, opt => opt.MapFrom(dest => dest.BirthDay))
                .ForMember(source => source.Bio, opt => opt.MapFrom(dest => dest.Bio))
                .ForMember(source => source.Avatar, opt => opt.MapFrom(dest => dest.Avatar))
                .ForMember(source => source.Subscribers, opt => opt.MapFrom(dest => dest.Subscribers))
                .ForMember(source => source.Subscriptions, opt => opt.MapFrom(dest => dest.Subscriptions))
                .ReverseMap();

            CreateMap<UserDTO, UserPreviewDTO>()
                .ForMember(source => source.Id, opt => opt.MapFrom(dest => dest.Id))
                .ForMember(source => source.FullName, opt => opt.MapFrom(dest => dest.FullName))
                .ForMember(source => source.NickName, opt => opt.MapFrom(dest => dest.NickName))
                .ForMember(source => source.Avatar, opt => opt.MapFrom(dest => dest.Avatar))
                .ReverseMap();

        }
    }
}
