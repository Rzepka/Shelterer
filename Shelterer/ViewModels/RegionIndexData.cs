using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shelterer.Models;


namespace Shelterer.ViewModels
{
    public class RegionIndexData
    {
        public PagedList.IPagedList<Region> Regions { get; set; }
        public IEnumerable<MountainRange> MountainRanges { get; set; }
        public IEnumerable<Shelter> Shelters { get; set; }
    }
}