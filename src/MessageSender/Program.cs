using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Azure;
using Services;

var builder = WebApplication.CreateBuilder(args);

var serviceBusConnection = builder.Configuration.GetConnectionString("serviceBusConnectionString");

var queue = "messaging_servicebus_queue";

builder.Services.AddAzureClients(clientBuilder =>
{ 
    clientBuilder.AddServiceBusClient(serviceBusConnection);

    clientBuilder.AddClient<ServiceBusSender, ServiceBusSenderOptions>((_, _, provider) =>
        provider.GetRequiredService<ServiceBusClient>()   
            .CreateSender(queue))
    .WithName("messaging-servicebus-sender");

    clientBuilder.AddClient<ServiceBusReceiver, ServiceBusClientOptions>(
            (_, _, provider) => provider.GetRequiredService<ServiceBusClient>()
                .CreateReceiver(queue))
    .WithName("messaging-servicebus-receiver");
});


// Add services to the container.
builder.Services.AddScoped<IBusServices, BusServices>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
