using CQRSServices.Helpers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSServices.Validators
{
    public class AccountIdentityValidator : AbstractValidator<string>
    {
        public AccountIdentityValidator() {

            var message = "Invalid Account Identity! Please enter valid Email or Contact Number.";
            RuleFor(s => s)
                .NotNull().WithMessage(message)
                .NotEmpty().WithMessage(message)
                .Must(x => x.isAccIdentityValid() ).WithMessage(message);

        }
    }
}
