using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console.Cli;
using ZenExtensions.Spectre.Console;

namespace ZenExtensions.CognitoOidc.Cli
{
    public class Startup : BaseStartup
    {
        public override void ConfigureCommands(in IConfigurator configurator)
        {
            configurator.AddCommand<GetClientCredentialsCommand>("client-credentials")
                .WithDescription("Get access token from cognito user pool");
            configurator.PropagateExceptions();
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
        {
            services.AddScoped<GetClientCredentialsCommandSettings>();
        }
    }
}