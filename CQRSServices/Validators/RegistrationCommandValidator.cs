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
    public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
    {
        public RegistrationCommandValidator() {
            try
            {
                RuleFor(s => s._registrationRequest.password)
                    .NotNull().WithMessage("Please enter a password!")
                    .NotEmpty().WithMessage("Please enter a password!")
                    .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                    .Must(m => m.containsOneUppercase()).WithMessage("Password must contain at least one uppercase letter.")
                    .Must(m => m.containsOneLowercase()).WithMessage("Password must contain at least one lowercase letter.")
                    .Must(m => m.containsDigit()).WithMessage("Password must contain at least one digit.");


                RuleFor(s => s._registrationRequest.accountIdentity)
                    .SetValidator(new AccountIdentityValidator());

                RuleFor(s => s._registrationRequest.firstName)
                    .NotNull().WithMessage("Please enter your First Name!")
                    .NotEmpty().WithMessage("Please enter your First Name!");

                RuleFor(s => s._registrationRequest.lastName)
                    .NotNull().WithMessage("Please enter your Last Name!")
                    .NotEmpty().WithMessage("Please enter your Last Name!");


            }
            catch (Exception e) {
                throw e;
            }


        }
    }
}
