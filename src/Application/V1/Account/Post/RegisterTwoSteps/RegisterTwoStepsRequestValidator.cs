using FluentValidation;

namespace Application.V1.Account.Post.RegisterTwoSteps
{
    internal class RegisterTwoStepsRequestValidator : AbstractValidator<RegisterTwoStepsRequest>
    {
        public RegisterTwoStepsRequestValidator()
        {
            RuleFor(m => m.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(m => m.FirstName)
                .NotEmpty();

            RuleFor(m => m.LastName)
                .NotEmpty();

            RuleFor(m => m.Password)
                .NotEmpty()
                .Equal(x => x.PasswordRepeat);

            RuleFor(m => m.PasswordRepeat)
                .NotEmpty()
                .Equal(x => x.Password);
        }
    }
}
