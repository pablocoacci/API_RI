using Core.Data.Repositories;
using Core.Shared.Services;
using Application.V1.Account.Post.Refresh.Models;
using MediatR;

namespace Application.V1.Account.Post.Refresh
{
    public class CanExtendSessionRequestHandler : IRequestHandler<CanExtendSessionRequest, CanExtendSessionResultModel>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IDateTimeOffsetService _dateTimeOffsetService;

        public CanExtendSessionRequestHandler(ISessionRepository sessionRepository, IDateTimeOffsetService dateTimeOffsetService)
        {
            _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
            _dateTimeOffsetService = dateTimeOffsetService ?? throw new ArgumentNullException(nameof(dateTimeOffsetService));
        }

        public async Task<CanExtendSessionResultModel> Handle(CanExtendSessionRequest request, CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.FindByIdAsync(request.RefreshToken);

            return new CanExtendSessionResultModel(
                isValid: session != null && session.Active && session.ExpiresOn > _dateTimeOffsetService.UtcNow());
        }
    }
}
