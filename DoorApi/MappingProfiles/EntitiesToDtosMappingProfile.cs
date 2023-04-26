﻿using System;
using AutoMapper;
using Entities;
using Common.Door.Dto;

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
