using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BalanceHistory : BaseEntity
    {
        public required virtual Accounts Account { get; set; }
        [ForeignKey("Account")]
        public Guid AccountId { get; set; }
        public required double CurrentBalance { get; set; }
        public required double PreviousBalance { get; set; }
        public required double Debit { get; set; }
        public required double Credit { get; set; }
    }
}
