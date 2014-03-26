using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Device.Location;

namespace Shelterer.Models.History
{
    public enum GeoCoordinateRecords
    {
        Coordinates        
    }
    public class GeoCoordinateHistory : RecordHistory
    {
        public GeoCoordinateRecords Record { get; set; }
        public GeoCoordinate Value { get; set; }
    }
}