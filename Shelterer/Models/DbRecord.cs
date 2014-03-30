using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shelterer.Models
{
    public class DbRecord
    {
        public int Id { get; set; }

        public virtual Dictionary<string, int> GetFieldIds()
        {
            return null;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public static readonly Dictionary<string, int> TableIds = new Dictionary<string, int>()
        {
            { "Shelter", 1 },
            { "ObjectType", 2 },
            { "Region", 3 },
            { "MountainRange", 4 }
        };
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public static readonly Dictionary<string, int> DataTypeIds = new Dictionary<string, int>()
        {
            { "Int32", 0 },
            { "Double", 1 },
            { "String", 2 }
        };
    }
}