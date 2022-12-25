using HW3_server.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HW3_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            User user= new User();
            return user.Read();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("/login")] //Query String
        public User LoginUser(string email, string password)
        {
            User user= new User();
            return user.login(email, password);
        }

        // POST api/<UsersController>
        [HttpPost]
        public bool Post([FromBody] User user)
        {
            return user.registration();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{email}")]
        public bool Put(string email , [FromBody] User user)
        {
            user.Email = email;
            int numAffected = user.Update(); 
            if (numAffected == 1)
                return true;
            else
                return false;
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
