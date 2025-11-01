using EVRental.BusinessObject.Shared.Models.QuanNH;
using Microsoft.AspNetCore.Mvc;

namespace EVRental.ReturnCondition.Microservices.QuanNH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnConditionController : ControllerBase
    {
        private List<EVRental.BusinessObject.Shared.Models.QuanNH.ReturnCondition> returnConditions;
        public ReturnConditionController()
        {
            returnConditions = new List<EVRental.BusinessObject.Shared.Models.QuanNH.ReturnCondition>()
            {
                new EVRental.BusinessObject.Shared.Models.QuanNH.ReturnCondition
                {
                    ReturnConditionId = 6,
                    Name = "Excellent",
                    SeverityLevel = 0,
                    RepairCost = 0.00m,
                    IsResolved = true
                },
                new EVRental.BusinessObject.Shared.Models.QuanNH.ReturnCondition
                {
                    ReturnConditionId = 7,
                    Name = "Good",
                    SeverityLevel = 1,
                    RepairCost = 50.00m,
                    IsResolved = true
                },
                new EVRental.BusinessObject.Shared.Models.QuanNH.ReturnCondition
                {
                    ReturnConditionId = 8,
                    Name = "Fair",
                    SeverityLevel = 2,
                    RepairCost = 150.00m,
                    IsResolved = false
                },
            };
        }

        [HttpGet]
        public IEnumerable<EVRental.BusinessObject.Shared.Models.QuanNH.ReturnCondition> Get()
        {
            return returnConditions;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ReturnConditionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReturnConditionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReturnConditionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
