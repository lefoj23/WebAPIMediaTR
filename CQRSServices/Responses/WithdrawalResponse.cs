using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSServices.Responses
{
    public class WithdrawalResponse
    {
        public double CurrentBalance { get; set; }
        public double PreviousBalance { get; set; }
        public double WithdrawalAmount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
