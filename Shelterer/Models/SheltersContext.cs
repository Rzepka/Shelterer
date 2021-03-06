﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shelterer.Models
{
    public class SheltersContext : DbContext
    {
        public DbSet<MountainRange> MountainRanges { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<ObjectType> ObjectTypes { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Image> Images { get; set; }

        public DbSet<FieldInstant> FieldInstants { get; set; }

        public SheltersContext()
            : base("DefaultConnection")
        {

        }
    }
}