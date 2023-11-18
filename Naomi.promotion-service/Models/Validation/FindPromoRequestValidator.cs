
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
                .MustAsync(async (TransId, cancellation) =>
                {
                    bool isCheckTransactionCommited = 
                        await dataDbContext.PromoTrans.AnyAsync(q => q.TransId!.ToUpper() == TransId!.ToUpper() && q.ActiveFlag, cancellationToken: cancellation);

                    return !isCheckTransactionCommited;
                })
                .WithMessage("TransId is null or already commited !!");

            RuleFor(x => x.SiteCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("SiteCode is null !!");

            RuleFor(x => x.CompanyCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("CompanyCode is null or not registered on table promo_workflow !!");

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
