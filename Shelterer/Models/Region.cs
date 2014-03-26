using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shelterer.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MountainRange> MountainRanges { get; set; }

    }
}