using System;
using AutoMapper;
using Common.User.Dto;
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

