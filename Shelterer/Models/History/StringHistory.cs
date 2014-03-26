using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shelterer.Models.History
{
    public enum StringRecords
    {
        Name, Owner, Opening, Location, TechnicalCondition, Remarks, WaterAccess, Fireplace
    }
    public class StringHistory : RecordHistory
    {
        public StringRecords Record { get; set; }
        public string Value { get; set; }
    }
}