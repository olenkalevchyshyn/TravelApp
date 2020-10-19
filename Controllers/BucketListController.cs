using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BucketListController
    {
        private string collectionName = "Lists";
        private IMongoCollection<BucketList> _bucketList;

        public BucketListController(IMongoClient client)
        {
            var database = client.GetDatabase("TravelAppDb");
            _bucketList = database.GetCollection<BucketList>(collectionName);
        }

        //get bucketlist by id
        // https://localhost:44347/api/BucketList/5f8c6e7c3089b5481697bb3b
        [HttpGet("{bucketListId}")]
        public ActionResult<BucketList> GetById(string bucketListId)
        {
            //if (bucketListId == null)
            //    return NotFound();
            ObjectId id = new ObjectId(bucketListId);
            //if (id == null)
            //    return NotFound();
            var filter = Builders<BucketList>.Filter.Eq("Id", id);
            var bucketList = _bucketList.Find(filter);
            //if (bucketList == null)
            //    return NotFound();
            return bucketList.FirstOrDefault();
        }

        //get places by bucketlist id
        // https://localhost:44347/api/BucketList/5f8c6e7c3089b5481697bb3b/All
        [HttpGet("{bucketListId}/All")]
        public ActionResult<IEnumerable<Place>> GetPlacesById(string bucketListId)
        {
            //if (bucketListId == null)
            //    return NotFound();
            ObjectId id = new ObjectId(bucketListId);
            //if (id == null)
            //    return  NotFound();
            var filter = Builders<BucketList>.Filter.Eq("Id", id);
            var bucketList = _bucketList.Find(filter).FirstOrDefault();
            var places = bucketList.Places;
            //if (places == null)
            //    return NotFound();
            return places.ToList();
        }

        //gey all user`s bucketlists
        //https://localhost:44347/api/BucketList/GetAllbyUserId/5f8c6cf161d7659726aa03a8
        [HttpGet("GetAllbyUserId/{userId}")]
        public ActionResult<IEnumerable<BucketList>> GetAllbyUserId(string userId) 
        {
            //if (userId == null)
            //    return NotFound();
            ObjectId id = new ObjectId(userId);
            //if (id == null)
            //    return NotFound();
            var filter = Builders<BucketList>.Filter.Eq("UserId", id);
            var collection = _bucketList.Find(filter);
            //if (collection == null)
            //    return NotFound();
            return collection.ToList();
        }

        [HttpPost]
        public void InsertBucketList(BucketList bucketList) //return ActionResult
        {
            //if (bucketList == null)
            //    return NotFound();
            _bucketList.InsertOne(bucketList);
            //return CreatedAtRoute("GetById", new { id = bucketList.Id }, bucketList);
        }
        [HttpPost]
        public void InsertBucketLists(List<BucketList> bucketLists)
        {
            //if (bucketLists == null)
            //    return NotFound();
            _bucketList.InsertMany(bucketLists);            
        }

        [HttpPut("bucketListId")]
        public void UpdateBucketList(BucketList bucketList) //return ActionResult
        {
            //if (bucketListId == null || bucketList == null)
            //    return NotFound();
            _bucketList.ReplaceOne(
                new BsonDocument("_id", bucketList.Id),
                bucketList,
                new ReplaceOptions { IsUpsert = true });
            //return NoContent();
        }

        [HttpDelete("bucketListId")]
        public void DeleteBucketList(ObjectId bucketListId)  //return ActionResult
        {
            //if (bucketListId == null)
            //    return NotFound();
            var filter = Builders<BucketList>.Filter.Eq("Id", bucketListId);
            _bucketList.DeleteOne(filter);
            //return NoContent();
        }
    }
}
