using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace onlive_core.DbModels
{
    public partial class GroupsMembers
    {
        public GroupsMembers()
        {
            GroupsMembersGroupsRoles = new HashSet<GroupsMembersGroupsRoles>();
        }

        public string Username { get; set; }
        public int IdGroups { get; set; }

        public virtual Groups IdGroupsNavigation { get; set; }
        public virtual Users UsernameNavigation { get; set; }
        public virtual ICollection<GroupsMembersGroupsRoles> GroupsMembersGroupsRoles { get; set; }
    }
}
