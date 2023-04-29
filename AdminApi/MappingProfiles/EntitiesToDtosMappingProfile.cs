using System;
using AutoMapper;
using Entities;
using Common.DoorDto;
using Common.AdminDto;

namespace AdminApi.MappingProfiles
{
	public class EntitiesToDtosMappingProfile : Profile
	{
		public EntitiesToDtosMappingProfile()
		{
			CreateMap<Door, DoorDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<UserInfoRole, UserInfoRoleDto>().ReverseMap();
			CreateMap<DoorRole, DoorRoleDto>().ReverseMap();
        }
	}
}

