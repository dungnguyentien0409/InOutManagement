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
			CreateMap<Door, DoorDto>().ReverseMap();
		}
	}
}

