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
        [Range(0, 8850, ErrorMessage = "Proper range <0,8850>")]
        public double? Altitude { get; set; }
        [Range(-90, 90, ErrorMessage = "Proper range <-90,90>")]
        public double? Latitude { get; set; }
        [Range(-180, 180, ErrorMessage = "Proper range <-180,180>")]
        public double? Longitude { get; set; }
        [Display(Name = "Region")]
        public int? RegionId { get; set; }
        [Display(Name = "Region")]
        public virtual Region Region { get; set; }
        public int? MountainRangeId { get; set; }
        public virtual MountainRange MountainRange { get; set; }
        public bool Visited { get; set; }
        public int? Capacity { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public string Owner { get; set; }
        [DataType(DataType.MultilineText),Display()]
        public string Opening { get; set; }
        [DataType(DataType.MultilineText)]
        public string Location { get; set; }
        [DataType(DataType.MultilineText), Display(Name = "Technical Condition")]
        public string TechnicalCondition { get; set; }
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
        [DataType(DataType.MultilineText), Display(Name = "Water Access")]
        public string WaterAccess { get; set; }
        [DataType(DataType.MultilineText)]
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