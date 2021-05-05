using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace onlive_core.DbModels
{
    public partial class Genres
    {
        public Genres()
        {
            EventsGenres = new HashSet<EventsGenres>();
            GroupsGenres = new HashSet<GroupsGenres>();
            UsersGenres = new HashSet<UsersGenres>();
        }

        public int Id { get; set; }
        public string Genre { get; set; }

        public virtual ICollection<EventsGenres> EventsGenres { get; set; }
        public virtual ICollection<GroupsGenres> GroupsGenres { get; set; }
        public virtual ICollection<UsersGenres> UsersGenres { get; set; }
    }
}
