
using FluentValidation;
using Naomi.promotion_service.Models.Request;

namespace Naomi.promotion_service.Models.Validation
{
    public class FindPromoShowRequestValidator : AbstractValidator<FindPromoShowRequest>
    {
        public FindPromoShowRequestValidator()
        {
            RuleFor(x => x.CompanyCode)
                .NotEmpty()
                .NotNull()
                .NotEqual("string", StringComparer.OrdinalIgnoreCase)
                .WithMessage("CompanyCode is required !!");

            RuleFor(x => x.PromotionApp)
                .NotEmpty()
                .NotNull()
                .NotEqual("string", StringComparer.OrdinalIgnoreCase)
                .WithMessage("PromotionApp is required !!");

            RuleFor(x => x.SiteCode)
                .NotEqual("string", StringComparer.OrdinalIgnoreCase)
                .WithMessage("SiteCode not valid !!");
        }
    }
}
