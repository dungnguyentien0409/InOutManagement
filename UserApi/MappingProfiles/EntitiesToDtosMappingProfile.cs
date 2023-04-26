using System;
using AutoMapper;
using Entities;
using Common.User.Dto;

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

