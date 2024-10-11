using Application.Exceptions;
using Application.Shared;
using Application.Shared.ErrorsCodesDefinitions;
using Core.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.V1.Account.Post.Register
{
    public class RegisterRequestHandler : IRequestHandler<RegisterRequest, EmptyResponse>
    {
        private readonly UserManager<User> _userManager;

        public RegisterRequestHandler(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<EmptyResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var user = new User(request.Email, request.FirstName, request.LastName)
            {
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new BusinessException(ErrorCodes.UserNotCreated);

            return new EmptyResponse();
        }
    }
}
