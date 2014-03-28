using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shelterer.Models
{
    public class RecordHistory
    {
        public int Id { get; set; }
        public int ShelterId { get; set; }
        public  int Record { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}