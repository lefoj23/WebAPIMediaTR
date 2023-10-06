using CQRSServices.Commands;
using CQRSServices.Requests;
using CQRSServices.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        public readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentException(); ;
        }

   
        [HttpGet("balance-inquiry")]
        public async Task<IActionResult> BalanceInquiry([FromQuery]BalanceInquiryRequest request)
        {
            try
            {
                var command = new BalanceInquiryCommand(request);
                var resp = await _mediator.Send(command).ConfigureAwait(false);

                return Ok(resp);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositRequest request)
        {
            try
            {
                var command = new DepositCommand(request);
                var resp = await _mediator.Send(command).ConfigureAwait(false);

                return Ok(resp);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("withdrawal")]
        public async Task<IActionResult> Withdrawal([FromBody] WithdrawalRequest request)
        {
            try
            {
                var command = new WithdrawalCommand(request);
                var resp = await _mediator.Send(command).ConfigureAwait(false);

                return Ok(resp);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("transfer-money")]
        public async Task<IActionResult> TransferMoney([FromBody] TransferMoneyRequest request)
        {
            try
            {
                var command = new TransferMoneyCommand(request);
                var resp = await _mediator.Send(command).ConfigureAwait(false);

                return Ok(resp);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
