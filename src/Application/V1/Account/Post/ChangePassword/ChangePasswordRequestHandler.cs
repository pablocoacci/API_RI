using Application.Exceptions;
using Application.Shared;
using Application.Shared.ErrorsCodesDefinitions;
using Core.Data.Entities;
using Core.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.V1.Account.Post.ChangePassword
{
    public class ChangePasswordRequestHandler : IRequestHandler<ChangePasswordRequest, EmptyResponse>
    {
        private readonly UserManager<User> _userManager;

        public ChangePasswordRequestHandler(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<EmptyResponse> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new BusinessException(ErrorCodes.UserNotExist);

            var isSamePassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (isSamePassword)
                throw new BusinessException(ErrorCodes.NewPasswordIsSameOld);

            var originalToken = request.Token.UnScapeTokenUrl();
            var result = await _userManager.ResetPasswordAsync(user, originalToken, request.Password);
            if (!result.Succeeded)
                throw new BusinessException(ErrorCodes.CanNotResetPassword);

            return new EmptyResponse();
        }
    }
}
