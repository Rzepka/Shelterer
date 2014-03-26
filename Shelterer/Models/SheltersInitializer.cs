using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Device.Location;

namespace Shelterer.Models
{
    public class SheltersInitializer : DropCreateDatabaseAlways<SheltersContext>//DropCreateDatabaseIfModelChanges
    {
        protected override void Seed(SheltersContext context)
        {
            var regions = new List<Region>
            {
            new Region{RegionName="a"},
            new Region{RegionName="b"},
            new Region{RegionName="c"},
            };
            regions.ForEach(s => context.Regions.Add(s));
            context.SaveChanges();

            var mountainRanges = new List<MountainRange>
            {
                new MountainRange{MountainRangeName="aa",Region=regions[0]},
                new MountainRange{MountainRangeName="ab",Region=regions[0]},
                new MountainRange{MountainRangeName="ba",Region=regions[1]},
            };
            mountainRanges.ForEach(s => context.MountainRanges.Add(s));
            context.SaveChanges();

            var objectTypes = new List<ObjectType>
            {
            new ObjectType{ObjectTypeName="a1"},
            new ObjectType{ObjectTypeName="b1"},
            new ObjectType{ObjectTypeName="c1"},
            };
            objectTypes.ForEach(s => context.ObjectTypes.Add(s));
            context.SaveChanges();

            var shelters = new List<Shelter>
            {
            new Shelter{Name="AA", MountainRange=mountainRanges[0], ObjectType=objectTypes[0], Altitude=1000.5, Remarks="comfy"},
            new Shelter{Name="BB", MountainRange=mountainRanges[2], ObjectType=objectTypes[2], Altitude=10.5,  Remarks="comfy"},
            };
            shelters.ForEach(s => context.Shelters.Add(s));
            context.SaveChanges();
        }
    }
}