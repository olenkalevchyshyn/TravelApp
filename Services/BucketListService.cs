using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Models;

namespace TravelApp.Services
{
    public class BucketListService
    {
        private string collectionName = "Lists";
        private IMongoCollection<BucketList> _bucketList;
        public BucketListService(IMongoClient client)
        {
            var database = client.GetDatabase("TravelAppDb");
            _bucketList = database.GetCollection<BucketList>(collectionName);
        }
        public BucketList Get(ObjectId id)
        {
            if (id == null)
                return null;
            var filter = Builders<BucketList>.Filter.Eq("Id", id);
            return _bucketList.Find(filter).FirstOrDefault();
        }

        public IEnumerable<Place> GetPlaces(ObjectId id)
        {
            if (id == null)
                return null;
            var filter = Builders<BucketList>.Filter.Eq("Id", id);
            var bucketList = _bucketList.Find(filter).FirstOrDefault();
            return bucketList.Places;
        }
        public BucketList Create(BucketList bucketList)
        {
            if (bucketList == null)
                return null;
            _bucketList.InsertOne(bucketList);
            return bucketList;
        }
        public void Update(ObjectId id, BucketList bucketList)
        {
            _bucketList.ReplaceOne(b => b.Id == id, bucketList);
        }
        public void Delete(BucketList bucketList)
        {
            _bucketList.DeleteOne(b => b.Id == bucketList.Id);
        }
        public void Delete(ObjectId id)
        {
            _bucketList.DeleteOne(b => b.Id == id);
        }
    }
}
