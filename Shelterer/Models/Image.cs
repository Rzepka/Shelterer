using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shelterer.Models
{
    public class Image : DbRecord
    {
        public int ShelterId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
        public byte[] FileContent { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public override Dictionary<string, int> GetFieldIds()
        {
            return FieldIds;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public readonly Dictionary<string, int> FieldIds = new Dictionary<string, int>()
        {
            { "ShelterId", 0 },
            { "Name", 1 },
        };
    }
}