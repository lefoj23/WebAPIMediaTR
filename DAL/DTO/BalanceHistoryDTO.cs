using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class BalanceHistoryDTO
    {
        public Guid AccountId { get; set; }
        public double CurrentBalance { get; set; }
        public double PreviousBalance { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
    }
}
