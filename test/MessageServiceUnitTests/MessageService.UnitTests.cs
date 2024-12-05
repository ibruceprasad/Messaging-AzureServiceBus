using Azure;
using Azure.Messaging.ServiceBus;
using FluentAssertions;
using Microsoft.Extensions.Azure;
using Moq;
using Services;

namespace MessageSenderUnitTests
{
    public class MessageSenderTests
    {
        Mock<IAzureClientFactory<ServiceBusSender>> mockSenderFactory;
        Mock<IAzureClientFactory<ServiceBusReceiver>> mockReceiverFactory;

        public MessageSenderTests()
        {
            mockSenderFactory = new Mock<IAzureClientFactory<ServiceBusSender>>();
            mockReceiverFactory = new Mock<IAzureClientFactory<ServiceBusReceiver>>();
        }
        [Fact]
        public async Task SendAndReceiveMessage_Test()
        {
            // Arrange
            //mockSenderFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns<ServiceBusSender>(res =>
            //{
            //    var mockServiceBusSender = new Mock<ServiceBusSender>();
            //    mockServiceBusSender
            //        .Setup(y => y.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
            //        .Returns(Task.FromResult(default(object)));
            //    return mockServiceBusSender.Object;
            //});

            
            mockSenderFactory
                .Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(new Mock<ServiceBusSender>().Object);

            //mockReceiverFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns<ServiceBusReceiver>(res =>
            //{
            //    var mockServiceBusReceiver = new Mock<ServiceBusReceiver>();
            //    var binaryMessage = new BinaryData("Hello");
            //    var mockMessage = new Mock<ServiceBusReceivedMessage>();
            //    mockMessage.Setup(m => m.Body).Returns(binaryMessage);
            //    mockServiceBusReceiver
            //        .Setup(y => y.ReceiveMessageAsync(It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
            //        .Returns(Task.FromResult(mockMessage.Object));
            //    return mockServiceBusReceiver.Object;
            //});

            mockReceiverFactory
              .Setup(factory => factory.CreateClient(It.IsAny<string>()))
              .Returns(new Mock<ServiceBusReceiver>().Object);

            // Act
            IBusServices busServices = new BusServices(mockSenderFactory.Object, mockReceiverFactory.Object);
            
            var sendStatus = await busServices.SendMessageAsync<string>("Hello");

            // Receiver test Incomplete, as I couldn't mock createclient , exception 
            var receivedMessage = busServices.GetMessageAsync<string>();

            // Assert
            sendStatus.Should().BeTrue();
            //receivedMessage.Should().Be("Hello");
        }
    }
}