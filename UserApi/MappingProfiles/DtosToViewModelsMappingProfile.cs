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
			CreateMap<UserInfoDto, UserViewModel>().ReverseMap();
			CreateMap<RoleDto, RoleViewModel>().ReverseMap();
			CreateMap<UserInfoRoleDto, UserInfoRoleViewModel>().ReverseMap();
		}
	}
}

