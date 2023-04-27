using System;
using AutoMapper;
using Common.UserDto;
using ViewModels;

namespace MappingProfiles
{
	public class DtosToViewModelsMappingProfile : Profile
	{
		public DtosToViewModelsMappingProfile()
		{
			CreateMap<UserInfoDto, UserViewModel>().ReverseMap();
		}
	}
}

