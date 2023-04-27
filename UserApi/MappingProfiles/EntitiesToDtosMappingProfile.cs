using System;
using AutoMapper;
using Entities;
using Common.UserDto;

namespace MappingProfiles
{
	public class EntitiesToDtosMappingProfile : Profile
	{
		public EntitiesToDtosMappingProfile()
		{
			CreateMap<UserInfo, UserInfoDto>().ReverseMap();
			
		}
	}
}

