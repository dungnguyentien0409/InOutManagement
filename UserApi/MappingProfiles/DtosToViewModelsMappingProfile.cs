using System;
using AutoMapper;
using Common.UserDto;
using Requests;

namespace MappingProfiles
{
	public class DtosToViewModelsMappingProfile : Profile
	{
		public DtosToViewModelsMappingProfile()
		{
			CreateMap<UserInfoDto, SigninRequest>().ReverseMap();
            CreateMap<UserInfoDto, SignupRequest>().ReverseMap();
        }
	}
}

