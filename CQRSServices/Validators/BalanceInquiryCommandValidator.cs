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
    public class BalanceInquiryCommandValidator : AbstractValidator<BalanceInquiryCommand>
    {
        public BalanceInquiryCommandValidator() {
            try
            {
                RuleFor(s => s._balanceInquiryRequest.password)
                    .NotNull().WithMessage("Please enter a password!")
                    .NotEmpty().WithMessage("Please enter a password!");


                RuleFor(s => s._balanceInquiryRequest.accountIdentity)
                    .SetValidator(new AccountIdentityValidator());

            }
            catch (Exception e) {
                throw e;
            }


        }
    }
}
