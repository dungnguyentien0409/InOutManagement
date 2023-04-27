using System;
using AutoMapper;
using Common.AdminDto;
using ViewModels;

namespace MappingProfiles
{
	public class DtosToViewModelsMappingProfile : Profile
	{
		public DtosToViewModelsMappingProfile()
		{
            CreateMap<RoleDto, RoleViewModel>().ReverseMap();
            CreateMap<UserInfoRoleDto, UserInfoRoleViewModel>().ReverseMap();
        }
	}
}

