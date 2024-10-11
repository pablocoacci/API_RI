using Application.Exceptions;
using Application.Shared.ErrorsCodesDefinitions;
using Application.V1.Account.Post.Login.Models;
using Core.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Immutable;
using System.Security.Claims;

namespace Application.V1.Account.Post.Login
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginModel>
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public LoginRequestHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task<LoginModel> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Email);

            if (user == null)
                throw new BusinessException(ErrorCodes.InvalidUserNameOrPassword);

            if(!user.EmailConfirmed)
                throw new BusinessException(ErrorCodes.UserAccountNotConfirmed);

            var claims = new List<Claim>();
            claims.AddRange(await userManager.GetClaimsAsync(user));

            var roles = await userManager.GetRolesAsync(user);
            foreach (var roleId in roles)
            {
                var role = await roleManager.FindByIdAsync(roleId);
                claims.Add(new Claim(ClaimTypes.Role, role.Id));
                claims.AddRange(await roleManager.GetClaimsAsync(role));
            }

            var isValidPassword = await userManager.CheckPasswordAsync(user, request.Password);
            if (!isValidPassword)
            {
                await userManager.AccessFailedAsync(user);
                throw new BusinessException(ErrorCodes.InvalidUserNameOrPassword);
            }

            await userManager.ResetAccessFailedCountAsync(user);

            var response = new LoginModel(user.Id, claims.ToImmutableList());

            return response;
        }
    }
}
