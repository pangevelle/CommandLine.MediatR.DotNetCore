using System.IO;
using CommandLine.MediatR.DotNetCore.Verbs;
using MediatR;

namespace CommandLine.MediatR.DotNetCore.Handlers
{
    class CommitHandler : RequestHandler<AddVerb>
    {
        private readonly TextWriter _textWriter;

        public CommitHandler(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        protected override void Handle(AddVerb message)
        {
            _textWriter.WriteLine("committed {0}", message.FileName);
        }
    }
}