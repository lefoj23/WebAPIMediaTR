using CQRSServices.Commands;
using CQRSServices.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        public readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentException(); ;
        }



        /// <summary>
        /// Registration 
        /// </summary>     
        /// <param name="request">
        /// AccountIdentity can be (Email/ContactNumber) 
        /// </param>
        /// <returns></returns>
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest request)
        {
            try
            {
                var command = new RegistrationCommand(request);
                var resp = await _mediator.Send(command).ConfigureAwait(false);

                if (!resp.Status)
                    return BadRequest(resp.Message);

                return Ok(resp);
            }
            catch (Exception)
            {
                throw;
            }          
        }

       
    }
}
