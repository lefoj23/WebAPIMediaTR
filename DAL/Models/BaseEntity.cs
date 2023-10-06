using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public Guid CreatedBy { get; set; }
        public DateTimeOffset ModifiedDate {  get; set; } = DateTimeOffset.UtcNow;
        public Guid ModifiedBy { get; set; } 
        public bool IsDeleted { get; set; }
    }
}
