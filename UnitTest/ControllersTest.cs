using CQRSServices.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPITest.Controllers;
using Xunit;

namespace UnitTest
{
    public class ControllersTest
    {
        private AccountController _accController;
        private TransactionController _transactionController;


        //Enter your accountIdentity(Email/ContactNumber)
        private const string _accountIdentity = "testUser@gmail.com";
        private const string _password = "Password1234";
        private const string _recipient = "recipient@gmail.com";

        private const double minValue = 500;
        private const double maxValue = 5000;


        public ControllersTest() {
            var mediatorMock = new Mock<IMediator>();

            _accController = new AccountController(mediatorMock.Object);
            _transactionController = new TransactionController(mediatorMock.Object);
        }

        [Fact]
        public async Task Registration()
        {
            // arrange
            var request = new RegistrationRequest()
            {
                accountIdentity = _accountIdentity,
                password = _password,
                firstName = "Test",
                lastName = "User"
            };

            //act
            var actionResult = await _accController.Registration(request);
            var okResult = actionResult as OkObjectResult;


            //assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task BalanceInquiry()
        {
            // arrange
            var request = new BalanceInquiryRequest() {
                accountIdentity = _accountIdentity, 
                password = _password 
            };

            //act
            var actionResult = await _transactionController.BalanceInquiry(request);
            var okResult = actionResult as OkObjectResult;


            //assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }


        [Fact]
        public async Task Deposit()
        {
            // arrange
            var request = new DepositRequest()
            {
                accountIdentity = _accountIdentity,
                password = _password,
                Amount = GenerateRandomAmount()
            };

            //act
            var actionResult = await _transactionController.Deposit(request);
            var okResult = actionResult as OkObjectResult;

            //assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }


        [Fact]
        public async Task Withdrawal()
        {
            // arrange
            var request = new WithdrawalRequest()
            {
                accountIdentity = _accountIdentity,
                password = _password,
                Amount = GenerateRandomAmount()
            };

            //act
            var actionResult = await _transactionController.Withdrawal(request);
            var okResult = actionResult as OkObjectResult;

            //assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }


        [Fact]
        public async Task TransferMoney()
        {
            // arrange
            var request = new TransferMoneyRequest()
            {
                accountIdentity = _accountIdentity,
                password = _password,
                Amount = GenerateRandomAmount(),
                recipient = _recipient
            };

            //act
            var actionResult = await _transactionController.TransferMoney(request);
            var okResult = actionResult as OkObjectResult;

            //assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }



        private decimal GenerateRandomAmount()
        {
            var random = new Random();
            var number = (random.NextDouble() * (maxValue - minValue) + minValue);
            return (decimal)number; 
        }
    }
}