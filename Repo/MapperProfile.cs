using AutoMapper;
using SocialMediaApp.DTOs;
using SocialMediaApp.Entities;
using SocialMediaApp.Extensions;

namespace SocialMediaApp.Repo;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<AppUser, Memberdto>()
            .ForMember(dest => dest.PhotoUrl,
        opt => opt.MapFrom(src => 
                    src.Photos.FirstOrDefault(x=>x.IsMain).Url))
            .ForMember(dest => dest.Age,
        opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
        CreateMap<Photos, Photosdto>();
    }
}
