using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using TravelApp.Models;
using Newtonsoft.Json.Serialization;

namespace TravelApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceController : ControllerBase
    {
        private string collectionName = "Places";
        private IMongoCollection<Place> _place;
        public PlaceController(IMongoClient client)
        {
            var database = client.GetDatabase("TravelAppDb");
            _place = database.GetCollection<Place>(collectionName);
        }

        //get place by id
        // https://localhost:44347/api/Place/5f8c78f38e4c5e7e02c120db
        [HttpGet("{placeId}")]
        public ActionResult<Place> GetById(string placeId)
        {
            if (placeId == null)
                return NotFound();
            ObjectId id = new ObjectId(placeId);
            if (id == null)
                return NotFound();
            var filter = Builders<Place>.Filter.Eq("Id", id);
            var place = _place.Find(filter).FirstOrDefault();
            return place;
        }

        //[HttpGet("{bucketListId}/All")]
        //public ActionResult<IEnumerable<Place>> GetAllbyListId(string bucketListId)
        //{
        //    if (bucketListId == null)
        //        return NotFound();
        //    ObjectId id = new ObjectId(bucketListId);
        //    if (id == null)
        //        return NotFound();            
        //    var collection = _place.Find(new BsonDocument()).ToList();
        //    return collection;
        //}

        [HttpPost]
        public ActionResult InsertBucketList(Place place)
        {
            //if (place == null)
            //    return NotFound();
            _place.InsertOne(place);
            return CreatedAtRoute("GetById", new { id = place.Id }, place);
        }

        [HttpPost]
        public void InsertBucketLists(List<Place> places)
        {
            _place.InsertMany(places);
            // CReateAtAction
        }

        public ActionResult<IEnumerable<Place>> GetPlacesInRadius(Place initialPlace, double radius)
        {
            if (initialPlace == null)
                return NotFound();
            List<Place> placesInRadius = new List<Place>();
            double deltaLong = initialPlace.Longitude - radius;
            double deltaLat = initialPlace.Latitude - radius;
            foreach (var place in _place.Find(new BsonDocument()).ToList())
            {
                if (place.Longitude < deltaLong || place.Latitude < deltaLat)
                    continue;
                else
                {
                    var s = Math.Abs(Math.Sqrt(initialPlace.Longitude - place.Longitude) - Math.Sqrt(initialPlace.Latitude - place.Latitude));
                    if (s < radius * radius)
                        placesInRadius.Add(place);
                }
            }
            return placesInRadius;
        }
    }
}
