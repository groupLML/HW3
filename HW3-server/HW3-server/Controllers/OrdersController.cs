using HW3_server.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HW3_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<Vacation> Get()
        {
            Vacation vacation= new Vacation();
            return vacation.Read();
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        [HttpGet("/startDate/{start}/endDate/{end}")] //Routing Resource
        public IActionResult GetByDates(DateTime start, DateTime end)
        {
            List<Vacation> vacationsList = Vacation.getByDates(start, end);

            if (vacationsList.Count > 0) //אם הרשימה לא ריקה תחזיר אותה
            {
                return Ok(vacationsList);
            }
            return NotFound("There are no results");
        }

        // POST api/<OrdersController>
        [HttpPost]
        public bool Post([FromBody] Vacation vacation)
        {
            return vacation.Insert();
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
