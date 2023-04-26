using System;
using AutoMapper;
using Common.Admin.Dto;
using ViewModels;
using Common.Admin.Dto;

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

