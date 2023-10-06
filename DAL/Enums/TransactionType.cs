using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enums
{
    public enum TransactionType
    {
       BalanceInquiry = 0,
       Withdrawal = 1,
       Deposit = 2,
       TransferMoney = 3
    }
}
