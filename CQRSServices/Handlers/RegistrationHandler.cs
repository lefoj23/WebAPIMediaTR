using CQRSServices.Commands;
using CQRSServices.Helpers;
using CQRSServices.Responses;
using DAL.DTO;
using DAL.Repositories.Interfaces;
using MediatR;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CQRSServices.Handlers
{
    public class RegistrationHandler: IRequestHandler<RegistrationCommand, RegistrationResponse>
    {
        private readonly IAccountsRepository _accountsRepository;

        public RegistrationHandler(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<RegistrationResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var response = new RegistrationResponse();

                var accIdentity = request._registrationRequest.accountIdentity;
                var isAccountExists = _accountsRepository.IsAccountExists(accIdentity);

                if (isAccountExists)
                {
                    response.Status = false;
                    response.Message = "Account is already registered.";
                    return response;
                }


                var newUser = new AccountsDTO() { 
                    FirstName = request._registrationRequest.firstName,
                    LastName = request._registrationRequest.lastName,
                    Password = request._registrationRequest.password,
                };

                if (accIdentity.isEmail())
                    newUser.Email = accIdentity;

                if (accIdentity.isContactNumber())
                    newUser.ContactNumber = accIdentity;

                
                    

                var result = await _accountsRepository.AddAsync(newUser);

                if(!result)
                {
                    response.Status = false;
                    response.Message = "Something went wrong, coulnt save your account.";
                    return response;
                }

                response.Status = true;
                response.Message = "Registration Success!";

                return response;
            }
            catch (Exception ex)
            {
                return new RegistrationResponse() { Status = false, Message = ex.Message };
            }
        }
    }
}
