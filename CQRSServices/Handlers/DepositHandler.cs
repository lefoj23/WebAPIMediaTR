using CQRSServices.Commands;
using CQRSServices.Helpers;
using CQRSServices.Responses;
using DAL.DTO;
using DAL.Enums;
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
    public class DepositHandler : IRequestHandler<DepositCommand, DepositResponse>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IBalanceHistoryRepository _balanceHistoryRepository;
        private readonly ITransactionsRepository _transactionsRepository;

        public DepositHandler(IAccountsRepository accountsRepository, IBalanceHistoryRepository balanceHistoryRepository, ITransactionsRepository transactionsRepository)
        {
            _accountsRepository = accountsRepository;
            _balanceHistoryRepository = balanceHistoryRepository;
            _transactionsRepository = transactionsRepository;
        }

        public async Task<DepositResponse> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            try
            {             
                var accIdentity = request._depositRequest.accountIdentity;
                var password = request._depositRequest.password;

                var account = await _accountsRepository.Authenticate(accIdentity,password);

                var depositAmount = (double)request._depositRequest.Amount;
                
         
                var latestBalHistory = await _balanceHistoryRepository.GetLatestBalanceHistory(account.Id);

                var currentBalance = latestBalHistory?.CurrentBalance ?? 0;


                var transaction = new TransactionsDTO
                {
                    AccountId = account.Id,
                    TransactionType = TransactionType.Deposit,
                    Amount = depositAmount,
                    RecipientAccIdentity = accIdentity,
                };


                var newCurrentBalance = (double)currentBalance + depositAmount;
                var newPreviousBalance = currentBalance;

                var newBalanceHistory = new BalanceHistoryDTO
                {
                    AccountId = account.Id,
                    CurrentBalance = newCurrentBalance,
                    PreviousBalance = newPreviousBalance,
                    Debit = depositAmount,
                    Credit = 0
                };

                await _balanceHistoryRepository.AddAsync(newBalanceHistory);


                await _transactionsRepository.AddAsync(transaction);



                var result = new DepositResponse()
                {
                    CurrentBalance = newCurrentBalance,
                    PreviousBalance = newPreviousBalance,
                    DepositedAmount = depositAmount,
                    TransactionDate =  DateTime.Now
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
