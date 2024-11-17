using AutoMapper;
using LuxuryPaintJohnsonAPI.Models;
using LuxuryPaintJohnsonAPI.Models.Dtos;

namespace LuxuryPaintJohnsonAPI.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Project, ProjectDto>()
			.ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos))
			.ReverseMap();

		CreateMap<Photo, PhotoDto>().ReverseMap();
	}
}
