using MessageReceiver.Job;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
 


var builder = Host.CreateApplicationBuilder();

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var connectionString = configuration.GetConnectionString("serviceBusConnectionString");

builder.Services.AddScoped<IBusServices, BusServices>();
builder.Services.AddHostedService<ReceiverWorker>();
       







/*
var host = new HostBuilder()
        .ConfigureHostConfiguration(configHost => {
           
        })
        .ConfigureServices((hostContext, services) => {
            services.AddScoped<IBusServices, BusServices>(serviceProvider => new BusServices(connectionString));
            services.AddHostedService<ReceiverWorker>();
        })
       .UseConsoleLifetime()
       .Build();
host.Run();
*/


/*
Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddHostedService<BackgroundWorker>();
    })
    .Build()
    .Run();
*/

 


 

