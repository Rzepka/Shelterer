using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Device.Location;
using System.ComponentModel.DataAnnotations;

namespace Shelterer.Models
{
    public class Shelter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ObjectTypeId { get; set; }
        public virtual ObjectType ObjectType { get; set; }
        public double? Altitude { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        [Display(Name = "Region")]
        public int? RegionId { get; set; }
        [Display(Name = "Region")]
        public virtual Region Region { get; set; }
        public int? MountainRangeId { get; set; }
        public virtual MountainRange MountainRange { get; set; }
        public bool Visited { get; set; }
        public string Owner { get; set; }
        public string Opening { get; set; }
        public string Location { get; set; }
        [Display(Name = "Technical Condition")]
        public string TechnicalCondition { get; set; }
        public string Remarks { get; set; }
        [Display(Name = "Water Access")]
        public string WaterAccess { get; set; }
        public string Fireplace { get; set; }
        [Display(Name = "Last Update")]
        public DateTime? LastUpdate { get; set; }
        
    }
}