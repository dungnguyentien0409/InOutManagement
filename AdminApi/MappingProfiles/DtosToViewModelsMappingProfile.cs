using System;
using AutoMapper;
using Common.AdminDto;
using Requests;

namespace MappingProfiles
{
	public class DtosToViewModelsMappingProfile : Profile
	{
		public DtosToViewModelsMappingProfile()
		{
            CreateMap<RoleDto, RoleRequest>().ReverseMap();
            CreateMap<UserInfoRoleDto, UserInfoRoleRequest>().ReverseMap();
        }
	}
}

