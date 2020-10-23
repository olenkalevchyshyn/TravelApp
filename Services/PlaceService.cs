using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TravelApp.Models;

namespace TravelApp.Services
{
    public class PlaceService
    {
        private string collectionName = "Places";
        private IMongoCollection<Place> _place;
        private IMongoDatabase database;
        public PlaceService(IMongoClient client)
        {
            database = client.GetDatabase("TravelAppDb");
            _place = database.GetCollection<Place>(collectionName);
        }
        public Place Get(ObjectId id)
        {
            if (id == null)
                return null;
            var filter = Builders<Place>.Filter.Eq("Id", id);
            return _place.Find(filter).FirstOrDefault();            
        }
        public Place Create(Place place)
        {
            if (place == null)
                return null;
            _place.InsertOne(place);
            return place;
        }
        public void Update(ObjectId id, Place place)
        {
            _place.ReplaceOne(p => p.Id == id, place);
        }
        public void Delete(Place place)
        {
            _place.DeleteOne(p => p.Id == place.Id);
        }
        public void Delete(ObjectId id)
        {
            _place.DeleteOne(p => p.Id == id);
        }

        public IEnumerable<Place> FindPlaces(ObjectId id)
        {
            //if (id == null)
            //    return null;
            //var filter1 = Builders<Place>.Filter.Eq("Id", id);
            //var place = _place.Find(filter1).FirstOrDefault();
            //if (place == null)
            //    return null;

            //var maxDistanceInKm = 5;
            ////Expression<Func<Place, object>> field = Expression<Func<Place, object>>(); //????
            //var collection = database.GetCollection<Place>(collectionName);
            //var point = GeoJson.Point(GeoJson.Geographic(place.Longitude, place.Latitude));
            //var filter2 = Builders<Place>.Filter.Near(field, point, maxDistanceInKm * 1000);
            //return collection.Find(filter2).ToList();
            return null;
        }
    }
}
