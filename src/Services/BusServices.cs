using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Services
{
    public class BusServices : IBusServices
    {
        private readonly string connection;
        private readonly ServiceBusSender _serviceBusSender;
        private readonly ServiceBusReceiver _serviceBusReceiver;
        public BusServices(IAzureClientFactory<ServiceBusSender> senderFactory, 
                            IAzureClientFactory<ServiceBusReceiver> receiverFactory)
        {
            _serviceBusSender = senderFactory.CreateClient("messaging-servicebus-sender");
            _serviceBusReceiver = receiverFactory.CreateClient("messaging-servicebus-sender");
        }

        public async Task<T?> GetMessageAsync<T>()
        {
            T? message;
            var data =  await _serviceBusReceiver.ReceiveMessageAsync(cancellationToken: new CancellationToken());
            if(data is not null)
            {
                byte[] bytes = data.Body.ToArray();
                var encoded =  Encoding.UTF8.GetString(bytes);
                message = JsonSerializer.Deserialize<T>(encoded);
                return message;
            }
            return default(T);
        }

        public async Task<bool> SendMessageAsync<T>(T message) where T : class
        { 
            var strMessage = JsonSerializer.Serialize<T>(message);
            await _serviceBusSender.SendMessageAsync(new ServiceBusMessage(strMessage));
            return true;
        }
    }
}