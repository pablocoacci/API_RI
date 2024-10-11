using Application.Exceptions;
using Application.Shared;
using Application.Shared.ErrorsCodesDefinitions;
using Application.V1.Account.Post.RegisterTwoSteps.Models;
using Core.Data.Entities;
using Core.Shared.Configuration;
using Core.Shared.Extensions;
using Core.Shared.Services.Mailing;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.V1.Account.Post.RegisterTwoSteps
{
    public class RegisterTwoStepsRequestHandler : IRequestHandler<RegisterTwoStepsRequest, EmptyResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMailingService _mailingService;
        private readonly FrontendOptions _frontendOptions;

        public RegisterTwoStepsRequestHandler(
            UserManager<User> userManager, 
            IMailingService mailingService, 
            FrontendOptions frontendOptions)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mailingService = mailingService ?? throw new ArgumentNullException(nameof(mailingService));
            _frontendOptions = frontendOptions ?? throw new ArgumentNullException(nameof(frontendOptions));
        }

        public async Task<EmptyResponse> Handle(RegisterTwoStepsRequest request, CancellationToken cancellationToken)
        {
            var user = new User(request.Email, request.FirstName, request.LastName)
            {
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new BusinessException(ErrorCodes.UserNotCreated);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var escapedToken = token.ScapeTokenUrl();
            var confirmUrl = ($"{request.GetBaseUrl()}{_frontendOptions.ConfirmAccount}")
                .Replace("{Token}", escapedToken)
                .Replace("{Email}", request.Email);

            var model = MailModel.Create(
                "Confirmacion de Email",
                new EmailVerificationModelData(escapedToken, user.Email, confirmUrl));

            await _mailingService.SendEmailAsync(
                user.Email, model.Subject, await request.GetEmailBody(model));

            return new EmptyResponse();
        }
    }
}
