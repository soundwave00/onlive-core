using onlive_core.Entities;
using onlive_core.DbModels;

using System.Collections.Generic;
using System;


namespace onlive_core.Models
{
    public class GetEventsRequest: Request
    {
        public List<int> genres { get; set; }
        public DateTime dateFrom { get; set; }
        public int groupId { get; set; }
        public Events events { get; set; }
        //public DateTime dateTo { get; set; }
    }
}
