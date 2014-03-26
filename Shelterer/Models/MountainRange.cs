using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shelterer.Models
{
    public class MountainRange
    {
        public int Id { get; set; }
        [Display(Name = "Mountain Range")]
        public string MountainRangeName { get; set; }
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<Shelter> Shelters { get; set; }

    }
}