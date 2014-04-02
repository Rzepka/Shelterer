using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Shelterer.Models
{
    public class DbRecord
    {
        public int Id { get; set; }

        //public virtual Dictionary<string, int> GetFieldIds()
        //{
        //    return null;
        //}
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public static readonly Dictionary<string, int> TableIds = new Dictionary<string, int>()
        //{
        //    { "Shelter", 1 },
        //    { "ObjectType", 2 },
        //    { "Region", 3 },
        //    { "MountainRange", 4 },
        //    { "Image", 5 }
        //};
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public static readonly Dictionary<string, int> DataTypeIds = new Dictionary<string, int>()
        {
            { "Int32", 0 },
            { "Double", 1 },
            { "String", 2 }
        };
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public static readonly Dictionary<string, Dictionary<string, int>> FieldIds = new Dictionary<string, Dictionary<string, int>>()
        //{
        //    { "Shelter", Shelter.FieldIds },
        //    { "ObjectType", ObjectType.FieldIds },
        //    { "Region", Region.FieldIds },
        //    { "MountainRange", MountainRange.FieldIds },
        //    { "Image", Image.FieldIds }
        //};
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public static readonly Dictionary<string, Tuple<int, Dictionary<string, int>, PropertyInfo>> TableFieldIds = new Dictionary<string, Tuple<int, Dictionary<string, int>, PropertyInfo>>()
        {
            { "Shelter", Tuple.Create( 1, Shelter.FieldIds, typeof(SheltersContext).GetProperty("Shelters") ) },
            { "ObjectType", Tuple.Create( 2, ObjectType.FieldIds, typeof(SheltersContext).GetProperty("ObjectTypes") ) },
            { "Region", Tuple.Create( 3, Region.FieldIds, typeof(SheltersContext).GetProperty("Regions") ) },
            { "MountainRange", Tuple.Create( 4, MountainRange.FieldIds, typeof(SheltersContext).GetProperty("MountainRanges") ) },
            { "Image", Tuple.Create( 5, Image.FieldIds, typeof(SheltersContext).GetProperty("Images") ) }
        };
    }
}