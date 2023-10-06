using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Transactions : BaseEntity
    {
        public required virtual Accounts Account { get; set; }
        public virtual Accounts? Recipient { get; set; }

        [ForeignKey("Account")]
        public required Guid AccountId { get; set; }
        [ForeignKey("Recipient")]
        public Guid? RecipientId { get; set; }

        public required TransactionType TransactionType { get; set; }
        public required double Amount { get; set; }

        public string? RecipientAccIdentity { get; set; }

    }
}
