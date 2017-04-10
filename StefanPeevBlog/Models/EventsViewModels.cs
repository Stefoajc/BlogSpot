using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StefanPeevBlog.Models
{
    public class EventsViewModels
    {
    }

    public class EventsHomeViewModel
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; }
    }

    public class EventsLocationViewModel
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
    }
}