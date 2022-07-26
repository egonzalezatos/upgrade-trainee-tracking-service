using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace Upgrade.TraineeTracking.Grpc.Interceptors
{
    public class LoggingInterceptor : Interceptor
    {
        private readonly ILogger _logger;

        public LoggingInterceptor(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LoggingInterceptor>();
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var template = $"Starting call. Type: {context.Method.Type}. " +
                           $"Method: {context.Method.Name}.";
            _logger.LogInformation(template);
            return continuation(request, context);
        }
    }
}