using FluentValidation;

namespace Application.V1.Account.Post.Register
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
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
