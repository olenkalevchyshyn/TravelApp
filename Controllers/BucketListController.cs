using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Models;
using TravelApp.Services;

namespace TravelApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BucketListController : ControllerBase
    {
        private readonly BucketListService _bucketListService;

        public BucketListController(BucketListService bucketListService)
        {
            _bucketListService = bucketListService;
        }

        //get bucketlist by id
        // https://localhost:44347/api/BucketList/5f8c6e7ce40b82c4840ad154
        [HttpGet("{bucketListId}", Name = "GetBucketList")]
        public ActionResult<BucketList> Get(string bucketListId)
        {
            ObjectId id = new ObjectId(bucketListId);
            if (id != null)
                return _bucketListService.Get(id);
            else
                return NotFound();
        }

        //get places by bucketlist id
        // https://localhost:44347/api/BucketList/5f8c6e7c5cf6b6d3a0b99215/All
        [HttpGet("{bucketListId}/All")]
        public ActionResult<IEnumerable<Place>> GetPlacesById(string bucketListId)
        {
            ObjectId id = new ObjectId(bucketListId);
            if (id != null)
                return _bucketListService.GetPlaces(id).ToList();
            else
                return NotFound();
        }        

        [HttpPost]
        public ActionResult Post(BucketList bucketList)
        {
            if (bucketList != null)
            {
                _bucketListService.Create(bucketList);
                return CreatedAtRoute("GetBucketList", new { id = bucketList.Id }, bucketList);
            }
            else
                return NotFound();
        }

        [HttpPut("id")]
        public ActionResult Put(string bucketListId)
        {
            ObjectId id = new ObjectId(bucketListId);
            if (id != null)
            {
                var bucketList = _bucketListService.Get(id);
                if (bucketList != null)
                    _bucketListService.Update(id, bucketList);
                else
                    return NotFound();
            }
            else
                return NotFound();
            return NoContent();
        }

        [HttpDelete("bucketListId")]
        public ActionResult DeleteBucketList(string bucketListId)
        {
            ObjectId id = new ObjectId(bucketListId);
            if (id != null)
            {
                var bucketList = _bucketListService.Get(id);
                if (bucketList != null)
                    _bucketListService.Delete(bucketList);
                else
                    return NotFound();
            }
            else
                return NotFound();
            return NoContent();
        }
    }
}
