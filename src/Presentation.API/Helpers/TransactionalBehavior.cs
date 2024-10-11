using Application.Exceptions;
using Application.Shared;
using Application.Shared.ErrorsCodesDefinitions;
using Core.Data.EF;
using MediatR;
using ILogger = Serilog.ILogger;

namespace Presentation.API.Helpers
{
    public class TransactionalBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequesDecorator<TResponse>
        where TResponse : ResponseBaseModel
    {
        private readonly ILogger _logger;
        private readonly DataContext _dataContext;
        public TransactionalBehavior(ILogger logger, DataContext dataContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(_dataContext));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                var response = await next();
                
                if(request.SaveChanges())
                    await _dataContext.SaveChangesAsync();
                
                return response;
            }
            catch (BusinessException ex)
            {
                _logger.Error(ex, ex.Message);
                return CreateEmptyResponse(ex.ErrorCode);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return CreateEmptyResponse(ErrorCodes.NotDefined);
            }
        }

        private TResponse CreateEmptyResponse(ErrorCodes code)
        {
            var response = Activator.CreateInstance<TResponse>();
            response.SetErrorCode(code);
            return response;
        }
    }
}
