using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shelterer.Models
{
    public class MountainRange
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<Object> Objects { get; set; }

    }
}