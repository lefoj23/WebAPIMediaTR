using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class BalanceInquiryDTO
    {
        public double CurrentBalance { get; set; }
        public double AvailableBalance { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
