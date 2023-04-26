using System;
using AutoMapper;
using Common.Door.Dto;
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

