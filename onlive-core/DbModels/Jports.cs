﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace onlive_core.DbModels
{
    public partial class Jports
    {
        public Jports()
        {
            Events = new HashSet<Events>();
        }

        public int Port { get; set; }
        public bool Running { get; set; }

        public virtual ICollection<Events> Events { get; set; }
    }
}
