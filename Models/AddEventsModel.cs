using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class AddEventsModel
    {
        public string EventName { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public string Recarsive { get; set; }
        public string M { get; set; }
        public string T { get; set; }
        public string W { get; set; }
        public string TH { get; set; }
        public string F { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

    }
}