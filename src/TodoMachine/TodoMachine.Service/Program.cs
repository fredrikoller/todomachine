using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using TodoMachine.Components.Consumers;
using TodoMachine.Services;

var isService = !(Debugger.IsAttached || args.Contains("--console"));

var builder = new HostBuilder()
    .ConfigureAppConfiguration((hostingContext, config) => 
    {
        config.AddJsonFile("appsettings.json", true);
        config.AddEnvironmentVariables();
        
        if (args != null)
        {
            config.AddCommandLine(args);
        }
    })
    .ConfigureServices((hostContext, services) => 
    {
        services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
        services.AddMassTransit(cfg => 
        {
            cfg.AddConsumersFromNamespaceContaining<SubmitTodoConsumer>();
            cfg.AddBus(ConfigureBus);
        });

        services.AddHostedService<MassTransitConsoleHostedService>();
    })
    .ConfigureLogging((hostContext, logging) => 
    {
        logging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
    });


await builder.RunConsoleAsync();
//builder.Build();

static IBusControl ConfigureBus(IServiceProvider provider)
{
    return Bus.Factory.CreateUsingRabbitMq(cfg => 
    {
        cfg.ConfigureEndpoints((IBusRegistrationContext)provider);
    });
}