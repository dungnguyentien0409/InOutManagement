using System;
using AutoMapper;
using Entities;
using Common.UserDto;

namespace UserApi.MappingProfiles
{
	public class EntitiesToDtosMappingProfile : Profile
	{
		public EntitiesToDtosMappingProfile()
		{
			CreateMap<UserInfo, UserInfoDto>().ReverseMap();
		}
	}
}

