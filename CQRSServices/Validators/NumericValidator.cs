using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSServices.Validators
{
    class NumericValidator : AbstractValidator<decimal>
    {
        public NumericValidator()
        {
            var message = "Please enter a valid amount!";

            RuleFor(s => s)
                .NotNull().WithMessage(message)
                .NotEmpty().WithMessage(message)
                .Must(x => x > 0).WithMessage(message);
        }
    }
}
