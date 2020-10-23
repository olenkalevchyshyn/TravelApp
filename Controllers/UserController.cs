using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TravelApp.Models;
using TravelApp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelApp.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")] 
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        //get place by id
        // https://localhost:44347/api/User/5f8c6cf1556aceeaead9e128
        [HttpGet("{userId}", Name = "GetUser")]
        public ActionResult<User> Get(string userId)
        {
            ObjectId id = new ObjectId(userId);
            if (id != null)
                return _userService.Get(id);
            else
                return NotFound();
        }

        //gey all user`s bucketlists
        //https://localhost:44347/api/User/GetAllbyUserId/5f8c6cf1556aceeaead9e128
        [HttpGet("GetAllbyUserId/{userId}")]
        public ActionResult<IEnumerable<ObjectId>> GetAllbyUserId(string userId)
        {
            ObjectId id = new ObjectId(userId);
            if (id != null)
                return _userService.GetBucketLists(id).ToList();
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult Post(User user)
        {
            if (user != null)
            {
                _userService.Create(user);
                return CreatedAtRoute("GetPlace", new { id = user.Id }, user);
            }
            else
                return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult Put(string userId)
        {
            ObjectId id = new ObjectId(userId);
            if (id != null)
            {
                var user = _userService.Get(id);
                if (user != null)
                    _userService.Update(id, user);
                else
                    return NotFound();
            }
            else
                return NotFound();
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(string userId)
        {
            ObjectId id = new ObjectId(userId);
            if (id != null)
            {
                var user = _userService.Get(id);
                if (user != null)
                    _userService.Delete(user);
                else
                    return NotFound();
            }
            else
                return NotFound();
            return NoContent();
        }
    }
}
