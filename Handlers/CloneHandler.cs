using System.IO;
using CommandLine.MediatR.DotNetCore.Verbs;
using MediatR;

namespace CommandLine.MediatR.DotNetCore.Handlers
{
    class CloneHandler : RequestHandler<AddVerb>
    {
        private readonly TextWriter _textWriter;

        public CloneHandler(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        protected override void Handle(AddVerb message)
        {
            _textWriter.WriteLine("cloned {0}", message.FileName);
        }
    }
}