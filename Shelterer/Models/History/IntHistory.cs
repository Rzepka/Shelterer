using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shelterer.Models.History
{
    public enum IntRecords
    {
        ObjectTypeId, RegionId, MountainRangeId
    }

    public class IntHistory : RecordHistory
    {
        public int Value { get; set; }
    }
}