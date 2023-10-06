using CQRSServices.Requests;
using CQRSServices.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSServices.Commands
{
    public class BalanceInquiryCommand : IRequest<BalanceInquiryResponse>
    {
        public BalanceInquiryRequest _balanceInquiryRequest { get; set; }

        public BalanceInquiryCommand(BalanceInquiryRequest balanceInquiryRequest)
        {
            _balanceInquiryRequest = balanceInquiryRequest;
        }
    }
}
