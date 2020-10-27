﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class RouteDTO
    {
        public Guid FirstCityId { get; set; }
        public Guid SecondCityId { get; set; }
        public Guid MapId { get; set; }
        public int Distance { get; set; }
    }
}
