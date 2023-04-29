using System;
using AutoMapper;
using Common.AdminDto;
using Requests;

namespace AdminApi.MappingProfiles
{
	public class DtosToViewModelsMappingProfile : Profile
	{
		public DtosToViewModelsMappingProfile()
		{
            CreateMap<RoleDto, RoleRequest>().ReverseMap();
            CreateMap<UserInfoRoleDto, UserInfoRoleRequest>().ReverseMap();
			CreateMap<DoorRoleDto, DoorRoleRequest>().ReverseMap();
        }
	}
}

