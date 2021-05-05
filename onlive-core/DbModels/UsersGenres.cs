using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace onlive_core.DbModels
{
    public partial class UsersGenres
    {
        public string Username { get; set; }
        public int IdGenres { get; set; }

        public virtual Genres IdGenresNavigation { get; set; }
        public virtual Users UsernameNavigation { get; set; }
    }
}
