﻿using AutoMapper;
using DesktopApp.Models;

namespace DesktopApp.APIInteraction.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<City, DataAccess.Models.CityDTO>()
                .ForMember(dest => dest.Position,
                    map => map.MapFrom(
                    source => new DataAccess.Models.Position
                    {
                          X = source.X,
                          Y = source.Y
                    }));

            CreateMap<DataAccess.DTO.SettingsDTO, Settings>();
        }
    }
}