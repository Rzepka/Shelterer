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
        public IntRecords Record { get; set; }
        public int Value { get; set; }
        //public int val;
        //public override object Value
        //{
        //    get
        //    {
        //        return (object)val;
        //    }
        //    set
        //    {
        //        val = (int)value;
        //    }
        //}
    }
}