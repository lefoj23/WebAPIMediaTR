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
    public class WithdrawalHandler : IRequestHandler<WithdrawalCommand, WithdrawalResponse>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IBalanceHistoryRepository _balanceHistoryRepository;
        private readonly ITransactionsRepository _transactionsRepository;

        public WithdrawalHandler(IAccountsRepository accountsRepository, IBalanceHistoryRepository balanceHistoryRepository, ITransactionsRepository transactionsRepository)
        {
            _accountsRepository = accountsRepository;
            _balanceHistoryRepository = balanceHistoryRepository;
            _transactionsRepository = transactionsRepository;
        }

        public async Task<WithdrawalResponse> Handle(WithdrawalCommand request, CancellationToken cancellationToken)
        {
            try
            {             
                var accIdentity = request._withdrawalRequest.accountIdentity;
                var password = request._withdrawalRequest.password;

                var account = await _accountsRepository.Authenticate(accIdentity,password);

                var withdrawalAmount = (double)request._withdrawalRequest.Amount;
                

                var latestBalHistory = await _balanceHistoryRepository.GetLatestBalanceHistory(account.Id);

                var currentBalance = latestBalHistory?.CurrentBalance ?? 0;


                if (withdrawalAmount > currentBalance) throw new Exception("Insufficient Balance!");

                var transaction = new TransactionsDTO
                {
                    AccountId = account.Id,
                    TransactionType = TransactionType.Withdrawal,
                    Amount = withdrawalAmount
                };


                var newCurrentBalance = (double)currentBalance - withdrawalAmount;
                var newPreviousBalance = currentBalance;

          
                var newBalanceHistory = new BalanceHistoryDTO
                {
                    AccountId = account.Id,
                    CurrentBalance = newCurrentBalance,
                    PreviousBalance = newPreviousBalance,
                    Debit = 0,
                    Credit = withdrawalAmount
                };
                           


                await _transactionsRepository.AddAsync(transaction);

                await _balanceHistoryRepository.AddAsync(newBalanceHistory);


   

                var result = new WithdrawalResponse()
                {
                    CurrentBalance = newCurrentBalance,
                    PreviousBalance = newPreviousBalance,
                    WithdrawalAmount = withdrawalAmount,
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
