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
    public class DepositCommand : IRequest<DepositResponse>
    {
        public DepositRequest _depositRequest { get; set; }

        public DepositCommand(DepositRequest depositRequest)
        {
            _depositRequest = depositRequest;
        }
    }
}
