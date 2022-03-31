using MediatR;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CommandLine.MediatR.DotNetCore.Core
{
    public class TimeRequestHandlerProxy<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly TextWriter _textWriter;

        public TimeRequestHandlerProxy(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var response = next();

            stopwatch.Stop();
            _textWriter.WriteLine("Elapsed Time in handler: {0} ms", stopwatch.ElapsedMilliseconds);
            return response;
        }
    }
}
