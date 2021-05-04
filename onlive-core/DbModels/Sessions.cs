using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace onlive_core.DbModels
{
    public partial class Sessions
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string CodiceToken { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateExp { get; set; }

        public virtual Users UsernameNavigation { get; set; }
    }
}
