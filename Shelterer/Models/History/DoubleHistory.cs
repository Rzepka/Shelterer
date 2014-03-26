using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shelterer.Models.History
{
    public enum DoubleRecords
    {
        Altitude, Latitude, Longitude
    }
    public class DoubleHistory : RecordHistory
    {
        public DoubleRecords Record { get; set; }
        public double Value { get; set; }
    }
}