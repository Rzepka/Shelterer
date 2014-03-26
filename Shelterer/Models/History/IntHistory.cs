using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shelterer.Models.History
{
    public enum IntRecords
    {
        TypeId, MountainRangeId
    }
    public class IntHistory : RecordHistory
    {
        public IntRecords Record { get; set; }
        public int Value { get; set; }
    }
}