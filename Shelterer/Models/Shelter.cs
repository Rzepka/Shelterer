using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Device.Location;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shelterer.Models
{
    public class Shelter : DbRecord
    {
        [Required]
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
        public int? Capacity { get; set; }
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

        public override Dictionary<string, int> GetFieldIds()
        {
            return FieldIds;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public readonly Dictionary<string, int> FieldIds = new Dictionary<string, int>()
        {
            { "Name", 0 },
            { "ObjectTypeId", 1 },
            { "Altitude", 2 },
            { "Latitude", 3 },
            { "Longitude", 4 },
            { "RegionId", 5 },
            { "MountainRangeId", 6 },
            { "Capacity", 7 },
            { "Owner", 8 },
            { "Opening", 9 },
            { "Location", 10 },
            { "TechnicalCondition", 11 },
            { "Remarks", 12 },
            { "WaterAccess", 13 },
            { "Fireplace", 14 }
        };
        
    }
}