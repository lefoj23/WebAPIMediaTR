using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSServices.Requests
{
    public class BalanceInquiryRequest
    {
        public required string accountIdentity { set; get; }
        public required string password { set; get; }
    }
}
