using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace onlive_core.DbModels
{
    public partial class Groups
    {
        public Groups()
        {
            Events = new HashSet<Events>();
            FavoritesGroups = new HashSet<FavoritesGroups>();
            GroupsGenres = new HashSet<GroupsGenres>();
            GroupsMembers = new HashSet<GroupsMembers>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Events> Events { get; set; }
        public virtual ICollection<FavoritesGroups> FavoritesGroups { get; set; }
        public virtual ICollection<GroupsGenres> GroupsGenres { get; set; }
        public virtual ICollection<GroupsMembers> GroupsMembers { get; set; }
    }
}
