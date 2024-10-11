using Application.Exceptions;
using Application.Shared.ErrorsCodesDefinitions;
using Application.V1.Account.Get.Confirm.Models;
using Core.Data.Entities;
using Core.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.V1.Account.Get.Confirm
{
    public class ConfirmAccountRequestHandler : IRequestHandler<ConfirmAccountRequest, AccountConfirmResponseModel>
    {
        private readonly UserManager<User> _userManager;

        public ConfirmAccountRequestHandler(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<AccountConfirmResponseModel> Handle(ConfirmAccountRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new BusinessException(ErrorCodes.UserNotExist);

            var originalToken = request.Token.UnScapeTokenUrl();
            user.EmailConfirmed = true;
            var result = await _userManager.ConfirmEmailAsync(user, originalToken);

            if (!result.Succeeded)
                throw new BusinessException(ErrorCodes.CanNotConfirmAccount);

            return new AccountConfirmResponseModel() { Result = "El usuario se ha confirmado con exito "};
        }
    }
}
