using Application.Shared;
using Application.Shared.ErrorsCodesDefinitions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Helpers.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Presentation.API.Controllers.Api.V1
{
    [SwaggerResponse(400, "Bad Request", Type = typeof(ErrorBadRequestModel))]
    [SwaggerResponse(401, "Unanuthorized")]
    [SwaggerResponse(403, "forbidden")]
    [SwaggerResponse(500, "Internal Error", Type = typeof(ErrorResponseModel))]
    //[Authorize]
    public class BaseController : Controller
    {
        protected readonly IMediator _mediator;
        private readonly IServiceProvider _serviceProvider;

        public BaseController(IMediator mediator, IServiceProvider serviceProvider)
        {
            _mediator = mediator;
            _serviceProvider = serviceProvider;
        }

        protected StatusCodeResult OkNoContent()
            => StatusCode((int)HttpStatusCode.NoContent);

        protected new OkObjectResult Ok(object value)
            => base.Ok(new HttpSuccess(value));

        protected ObjectResult BadRequestContent(object value)
            => StatusCode(400, value);

        protected ErrorBadRequestModel ValidateRequest<T>(T request)
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();

            if (validator == null)
                return new ErrorBadRequestModel();

            var result = validator.Validate(request);
            return new ErrorBadRequestModel(result);
        }

        protected IActionResult GetResult<T>(T result)
            where T : ResponseBaseModel
        {
            if (result.IsError())
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ErrorResponseModel((int)result.GetErrorCode(), ErrorDiccionary.GetErrorDescription(result.GetErrorCode())));

            return StatusCode(200, result);
        }

        protected async Task<IActionResult> ExecuteRequest<T, TR>(T req) 
            where T : LoggedRequest<TR>
            where TR : ResponseBaseModel
        {
            var resultValidation = ValidateRequest(req);

            if (!resultValidation.IsValid)
                return BadRequestContent(resultValidation);

            var response = await _mediator.Send(req);
            return GetResult(response);
        }
    }
}
