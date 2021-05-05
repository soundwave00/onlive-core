using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace onlive_core.DbModels
{
    public partial class GroupsRoles
    {
        public GroupsRoles()
        {
            GroupsMembersGroupsRoles = new HashSet<GroupsMembersGroupsRoles>();
        }

        public int Id { get; set; }
        public string Roles { get; set; }

        public virtual ICollection<GroupsMembersGroupsRoles> GroupsMembersGroupsRoles { get; set; }
    }
}
