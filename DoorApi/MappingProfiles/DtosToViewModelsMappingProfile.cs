using System;
using AutoMapper;
using Dto;
using ViewModels;

namespace MappingProfiles
{
	public class DtosToViewModelsMappingProfile : Profile
	{
		public DtosToViewModelsMappingProfile()
		{
			CreateMap<DoorDto, DoorViewModel>().ReverseMap();
		}
	}
}

