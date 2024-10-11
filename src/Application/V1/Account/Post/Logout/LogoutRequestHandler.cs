using Application.Shared;
using Core.Data.Repositories;
using MediatR;

namespace Application.V1.Account.Post.Logout
{
    public class LogoutRequestHandler : IRequestHandler<LogoutRequest, EmptyResponse>
    {
        private readonly ISessionRepository _sessionRepository;

        public LogoutRequestHandler(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
        }

        public async Task<EmptyResponse> Handle(LogoutRequest request, CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.FindByUserIdAndToken(request.UserId, request.AccessToken);

            _sessionRepository.Delete(session);

            return new EmptyResponse();
        }
    }
}
