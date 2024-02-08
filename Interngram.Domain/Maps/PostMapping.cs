using AutoMapper;
using Interngram.Domain.DTOs;
using Interngram.Repository.Models;

namespace Interngram.Domain.Maps;

public class PostMapping : Profile
{
    public PostMapping()
    {
        CreateMap<Post, PostDTO>()
            .ForMember(source => source.Id, opt => opt.MapFrom(dest => dest.Id))
            .ForMember(source => source.AuthorId, opt => opt.MapFrom(dest => dest.AuthorId))
            .ForMember(source => source.Description, opt => opt.MapFrom(dest => dest.Description))
            .ForMember(source => source.Image, opt => opt.MapFrom(dest => dest.Image))
            .ForMember(source => source.Date, opt => opt.MapFrom(dest => dest.Date))
            .ForMember(source => source.Likes, opt => opt.MapFrom(dest => dest.Likes))
            .ForMember(source => source.Comments, opt => opt.MapFrom(dest => dest.Comments))
            .ReverseMap();

        CreateMap<PostDTO, PostPreviewDTO>()
            .ForMember(source => source.Id, opt => opt.MapFrom(dest => dest.Id))
            .ForMember(source => source.AuthorId, opt => opt.MapFrom(dest => dest.AuthorId))
            .ForMember(source => source.Image, opt => opt.MapFrom(dest => dest.Image))
            .ReverseMap();

        CreateMap<Post, PostCreateDTO>()
            .ForMember(source => source.Id, opt => opt.MapFrom(dest => dest.Id))
            .ForMember(source => source.AuthorId, opt => opt.MapFrom(dest => dest.AuthorId))
            .ForMember(source => source.Description, opt => opt.MapFrom(dest => dest.Description))
            .ForMember(source => source.Image, opt => opt.MapFrom(dest => dest.Image))
            .ForMember(source => source.Date, opt => opt.MapFrom(dest => dest.Date))
            .ReverseMap();
    }
}