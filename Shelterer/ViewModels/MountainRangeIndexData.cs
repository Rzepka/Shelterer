using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shelterer.Models;

namespace Shelterer.ViewModels
{
    public class MountainRangeIndexData
    {
        public PagedList.IPagedList<MountainRange> MountainRanges { get; set; }
        public IEnumerable<Shelter> Shelters { get; set; }
    }
}