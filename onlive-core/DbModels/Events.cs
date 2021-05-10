using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace onlive_core.DbModels
{
    public partial class Events
    {
        public Events()
        {
            EventsGenres = new HashSet<EventsGenres>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IdGroups { get; set; }
        public int? Pid { get; set; }
        public int? Port { get; set; }
        public bool Running { get; set; }
		public DateTime DateSet { get; set; }
		public DateTime? DateStart { get; set; }
		public DateTime? DateStop { get; set; }

        public virtual Groups IdGroupsNavigation { get; set; }
        public virtual Jports PortNavigation { get; set; }
        public virtual ICollection<EventsGenres> EventsGenres { get; set; }
    }
}
