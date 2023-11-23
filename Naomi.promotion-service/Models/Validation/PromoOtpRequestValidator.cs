
using FluentValidation;
using Naomi.promotion_service.Models.Request;

namespace Naomi.promotion_service.Models.Validation
{
    public class PromoOtpRequestValidator : AbstractValidator<PromoOtpRequest>
    {
        public PromoOtpRequestValidator()
        {
            RuleFor(x => x.CompanyCode)
           .NotEmpty()
           .NotNull()
           .WithMessage("CompanyCode is null !!");

            RuleFor(x => x.Nip)
            .NotEmpty()
            .NotNull()
            .WithMessage("Nip is null !!");
        }
    }
}
