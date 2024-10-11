using Application.Exceptions;
using Application.Shared;
using Application.Shared.ErrorsCodesDefinitions;
using Core.Data.Entities;
using Core.Shared.Configuration;
using Core.Shared.Extensions;
using Core.Shared.Services.Mailing;
using Core.V1.Account.Post.Reset.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Core.V1.Account.Post.Reset
{
    public class ResetPasswordRequestHandler : IRequestHandler<ResetPasswordRequest, EmptyResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMailingService _mailingService;
        private readonly FrontendOptions _frontendOptions;

        public ResetPasswordRequestHandler(
            UserManager<User> userManager,
            IMailingService mailingService,
            FrontendOptions frontendOptions)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mailingService = mailingService ?? throw new ArgumentNullException(nameof(mailingService));
            _frontendOptions = frontendOptions ?? throw new ArgumentNullException(nameof(frontendOptions));
        }

        public async Task<EmptyResponse> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new BusinessException(ErrorCodes.UserNotExist);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var escapedToken = token.ScapeTokenUrl();
            var resetUrl = ($"{request.GetBaseUrl()}{_frontendOptions.ForgotPassword}")
                .Replace("{token}", escapedToken)
                .Replace("{email}", request.Email);

            var model = MailModel.Create(
                "Recupero de contraseña",
                new ResetPasswordEmailDataModel(escapedToken, resetUrl));

            await _mailingService.SendEmailAsync(
                user.Email, model.Subject, await request.GetEmailBody(model));

            return new EmptyResponse();
        }
    }
}
