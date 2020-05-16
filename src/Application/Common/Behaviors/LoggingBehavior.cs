using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MediatR.Pipeline;

namespace TagDossier.Application.Common.Behaviors
{
    // public class LoggingBehavior<TRequest>
    //     : IRequestPreProcessor<TRequest>
    // {
    //     private readonly ILogger<TRequest> _logger;
    //     private readonly ICurrentUserService _currentUserService;
    //
    //     public LoggingBehavior(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    //         => (_logger, _currentUserService) = (logger, currentUserService);
    //
    //     public async Task Process(TRequest request, CancellationToken cancellationToken)
    //     {
    //         var requestName = typeof(TRequest).Name;
    //         var userId = _currentUserService.User?.Id;
    //         
    //         _logger.LogInformation("Request: {Name} {@UserId} {@Request}",
    //             requestName, userId, request);
    //     }
    // }
}