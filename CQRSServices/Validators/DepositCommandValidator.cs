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
    public class DepositCommandValidator : AbstractValidator<DepositCommand>
    {
        public DepositCommandValidator() {
            try
            {
                RuleFor(s => s._depositRequest.password)
                    .NotNull().WithMessage("Please enter a password!")
                    .NotEmpty().WithMessage("Please enter a password!");


                RuleFor(s => s._depositRequest.accountIdentity)
                    .SetValidator(new AccountIdentityValidator());


                RuleFor(s => s._depositRequest.Amount)
                     .SetValidator(new NumericValidator());

            }
            catch (Exception e) {
                throw e;
            }


        }
    }
}
