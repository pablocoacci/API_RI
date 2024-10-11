using FluentValidation;

namespace Application.V1.Account.Get.Confirm
{
    public class ConfirmAccountRequestValidator : AbstractValidator<ConfirmAccountRequest>
    {
        public ConfirmAccountRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(x => x.Token)
                .NotEmpty();
        }
    }
}
