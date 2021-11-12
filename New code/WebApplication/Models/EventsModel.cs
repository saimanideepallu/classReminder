using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class EventsModel
    {
        public string UserID { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public bool IsRecarsive {get;set;}
        public string[] Days { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ImageName { get; set; }
    }
}