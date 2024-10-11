using Application.Shared;
using Core.Data.Repositories;
using Core.Shared.Services;
using MediatR;

namespace Application.V1.Account.Post.Refresh
{
    public class ExtendSessionRequestHandler : IRequestHandler<ExtendSessionRequest, EmptyResponse>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IDateTimeOffsetService _dateTimeOffsetService;

        public ExtendSessionRequestHandler(ISessionRepository sessionRepository, IDateTimeOffsetService dateTimeOffsetService)
        {
            _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
            _dateTimeOffsetService = dateTimeOffsetService ?? throw new ArgumentNullException(nameof(dateTimeOffsetService));
        }

        public async Task<EmptyResponse> Handle(ExtendSessionRequest request, CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.FindByIdAsync(request.OldRefreshToken);

            session.Token = request.AccessToken;
            session.ExpiresOn = _dateTimeOffsetService.UtcNow().Add(request.ValidFor);

            _sessionRepository.Update(session);

            return new EmptyResponse();
        }
    }
}
