using CQRSServices.Commands;
using CQRSServices.Handlers;
using CQRSServices.Requests;
using CQRSServices.Responses;
using DAL.DTO;
using DAL.Enums;
using DAL.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Moq;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;
using System.Threading;
using WebAPITest.Controllers;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UnitTest
{
    public class CommandHandlersTest
    {      

        //Enter your accountIdentity(Email/ContactNumber)
        private const string _accountIdentity = "testUser@gmail.com";
        private const string _password = "Password1234";
        private const string _recipient = "recipient@gmail.com";

        private const double minValue = 500;
        private const double maxValue = 5000;
        private readonly IAccountsRepository _accountsRepository;
        private readonly IBalanceHistoryRepository _balanceHistoryRepository;
        private readonly ITransactionsRepository _transactionsRepository;

        private AccountsDTO _account;
  

        public CommandHandlersTest()
        {
            this.initializeDummyData();
            var mockAccRepo = new Mock<IAccountsRepository>();
            mockAccRepo.Setup(repo => repo.Authenticate(_accountIdentity, _password))
                        .Returns(Task.FromResult(_account));
            mockAccRepo.Setup(repo => repo.IsAccountExists(_recipient))
                      .Returns(true);
            mockAccRepo.Setup(repo => repo.Get(_recipient))
                      .Returns(Task.FromResult(GenerateRecipientsDTO()));

            _accountsRepository = mockAccRepo.Object;

            var mockBalHistoryRepo = new Mock<IBalanceHistoryRepository>();
            mockBalHistoryRepo.Setup(repo => repo.GetLatestBalanceHistory(_account.Id))
                              .Returns(
                                   Task.FromResult(GenerateBalanceHistoryDTO())
                              );
            mockBalHistoryRepo.Setup(repo => repo.BalanceInquiry(_account.Id))
                              .Returns(
                                  Task.FromResult(GenerateBalanceInquiry())
                              );
            _balanceHistoryRepository = mockBalHistoryRepo.Object;


            var mockTransactionRepo = new Mock<ITransactionsRepository>();

            _transactionsRepository = mockTransactionRepo.Object;

        }

        private void initializeDummyData()
        {
            _account = new AccountsDTO()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000111"),
                Email = "testUser@gmail.com",
                Password = "Password1234",
                FirstName = "Test",
                LastName = "User"
            };

        }

        [Fact]
        public async Task BalanceInquiry()
        {
            // arrange
            var _handler = new BalanceInquiryHandler(_accountsRepository, _balanceHistoryRepository);
            var cancellationTokenSource = new CancellationTokenSource();

            var request = new BalanceInquiryRequest() {
                accountIdentity = _accountIdentity, 
                password = _password 
            };

            var command = new BalanceInquiryCommand(request);

            //act
            var response = await _handler.Handle(command, cancellationTokenSource.Token);

            //assert
            Assert.NotNull(response);
            Assert.IsType<BalanceInquiryResponse>(response);
        }


        [Fact]
        public async Task Deposit()
        {
            // arrange
            var _handler = new DepositHandler(_accountsRepository, _balanceHistoryRepository, _transactionsRepository);
            var cancellationTokenSource = new CancellationTokenSource();

            var request = new DepositRequest()
            {
                accountIdentity = _accountIdentity,
                password = _password,
                Amount = GenerateRandomAmount()
            };

            var command = new DepositCommand(request);

            //act
            var response = await _handler.Handle(command, cancellationTokenSource.Token);

            //assert
            Assert.NotNull(response);
            Assert.IsType<DepositResponse>(response);
        }


        [Fact]
        public async Task Withdrawal()
        {
            // arrange
            var _handler = new WithdrawalHandler(_accountsRepository, _balanceHistoryRepository, _transactionsRepository);
            var cancellationTokenSource = new CancellationTokenSource();

            var request = new WithdrawalRequest()
            {
                accountIdentity = _accountIdentity,
                password = _password,
                Amount = GenerateRandomAmount()
            };

            var command = new WithdrawalCommand(request);

            //act
            var response = await _handler.Handle(command, cancellationTokenSource.Token);

            //assert
            Assert.NotNull(response);
            Assert.IsType<WithdrawalResponse>(response);
        }


        [Fact]
        public async Task TransferMoney()
        {
            // arrange
            var _handler = new TransferMoneyHandler(_accountsRepository, _balanceHistoryRepository, _transactionsRepository);
            var cancellationTokenSource = new CancellationTokenSource();

            var request = new TransferMoneyRequest()
            {
                accountIdentity = _accountIdentity,
                password = _password,
                Amount = GenerateRandomAmount(),
                recipient = _recipient
            };

            var command = new TransferMoneyCommand(request);

            //act
            var response = await _handler.Handle(command, cancellationTokenSource.Token);

            //assert
            Assert.NotNull(response);
            Assert.IsType<TransferMoneyResponse>(response);
        }


        private decimal GenerateRandomAmount()
        {
            var random = new Random();
            var number = (random.NextDouble() * (maxValue - minValue) + minValue);
            return (decimal)number; 
        }

        private BalanceHistoryDTO GenerateBalanceHistoryDTO(TransactionType? transactionType = 0)
        {
          

            var type = transactionType.HasValue ? (int) transactionType: 0;

            if (!transactionType.HasValue)
            {
                var max = 3;
                var min = 0;
                var random = new Random();
                var number = (int)(random.NextDouble() * (3 - 0) + 0);
                type = number;
            }

            var amount = GenerateRandomAmount();
            var balance = GenerateRandomAmount();


            switch (type)
            {
                case (int)TransactionType.TransferMoney:
                case (int)TransactionType.Withdrawal:
                    return new BalanceHistoryDTO
                    {
                        AccountId = _account.Id,
                        CurrentBalance = (double)(balance - amount),
                        PreviousBalance = (double)balance,
                        Debit = 0,
                        Credit = (double)amount,
                    };
                case (int)TransactionType.Deposit:
                default:
                    return new BalanceHistoryDTO
                    {
                        AccountId = _account.Id,
                        CurrentBalance = (double)(balance + amount),
                        PreviousBalance = (double)balance,
                        Debit = (double)amount,
                        Credit = 0
                    };                
            }

        }

        private BalanceInquiryDTO GenerateBalanceInquiry()
        {
            var amount = GenerateRandomAmount();
            return new BalanceInquiryDTO
            {
                CurrentBalance = (double)amount,
                AvailableBalance = (double)amount,
                TransactionDate = DateTime.Now
            };

        }

        private AccountsDTO GenerateRecipientsDTO()
        {
           return new AccountsDTO()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000222"),
                Email = _recipient,
                FirstName = "Recipient",
                LastName = "User"
            };

        }
    }
}