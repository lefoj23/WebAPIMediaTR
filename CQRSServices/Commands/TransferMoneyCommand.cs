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
    public class TransferMoneyCommand : IRequest<TransferMoneyResponse>
    {
        public TransferMoneyRequest _transferMoneyRequest { get; set; }

        public TransferMoneyCommand(TransferMoneyRequest transferMoneyRequest)
        {
            _transferMoneyRequest = transferMoneyRequest;
        }
    }
}
