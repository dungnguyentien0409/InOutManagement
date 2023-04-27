using System;
using AutoMapper;
using Entities;
using Common.DoorDto;

namespace MappingProfiles
{
	public class EntitiesToDtosMappingProfile : Profile
	{
		public EntitiesToDtosMappingProfile()
		{
			CreateMap<Entities.Door, DoorDto>().ReverseMap();
		}
	}
}

