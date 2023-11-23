
using FluentValidation;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Response;

namespace Naomi.promotion_service.Models.Validation
{
    public class CommitRequestValidator : AbstractValidator<CommitRequest>
    {
        public CommitRequestValidator()
        {
            RuleFor(x => x.TransId)
            .NotEmpty()
            .NotNull()
            .WithMessage("TransId is null !!");

            RuleFor(x => x.CompanyCode)
            .NotEmpty()
            .NotNull()
            .WithMessage("CompanyCode is null !!");
        }

    }
}
