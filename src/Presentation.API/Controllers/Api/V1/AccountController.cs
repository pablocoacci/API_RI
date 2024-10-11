using Core.Shared;
using Application.V1.Account.Get.Confirm;
using Application.V1.Account.Post.ChangePassword;
using Application.V1.Account.Post.Login;
using Application.V1.Account.Post.Login.Models;
using Application.V1.Account.Post.Logout;
using Application.V1.Account.Post.Refresh;
using Application.V1.Account.Post.Register;
using Core.V1.Account.Post.Reset;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Presentation.API.Auth.Jwt;
using Presentation.API.Helpers.Extensions;
using Swashbuckle.AspNetCore.Annotations;
using Application.V1.Account.Post.RegisterTwoSteps;
using Application.V1.Account.Get.Confirm.Models;
using Application.Shared;

namespace Presentation.API.Controllers.Api.V1
{
    [Route("v{version:apiVersion}/account")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly IOptions<JwtIssuerOptions> _jwtOptions;

        public AccountController(
            IMediator mediator, 
            IJwtFactory jwtFactory, 
            IOptions<JwtIssuerOptions> jwtOptions,
            IServiceProvider serviceProvider) : base(mediator, serviceProvider)
        {
            _jwtFactory = jwtFactory ?? throw new ArgumentNullException(nameof(jwtFactory));
            _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
        }

        /// <remarks>
        /// La cuenta se crea ya confirmada lista para usarse. 
        /// Comentarios:
        /// 
        ///     - email: el email del usuario utilizado para loguearse en el sistema
        ///     - firstName: primer nombre del usuario
        ///     - lastName: apellido del usuario
        ///     - password: password que se utilizara en la cuenta.
        ///     - passwordRepeat: password que se utilizara en la cuenta. Debe coincidir con el campo password
        ///     
        /// </remarks>
        [SwaggerOperation("Creacion de un nuevo usuario. Se crea con el email ya confirmado")]
        [SwaggerResponse(200, "Ok", Type = typeof(EmptyResponse))]
        [Authorize]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
            => await ExecuteRequest<RegisterRequest, EmptyResponse>(request);

        /// <remarks>
        /// La cuenta se crea, pero adicionalmente se envia un mail para que el usuario confirme la cuenta.
        /// Comentarios:
        /// 
        ///     - email: el email del usuario utilizado para loguearse en el sistema
        ///     - firstName: primer nombre del usuario
        ///     - lastName: apellido del usuario
        ///     - password: password que se utilizara en la cuenta.
        ///     - passwordRepeat: password que se utilizara en la cuenta. Debe coincidir con el campo password
        ///     
        /// </remarks>
        [SwaggerOperation("Creacion de un nuevo usuario. Se crea la cuenta pero sin confirmar")]
        [SwaggerResponse(200, "Ok", Type = typeof(EmptyResponse))]
        [AllowAnonymous]
        [HttpPost("registerTwoSteps")]
        public async Task<IActionResult> RegisterTwoSteps([FromBody] RegisterTwoStepsRequest request)
        {
            request.SetBaseUrl($"{Request.Scheme}://{Request.Host}");
            request.SetEmailBodyBuilder(async (model) => await this.RenderViewAsync("EmailVerification", model));
            return await ExecuteRequest<RegisterTwoStepsRequest, EmptyResponse>(request);
        }

        /// <remarks>
        /// Comentarios:
        /// 
        ///     - email: el email del usuario utilizado para loguearse en el sistema
        ///     - token: teken generado por el sistema para la confirmacion de la cuenta
        ///     
        /// </remarks>
        [SwaggerOperation("Confirmacion de una cuenta de usuario")]
        [SwaggerResponse(200, "Ok", Type = typeof(AccountConfirmResponseModel))]
        [AllowAnonymous]
        [HttpGet("confirm")]
        public async Task<IActionResult> Confirm([FromQuery] ConfirmAccountRequest request)
            => await ExecuteRequest<ConfirmAccountRequest, AccountConfirmResponseModel>(request);

        /// <remarks>
        /// Comentarios:
        /// 
        ///     - email: mail del usuario para hacer login
        ///     - password: password del usuario
        ///     
        /// </remarks>
        [SwaggerOperation("Genera un token para la utlizacion de la API")]
        [SwaggerResponse(200, "Ok", Type = typeof(LoginResult))]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var resultValidation = ValidateRequest(request);

            if (!resultValidation.IsValid)
                return BadRequestContent(resultValidation);

            // Verify that the user & pass are on db and match
            var resultLogin = await _mediator.Send(request);
            
            if(resultLogin.IsError())
                return GetResult(resultLogin);

            // Create an identity for the user
            var identity = _jwtFactory.GenerateClaimsIdentity(request.Email, resultLogin.Id, resultLogin.Claims);

            // With the identity generate a jwt and a refresh token
            var accessToken = await _jwtFactory.GenerateEncodedTokenAsync(request.Email, identity);
            var refreshToken = _jwtFactory.GenerateRefreshToken();
            
            // Save tokens into db
            var resultSesion = await _mediator.Send(
                new InitSessionRequest(resultLogin.Id, accessToken, refreshToken, _jwtOptions.Value.RefreshTokenValidFor));

            if(resultSesion.IsError())
                return GetResult(resultSesion);

            // Reply with both tokens
            return Ok(new LoginResult(accessToken, refreshToken));
        }

        /// <remarks>
        /// Comentarios:
        /// 
        ///     - refreshToken: refresh token otorgado por el sistema en el momento de login
        ///     
        /// </remarks>
        [SwaggerOperation("Extiende la duracion de la sesion de un usuario previamente logueado")]
        [SwaggerResponse(200, "Ok", Type = typeof(LoginResult))]
        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] CanExtendSessionRequest request)
        {
            var resultValidation = ValidateRequest(request);

            if (!resultValidation.IsValid)
                return BadRequestContent(resultValidation);

            var resultCanExtendReq = await _mediator.Send(request);
            
            if(resultCanExtendReq.IsError())
                return GetResult(resultCanExtendReq);
            
            if (!resultCanExtendReq.IsValid)
                return Unauthorized();

            // Get the identity that is within the jwt
            var identity = _jwtFactory.GetPrincipalFromToken(
                token: Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "") ?? "", allowExpired: true);

            // With the identity generate a new jwt and refresh token
            var accessToken = await _jwtFactory.GenerateEncodedTokenAsync(identity.UserName(), identity.Identities.First());

            var resultExtenSession = await _mediator.Send(
                new ExtendSessionRequest(identity.Id(), request.RefreshToken, accessToken, _jwtOptions.Value.RefreshTokenValidFor));

            if(resultExtenSession.IsError())
                return GetResult(resultExtenSession);

            // Reply with both new tokens
            return Ok(new LoginResult(accessToken, request.RefreshToken));
        }

        [SwaggerOperation("Cierra la sesion de un usuario logueado previamente")]
        [SwaggerResponse(200, "Ok", Type = typeof(EmptyResponse))]
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "") ?? "";

            // Delete session with userId and jwt
            var result = await _mediator.Send(new LogoutRequest(User.Id(), token));
            
            return GetResult(result);
        }

        /// <remarks>
        /// Comentarios:
        /// 
        ///     - email: email del usuario que desea resetear el password
        ///     - redirectUrl: url donde dirigira el sistema para cambiar la password
        ///     
        /// </remarks>
        [SwaggerOperation("Permite resetear el passoword de un usuario registrado mediante el envio de un mail")]
        [SwaggerResponse(200, "Ok", Type = typeof(EmptyResponse))]
        [AllowAnonymous]
        [HttpPost("reset")]
        public async Task<IActionResult> Reset([FromBody] ResetPasswordRequest request)
        {
            request.SetBaseUrl($"{Request.Scheme}://{Request.Host}");
            request.SetEmailBodyBuilder(async (model) => await this.RenderViewAsync("ResetPassword", model));
            return await ExecuteRequest<ResetPasswordRequest, EmptyResponse>(request);
        }

        /// <remarks>
        /// Comentarios:
        /// 
        ///     - token: token generado por el sistema
        ///     - email: email del usuario que desea resetear el password
        ///     - password: nueva password de la cuenta
        ///     - passwordRepeat: nueva password de la cuenta. Debe coincidir con el parametro password
        ///     
        /// </remarks>
        [SwaggerOperation("Permite resetear el passoword de un usuario registrado")]
        [SwaggerResponse(200, "Ok", Type = typeof(EmptyResponse))]
        [AllowAnonymous]
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
            => await ExecuteRequest<ChangePasswordRequest, EmptyResponse>(request);
    }
}
