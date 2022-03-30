using System;
using System.Linq;
using CommandLine.MediatR.DotNetCore.Handlers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommandLine.MediatR.DotNetCore
{
    class Program
    {
        static void Main(string[] args)
        {            
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                                    .AddJsonFile("./config/appsettings.json")
                                    .AddJsonFile($"./config/appsettings.{environmentName}.json", true)
                                    .AddEnvironmentVariables()
                                    .Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(context => System.Console.Out)
                .AddMediatR(typeof(Program))  // auto register every IRequest and RequestHandler
                .BuildServiceProvider();

            var allTheCommands = (typeof(Program).Assembly.GetTypes().Where(type => (typeof(IRequest).IsAssignableFrom(type)))).ToArray();

            var parser = new Parser(with =>
            {
                with.EnableDashDash = true;
                with.CaseSensitive = false;
                with.HelpWriter = System.Console.Out;
            });

            var result = parser.ParseArguments(args, allTheCommands) as Parsed<object>;
            if (result != null)
            {
                serviceProvider.GetService<IMediator>().Send((IRequest)result.Value);
            }
            else
            {
                Environment.Exit(1229);
            }

#if DEBUG
            System.Console.ReadKey();
#endif

        }
    }
}
