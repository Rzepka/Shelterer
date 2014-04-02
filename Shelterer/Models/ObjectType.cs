using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shelterer.Models
{
    public class ObjectType : DbRecord
    {
        [Required, Display(Name = "Object Type")]
        public string ObjectTypeName { get; set; }
        public virtual ICollection<Shelter> Shelters { get; set; }

        //public override Dictionary<string, int> GetFieldIds()
        //{
        //    return FieldIds;
        //}
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public static readonly Dictionary<string, int> FieldIds = new Dictionary<string, int>()
        {
            { "ObjectTypeName", 0 },
        };
    }
}