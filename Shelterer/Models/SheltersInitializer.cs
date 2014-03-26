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
            new Region{Name="a"},
            new Region{Name="b"},
            new Region{Name="c"},
            };
            regions.ForEach(s => context.Regions.Add(s));
            context.SaveChanges();

            var mountainRanges = new List<MountainRange>
            {
                new MountainRange{Name="aa",Region=regions[0]},
                new MountainRange{Name="ab",Region=regions[0]},
                new MountainRange{Name="ba",Region=regions[1]},
            };
            mountainRanges.ForEach(s => context.MountainRanges.Add(s));
            context.SaveChanges();

            var objectTypes = new List<ObjectType>
            {
            new ObjectType{Name="a1"},
            new ObjectType{Name="b1"},
            new ObjectType{Name="c1"},
            };
            objectTypes.ForEach(s => context.ObjectTypes.Add(s));
            context.SaveChanges();

            var shelters = new List<Shelter>
            {
            new Shelter{Name="AA", MountainRange=mountainRanges[0], ObjectType=objectTypes[0], Altitude=1000.5, Coordinates=new GeoCoordinate(1.0,1.0,1.0,1.0,1.0,1.0,1.0), Remarks="comfy", LastUpdate=new DateTime(2000,1,1)},
            new Shelter{Name="BB", MountainRange=mountainRanges[2], ObjectType=objectTypes[2], Altitude=10.5, Coordinates=new GeoCoordinate(1.0,1.0,1.0,1.0,1.0,1.0,1.0),  Remarks="comfy", LastUpdate=new DateTime(2000,1,1)}
            };
            shelters.ForEach(s => context.Shelters.Add(s));
            context.SaveChanges();
        }
    }
}