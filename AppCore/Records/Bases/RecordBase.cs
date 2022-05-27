using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Records.Bases
{
    public abstract class RecordBase 
    {
        public int Id { get; set; }
        public string? Guid  { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }


    }
}
