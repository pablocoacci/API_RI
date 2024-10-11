using Application.Shared;
using Core.Data.Entities;
using Core.Data.Repositories;
using Core.Shared.Services;
using MediatR;

namespace Application.V1.Account.Post.Login
{
    public class InitSessionRequestHandler : IRequestHandler<InitSessionRequest, EmptyResponse>
    {
        private readonly ISessionRepository sessionRepository;
        private readonly IDateTimeOffsetService dateTimeOffsetService;

        public InitSessionRequestHandler(
            ISessionRepository sessionRepository,
            IDateTimeOffsetService dateTimeOffsetService)
        {
            this.sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
            this.dateTimeOffsetService = dateTimeOffsetService ?? throw new ArgumentNullException(nameof(dateTimeOffsetService));
        }

        public Task<EmptyResponse> Handle(InitSessionRequest request, CancellationToken cancellationToken)
        {
            var session = new Session(
                request.RefreshToken,
                request.UserId,
                request.Token,
                expiresOn: dateTimeOffsetService.UtcNow().Add(request.ValidFor),
                createdOn: dateTimeOffsetService.UtcNow());

            sessionRepository.Add(session);

            return Task.FromResult(new EmptyResponse());
        }
    }
}
