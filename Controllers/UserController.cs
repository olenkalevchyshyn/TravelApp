using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TravelApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMongoCollection<User> _user;
        public UserController(IMongoClient client)
        {
            var database = client.GetDatabase("TravelAppDb");
            _user = database.GetCollection<User>("Users");
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _user.Find(u => u.Password == "pass123").ToList();
        }

        //// GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(string id)
        {
            ObjectId _id = new ObjectId(id);
            return _user.Find(u => u.Id == _id).First();
        }

        //// POST api/<UserController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
