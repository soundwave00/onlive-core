using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace onlive_core.DbModels
{
    public partial class EventsGenres
    {
        public int IdGenres { get; set; }
        public int IdEvents { get; set; }

        public virtual Events IdEventsNavigation { get; set; }
        public virtual Genres IdGenresNavigation { get; set; }
    }
}
