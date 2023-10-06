using CQRSServices.Commands;
using CQRSServices.Helpers;
using CQRSServices.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSServices.Validators
{
    public class TransferMoneyCommandValidator : AbstractValidator<TransferMoneyCommand>
    {
        public TransferMoneyCommandValidator() {
            try
            {
                RuleFor(s => s._transferMoneyRequest.password)
                    .NotNull().WithMessage("Please enter a password!")
                    .NotEmpty().WithMessage("Please enter a password!");


                RuleFor(s => s._transferMoneyRequest.accountIdentity)
                    .SetValidator(new AccountIdentityValidator());


                RuleFor(s => s._transferMoneyRequest.recipient)
                  .NotNull().WithMessage("Please enter your recipient!")
                  .NotEmpty().WithMessage("Please enter your recipient!");


                RuleFor(s => s._transferMoneyRequest.Amount)
                     .SetValidator(new NumericValidator());

            }
            catch (Exception e) {
                throw e;
            }


        }
    }
}
