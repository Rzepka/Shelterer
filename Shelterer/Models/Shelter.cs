using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Device.Location;

namespace Shelterer.Models
{
    public class Shelter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ObjectTypeId { get; set; }
        public virtual ObjectType ObjectType { get; set; }
        public double Altitude { get; set; }
        public GeoCoordinate Coordinates { get; set; }
        //public int RegionId { get; set; }
        //public virtual Region Region { get; set; }
        public int MountainRangeId { get; set; }
        public virtual MountainRange MountainRange { get; set; }
        public bool Visited { get; set; }
        public string Owner { get; set; }
        public string Opening { get; set; }
        public string Location { get; set; }
        public string TechnicalCondition { get; set; }
        public string Remarks { get; set; }
        public string WaterAccess { get; set; }
        public string Fireplace { get; set; }
        public DateTime LastUpdate { get; set; }

        
    }
}