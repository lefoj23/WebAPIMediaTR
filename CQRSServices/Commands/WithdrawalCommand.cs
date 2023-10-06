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
    public class WithdrawalCommand : IRequest<WithdrawalResponse>
    {
        public WithdrawalRequest _withdrawalRequest { get; set; }

        public WithdrawalCommand(WithdrawalRequest withdrawalRequest)
        {
            _withdrawalRequest = withdrawalRequest;
        }
    }
}
