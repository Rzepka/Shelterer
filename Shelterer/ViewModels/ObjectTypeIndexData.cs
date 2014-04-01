using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shelterer.Models;

namespace Shelterer.ViewModels
{
    public class ObjectTypeIndexData
    {
        public PagedList.IPagedList<ObjectType> ObjectTypes { get; set; }
        public IEnumerable<Shelter> Shelters { get; set; }
    }
}