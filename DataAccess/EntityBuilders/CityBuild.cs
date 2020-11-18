﻿using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityBuilders
{
    public class CityBuild
    {
        public CityBuild(EntityTypeBuilder<City> entityBuilder)
        {
            entityBuilder.HasKey(m => m.Id);
            entityBuilder.Property(m => m.Name).IsRequired();
            entityBuilder.Property(m => m.X).IsRequired();
            entityBuilder.Property(m => m.Y).IsRequired();
        }
    }
}
