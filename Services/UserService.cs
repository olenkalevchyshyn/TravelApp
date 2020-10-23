using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Models;

namespace TravelApp.Services
{
    public class UserService
    {
        private string collectionName = "Users";
        private IMongoCollection<User> _user;
        public UserService(IMongoClient client)
        {
            var database = client.GetDatabase("TravelAppDb");
            _user = database.GetCollection<User>(collectionName);
        }
        public User Get(ObjectId id)
        {
            if (id == null)
                return null;
            var filter = Builders<User>.Filter.Eq("Id", id);
            return _user.Find(filter).FirstOrDefault();
        }

        public IEnumerable<ObjectId> GetBucketLists(ObjectId id)
        {
            if (id == null)
                return null;
            var filter = Builders<User>.Filter.Eq("Id", id);
            var user = _user.Find(filter).FirstOrDefault();            
            return user.BucketLists;
        }

        public User Create(User user)
        {
            if (user == null)
                return null;
            _user.InsertOne(user);
            return user;
        }
        public void Update(ObjectId id, User user)
        {
            _user.ReplaceOne(u => u.Id == id, user);
        }
        public void Delete(User user)
        {
            _user.DeleteOne(u => u.Id == user.Id);
        }
        public void Delete(ObjectId id)
        {
            _user.DeleteOne(u => u.Id == id);
        }
    }
}
