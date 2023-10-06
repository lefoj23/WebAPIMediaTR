using DAL.Enums;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class TransactionsDTO
    {
        public Guid AccountId { get; set; }
        public TransactionType TransactionType { get; set; }
        public double Amount { get; set; }
        public Guid? RecipientId { get; set; }
        public string? RecipientAccIdentity { get; set; }

    }
}
