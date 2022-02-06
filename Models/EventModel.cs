using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Event_Management.Models
{
    public class EventModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserID { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        public DateTime? Date { get; set; }
        public bool IsRecarsive { get; set; }
        public string[] Days { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ImageName { get; set; }
    }
}
