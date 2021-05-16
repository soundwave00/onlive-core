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
            FavoritesGroups = new HashSet<FavoritesGroups>();
            GroupsMembers = new HashSet<GroupsMembers>();
            Sessions = new HashSet<Sessions>();
            UsersGenres = new HashSet<UsersGenres>();
            UsersMusicRoles = new HashSet<UsersMusicRoles>();
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsActive { get; set; }
        public string Avatar { get; set; }
		public DateTime DateCreate { get; set; }
		public DateTime? DateDelete { get; set; }

        public virtual ICollection<FavoritesGroups> FavoritesGroups { get; set; }
        public virtual ICollection<GroupsMembers> GroupsMembers { get; set; }
        public virtual ICollection<Sessions> Sessions { get; set; }
        public virtual ICollection<UsersGenres> UsersGenres { get; set; }
        public virtual ICollection<UsersMusicRoles> UsersMusicRoles { get; set; }
    }
}
