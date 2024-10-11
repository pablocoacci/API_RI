using FluentValidation;

namespace Application.V1.Account.Post.Refresh
{
    public class CanExtendSessionRequestValidator : AbstractValidator<CanExtendSessionRequest>
    {
        public CanExtendSessionRequestValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .NotNull();
        }
    }
}
