using EVRental.BusinessObject.Shared.Models.QuanNH;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace EVRental.CheckOutQuanNhs.Microservices.QuanNH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutQuanNhController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IBus _bus;

        private List<CheckOutQuanNh> checkOutQuanNhs;
        public CheckOutQuanNhController(ILogger<CheckOutQuanNhController> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;

            checkOutQuanNhs = new List<CheckOutQuanNh>()
            {
                new CheckOutQuanNh
                {
                    CheckOutQuanNhid = 1,
                    CheckOutTime = DateTime.Now.AddHours(-5),
                    ReturnDate = DateOnly.FromDateTime(DateTime.Now),
                    ExtraCost = 15.50m,
                    TotalCost = 150.75m,
                    LateFee = 10.00m,
                    IsPaid = true,
                    IsDamageReported = false,
                    Notes = "No issues during rental period.",
                    CustomerFeedback = "Great service!",
                    PaymentMethod = "Credit Card",
                    StaffSignature = "John Doe",
                    CustomerSignature = "Jane Smith",
                    ReturnCondition = new ReturnCondition
                    {
                        ReturnConditionId = 5,
                        Name = "Good",
                        SeverityLevel = 1,
                        RepairCost = 0.00m,
                        IsResolved = true
                    }
                },
            };
        }

        [HttpGet]
        public IEnumerable<CheckOutQuanNh> Get()
        {
            return checkOutQuanNhs;
        }

        [HttpGet("{id}")]
        public CheckOutQuanNh Get(int id)
        {
            return checkOutQuanNhs.Find(c => c.CheckOutQuanNhid == id);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CheckOutQuanNh checkOutQuanNh)
        {
            Uri uri = new Uri("rabbitmq://localhost/CheckOutQuanNhQueue");
            var endpoint = await _bus.GetSendEndpoint(uri);
            await endpoint.Send(checkOutQuanNh);

            var checkOutJsonString = JsonSerializer.Serialize(checkOutQuanNh);
            string messageLog = string.Format("{0} *** PUBLISH *** data (order json string) into CheckOutQuanNhQueue on RabbitMQ", DateTime.Now, checkOutJsonString);
            _logger.LogInformation(messageLog);

            return Ok();
        }

        // PUT api/<CheckOutQuanNhController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CheckOutQuanNhController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
