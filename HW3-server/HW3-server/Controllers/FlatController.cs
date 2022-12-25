using HW3_server.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HW3_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatController : ControllerBase
    {
        // GET: api/<FlatController>
        [HttpGet]
        public IEnumerable<Flat> Get()
        {
            Flat flat = new Flat();           
            return flat.Read();
        }

        // GET api/<FlatController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("/getCityPrice")] //Query String
        public IActionResult GetCityPrice(string city, double price)
        {
            List<Flat> flatList = Flat.getCityPrice(city, price);

            if (flatList.Count > 0) //אם הרשימה לא ריקה תחזיר אותה
            {
                return Ok(flatList);
            }
           return NotFound("There are no results");
        }

        // POST api/<FlatController>
        [HttpPost]
        public bool Post([FromBody] Flat flat)
        {
            return flat.Insert();
        }

        // PUT api/<FlatController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FlatController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
