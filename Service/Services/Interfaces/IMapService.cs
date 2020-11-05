﻿using DataAccess.Models;
using Service.DTO;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface IMapService
    {
        IEnumerable<MapGetDTO> GetMaps();
        MapGetDTO GetMap(Guid id);
        Map CreateMap(MapCreateDTO dto);
        Map UpdateMap(MapCreateDTO dto, Guid id);
        bool DeleteMap(Guid id);
    }
}
