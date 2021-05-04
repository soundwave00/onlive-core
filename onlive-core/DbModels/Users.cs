using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace onlive_core.DbModels
{
    public partial class Users
    {
        public Users()
        {
            Sessions = new HashSet<Sessions>();
        }

        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
		public DateTime DateCreate { get; set; }
		public DateTime? DateDelete { get; set; }

        public virtual ICollection<Sessions> Sessions { get; set; }
    }
}
