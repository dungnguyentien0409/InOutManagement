using System;
using AutoMapper;
using Entities;
using Common.DoorDto;
using Common.AdminDto;

namespace MappingProfiles
{
	public class EntitiesToDtosMappingProfile : Profile
	{
		public EntitiesToDtosMappingProfile()
		{
			CreateMap<Entities.Door, DoorDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<UserInfoRole, UserInfoRoleDto>().ReverseMap();
        }
	}
}

