using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace onlive_core.DbModels
{
    public partial class GroupsMembersGroupsRoles
    {
        public string Username { get; set; }
        public int IdGroupsMembers { get; set; }
        public int IdGroupsRoles { get; set; }

        public virtual GroupsMembers GroupsMembers { get; set; }
        public virtual GroupsRoles IdGroupsRolesNavigation { get; set; }
    }
}
