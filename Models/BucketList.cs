using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public class BucketList
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("places")]
        public List<Place> Places { get; set; }
        [BsonElement("user_id")]
        public ObjectId UserId { get; set; }
    }
}
