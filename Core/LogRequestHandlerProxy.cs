using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CommandLine.MediatR.DotNetCore.Core
{
    public class LogRequestHandlerProxy<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly TextWriter _textWriter;

        public LogRequestHandlerProxy(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _textWriter.WriteLine("Log before handler");
            var response = next();
            _textWriter.WriteLine("Log after handler");
            return response;
        }
    }
}
