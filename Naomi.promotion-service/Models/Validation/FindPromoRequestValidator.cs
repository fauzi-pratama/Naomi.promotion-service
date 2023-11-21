
using FluentValidation;
using Naomi.promotion_service.Models.Contexts;
using Naomi.promotion_service.Models.Request;

namespace Naomi.promotion_service.Models.Validation
{
    public class FindPromoRequestValidator : AbstractValidator<FindPromoRequest>
    {
        public FindPromoRequestValidator(DataDbContext dataDbContext)
        {
            RuleFor(x => x.TransId)
                .NotEmpty()
                .NotNull()
                .WithMessage("TransId is null !!");

            RuleFor(x => x.SiteCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("SiteCode is null !!");

            RuleFor(x => x.CompanyCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("CompanyCode is null !!");

            RuleFor(x => x.TransDate)
                .NotEmpty()
                .NotNull()
                .WithMessage("TransDate is null !!");

            RuleFor(x => x.EntertaimentNip)
                .NotEmpty()
                .NotNull()
                .When(x => x.EntertaimentStatus)
                .WithMessage("EntertaimentNip is required, if EntertaimentStatus is True !!");

            RuleFor(x => x.EntertaimentOtp)
                .NotEmpty()
                .NotNull()
                .When(x => x.EntertaimentStatus)
                .WithMessage("EntertaimentOtp is required, if EntertaimentStatus is True !!");

        }
    }
}
