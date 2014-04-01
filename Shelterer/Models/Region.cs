using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shelterer.Models
{
    public class Region : DbRecord
    {
        [Required, Display(Name = "Region")]
        public string RegionName { get; set; }
        public virtual ICollection<MountainRange> MountainRanges { get; set; }
        public virtual ICollection<Shelter> Shelters { get; set; }

        public override Dictionary<string, int> GetFieldIds()
        {
            return FieldIds;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public readonly Dictionary<string, int> FieldIds = new Dictionary<string, int>()
        {
            { "RegionName", 0 },
        };
    }
}