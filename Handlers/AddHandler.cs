using System.IO;
using CommandLine.MediatR.DotNetCore.Verbs;
using MediatR;

namespace CommandLine.MediatR.DotNetCore.Handlers
{
    class AddHandler : RequestHandler<AddVerb>
    {
        private readonly TextWriter _textWriter;

        public AddHandler(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        protected override void Handle(AddVerb message)
        {
            _textWriter.WriteLine("added {0}. Forced: {1}. Patch {2}", message.FileName, message.Force, message.Patch);
        }
    }
}