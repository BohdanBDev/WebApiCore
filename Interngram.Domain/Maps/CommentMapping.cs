using AutoMapper;
using Interngram.Domain.DTOs;
using Interngram.Repository.Models;

namespace Interngram.Domain.Maps;

public class CommentMapping: Profile
{
    public CommentMapping()
    {
        CreateMap<Comment, CommentDTO>()
            .ForMember(source => source.Id, opt => opt.MapFrom(dest => dest.Id))
            .ForMember(source => source.AuthorId, opt => opt.MapFrom(dest => dest.AuthorId))
            .ForMember(source => source.PostId, opt => opt.MapFrom(dest => dest.PostId))
            .ForMember(source => source.Content, opt => opt.MapFrom(dest => dest.Content))
            .ForMember(source => source.Date, opt => opt.MapFrom(dest => dest.Date))
            .ReverseMap();
    }
}