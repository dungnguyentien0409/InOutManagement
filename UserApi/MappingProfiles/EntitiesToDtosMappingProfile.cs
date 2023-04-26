using System;
using AutoMapper;
using Entities;
using Dto;

namespace MappingProfiles
{
	public class EntitiesToDtosMappingProfile : Profile
	{
		public EntitiesToDtosMappingProfile()
		{
			CreateMap<UserInfo, UserInfoDto>().ReverseMap();
			CreateMap<Role, RoleDto>().ReverseMap();
			CreateMap<UserInfoRole, UserInfoRoleDto>().ReverseMap();
		}
	}
}

