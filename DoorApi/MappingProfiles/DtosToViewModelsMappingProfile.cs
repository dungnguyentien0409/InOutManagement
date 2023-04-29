using System;
using AutoMapper;
using Common.DoorDto;
using Requests;

namespace DoorApi.MappingProfiles
{
	public class DtosToViewModelsMappingProfile : Profile
	{
		public DtosToViewModelsMappingProfile()
		{
			CreateMap<DoorDto, DoorRequest>().ReverseMap();
			CreateMap<TapDoorDto, TapDoorRequest>().ReverseMap();
		}
	}
}

