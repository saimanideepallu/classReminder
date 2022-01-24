using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace classReminder.Models
{
    public class scheduleModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("reminderName")]
        public string ReminderName { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("dateandtime")]
        public string DateTime { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }
    }
}
