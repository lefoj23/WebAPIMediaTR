using CQRSServices.Commands;
using CQRSServices.Helpers;
using CQRSServices.Responses;
using DAL.Repositories;
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
    public class BalanceInquiryHandler : IRequestHandler<BalanceInquiryCommand, BalanceInquiryResponse>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IBalanceHistoryRepository _balanceHistoryRepository;

        public BalanceInquiryHandler(IAccountsRepository accountsRepository, IBalanceHistoryRepository balanceHistoryRepository)
        {
            _accountsRepository = accountsRepository;
            _balanceHistoryRepository = balanceHistoryRepository;
        }

        public async Task<BalanceInquiryResponse> Handle(BalanceInquiryCommand request, CancellationToken cancellationToken)
        {
            try
            {             
                var accIdentity = request._balanceInquiryRequest.accountIdentity;
                var password = request._balanceInquiryRequest.password;

                var account = await _accountsRepository.Authenticate(accIdentity,password);


                var balInq = await _balanceHistoryRepository.BalanceInquiry( account.Id);

                var result = new BalanceInquiryResponse()
                {
                    CurrentBalance = balInq?.CurrentBalance ?? 0,
                    AvailableBalance = balInq?.AvailableBalance ?? 0,
                    TransactionDate = balInq?.TransactionDate ?? DateTime.Now
                };

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
