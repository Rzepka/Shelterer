using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shelterer.Models;

namespace Shelterer.ViewModels
{
    public class ShelterIndexData
    {
        public PagedList.IPagedList<Shelter> Shelters { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}