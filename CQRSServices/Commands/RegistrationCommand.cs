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
    public class RegistrationCommand : IRequest<RegistrationResponse>
    {
        public RegistrationRequest _registrationRequest { get; set; }

        public RegistrationCommand(RegistrationRequest registrationRequest)
        {
            _registrationRequest = registrationRequest;
        }
    }
}
