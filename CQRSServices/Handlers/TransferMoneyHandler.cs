using CQRSServices.Commands;
using CQRSServices.Helpers;
using CQRSServices.Responses;
using DAL.DTO;
using DAL.Enums;
using DAL.Models;
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
    public class TransferMoneyHandler : IRequestHandler<TransferMoneyCommand, TransferMoneyResponse>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IBalanceHistoryRepository _balanceHistoryRepository;
        private readonly ITransactionsRepository _transactionsRepository;

        public TransferMoneyHandler(IAccountsRepository accountsRepository, IBalanceHistoryRepository balanceHistoryRepository, ITransactionsRepository transactionsRepository)
        {
            _accountsRepository = accountsRepository;
            _balanceHistoryRepository = balanceHistoryRepository;
            _transactionsRepository = transactionsRepository;
        }

        public async Task<TransferMoneyResponse> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
        {
            try
            {             
                var accIdentity = request._transferMoneyRequest.accountIdentity;
                var password = request._transferMoneyRequest.password;
                var recipient = request._transferMoneyRequest.recipient;
                var transferMoneyAmount = (double)request._transferMoneyRequest.Amount;

                var account = await _accountsRepository.Authenticate(accIdentity,password);

                var isRecipientsExists = _accountsRepository.IsAccountExists(recipient);

                if(!isRecipientsExists) throw new Exception("Recipient does not exists!");



                var recipientAcc = await _accountsRepository.Get(recipient);


                var latestBalHistory = await _balanceHistoryRepository.GetLatestBalanceHistory(account.Id);

                var currentBalance = latestBalHistory?.CurrentBalance ?? 0;


                if (transferMoneyAmount > currentBalance) throw new Exception("Insufficient Balance!");


                var balanceHistoryList = new List<BalanceHistoryDTO>();

                #region userData
                    var newCurrentBalance = (double)currentBalance - transferMoneyAmount;
                    var newPreviousBalance = currentBalance;


                    var newBalanceHistory = new BalanceHistoryDTO
                    {
                        AccountId = account.Id,
                        CurrentBalance = newCurrentBalance,
                        PreviousBalance = newPreviousBalance,
                        Debit = 0,
                        Credit = transferMoneyAmount
                    };

                balanceHistoryList.Add(newBalanceHistory);
                #endregion


                #region recipient
                    var recipientBalHistory = await _balanceHistoryRepository.GetLatestBalanceHistory(recipientAcc.Id);
                    var recipientCurrBalance = recipientBalHistory?.CurrentBalance ?? 0;
                    var newRecipientCurrBal = (double)recipientCurrBalance + transferMoneyAmount;
                    var newRecipientPrevBal = recipientCurrBalance;


                    var newRecipientBalHistory = new BalanceHistoryDTO
                    {
                        AccountId = recipientAcc.Id,
                        CurrentBalance = newRecipientCurrBal,
                        PreviousBalance = newRecipientPrevBal,
                        Debit = transferMoneyAmount,
                        Credit = 0
                    };

                    balanceHistoryList.Add(newRecipientBalHistory);
                #endregion



                var transaction = new TransactionsDTO
                {
                    AccountId = account.Id,
                    TransactionType = TransactionType.TransferMoney,
                    Amount = transferMoneyAmount,
                    RecipientId = recipientAcc.Id,
                    RecipientAccIdentity = accIdentity,
                };

                await _transactionsRepository.AddAsync(transaction);

                await _balanceHistoryRepository.AddRangeAsync(balanceHistoryList);

                var result = new TransferMoneyResponse()
                {
                    CurrentBalance = newCurrentBalance,
                    PreviousBalance = newPreviousBalance,
                    Amount = transferMoneyAmount,
                    Recipient = recipient,
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
