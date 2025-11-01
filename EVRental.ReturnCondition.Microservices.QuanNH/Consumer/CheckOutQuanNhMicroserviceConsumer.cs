using EVRental.BusinessObject.Shared.Models.QuanNH;
using MassTransit;
using System.Text.Json;

namespace EVRental.ReturnCondition.Microservices.QuanNH.Consumer
{
    public class CheckOutQuanNhMicroserviceConsumer : IConsumer<CheckOutQuanNh>
    {
        private readonly ILogger _logger;
        public CheckOutQuanNhMicroserviceConsumer(ILogger<CheckOutQuanNhMicroserviceConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<CheckOutQuanNh> context)
        {
            var receiveData = context.Message;

            var receiveDataJsonString = JsonSerializer.Serialize(receiveData);
            var messageLog = string.Format("{0} *** RECEIVE *** data from CheckOutQuanNhQueue on RabbitMQ", DateTime.Now, receiveDataJsonString);
            _logger.LogInformation(messageLog);

        }
    }
}
