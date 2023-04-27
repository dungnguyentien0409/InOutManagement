using System;
using AutoMapper;
using Common.DoorDto;
using ViewModels;

namespace MappingProfiles
{
	public class DtosToViewModelsMappingProfile : Profile
	{
		public DtosToViewModelsMappingProfile()
		{
			CreateMap<InOutHistoryDto, InOutHistoryViewModel>().ReverseMap();
		}
	}
}

