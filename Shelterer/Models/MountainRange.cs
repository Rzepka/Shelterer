using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shelterer.Models
{
    public class MountainRange : DbRecord
    {
        [Required, Display(Name = "Mountain Range")]
        public string MountainRangeName { get; set; }
        [Display(Name = "Region")]
        public int RegionId { get; set; }
        [Display(Name = "Region")]
        public virtual Region Region { get; set; }
        public virtual ICollection<Shelter> Shelters { get; set; }

        public override Dictionary<string, int> GetFieldIds()
        {
            return FieldIds;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public readonly Dictionary<string, int> FieldIds = new Dictionary<string, int>()
        {
            { "MountainRangeName", 0 },
            { "RegionId", 1 },
        };
    }
}