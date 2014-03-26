using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shelterer.Models
{
    public class ObjectType
    {
        public int Id { get; set; }
        [Display(Name = "Object Type")]
        public string ObjectTypeName { get; set; }
        public virtual ICollection<Shelter> Shelters { get; set; }

    }
}