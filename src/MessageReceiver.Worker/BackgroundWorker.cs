
using MessageSender.Models;
using Microsoft.Extensions.Hosting;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageReceiver.Job
{
    public class ReceiverWorker : BackgroundService
    {
        private readonly IBusServices _busServices;
        private const string QueueName = "messaging_servicebus_queue";
        public ReceiverWorker(IBusServices busServices)
        {
            _busServices = busServices;
        }
        protected  async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new(TimeSpan.FromSeconds(20));

            do
            {
                var message = await _busServices.GetMessageAsync<GreetingMessage>();
                Console.WriteLine($"Received Message : {message.Email} {message.Message}");
            } while ( await timer.WaitForNextTickAsync(stoppingToken));

            return;
            //throw new NotImplementedException();
        }
 
    }
}
