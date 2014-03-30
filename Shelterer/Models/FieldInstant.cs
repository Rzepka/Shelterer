using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shelterer.Models
{
    public class FieldInstant
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public int RecordId { get; set; }
        public int FieldId { get; set; }
        public int DataTypeId { get; set; }
        public string Value { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}