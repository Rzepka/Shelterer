using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shelterer.Models.History
{
    public enum StringRecords
    {
        Name, Owner, Opening, Location, TechnicalCondition, Remarks, WaterAccess, Fireplace,
        ObjectTypeName, MountainRangeName, RegionName
    }
    public class StringHistory : RecordHistory
    {
        public string Value { get; set; }
    }
}