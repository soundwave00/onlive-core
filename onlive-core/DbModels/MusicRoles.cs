using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace onlive_core.DbModels
{
    public partial class MusicRoles
    {
        public MusicRoles()
        {
            UsersMusicRoles = new HashSet<UsersMusicRoles>();
        }

        public int Id { get; set; }
        public string Instrument { get; set; }

        public virtual ICollection<UsersMusicRoles> UsersMusicRoles { get; set; }
    }
}
