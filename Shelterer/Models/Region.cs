using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shelterer.Models
{
    public class Region
    {
        [Display(Name = "Region")]
        public int Id { get; set; }
        [Display(Name = "Region")]
        public string RegionName { get; set; }
        public virtual ICollection<MountainRange> MountainRanges { get; set; }
        public virtual ICollection<Shelter> Shelters { get; set; }

    }
}