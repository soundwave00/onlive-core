using System.Linq;
using System.Data;
using System.Collections.Generic;

using onlive_core.DbModels;

namespace onlive_core.DataAccess
{
    public class GroupDataAccess
    {
        #region Metodi

        public Groups getGroup(int idEvents)
        {
			Groups group = new Groups();
			Events eventItem = new Events();

			using (var context = new ONSTAGEContext())
			{
				var groupTmp = context.Groups
					.Join(
						context.Events,
						group => group.Id,
						eventItem => eventItem.IdGroups, 
						(group,eventItem) => new
						{	
							Id = group.Id,
							Name = group.Name,
							Description = group.Description,
							Event = eventItem.Id,
							Avatar = group.Avatar
						}
					)
					.Where (x => x.Event == idEvents)
					.FirstOrDefault();

				group.Id = groupTmp.Id;
				group.Name = groupTmp.Name;
				group.Description = groupTmp.Description;
				group.Avatar = groupTmp.Avatar;
			}

			return group;
        }

		public Groups getMyGroup(int groupId)
        {
			Groups group = new Groups();

			using (var context = new ONSTAGEContext())
			{
				group = context.Groups
					.Where(x => x.Id == groupId)
					.FirstOrDefault();
			}

			return group;
        }

		public List<int> getUserGroup(Users user)
        {
			List<int> userGroup = new List<int>();

			using (var context = new ONSTAGEContext())
			{
				userGroup = context.GroupsMembers
					.Where(x => x.Username == user.Username)
					.Select(x => x.IdGroups)
					.ToList();
			}

			return userGroup;
        }

		public List<GroupsMembers> getMembersGroup(int groupId)
        {
			List<GroupsMembers> userMembersList = new List<GroupsMembers>();

			using (var context = new ONSTAGEContext())
			{
				userMembersList = context.GroupsMembers
					.Where(x => x.IdGroups == groupId)
					.Select(x => x)
					.ToList();
			}


			return userMembersList;
        }

		public List<Genres> getGroupGenres(int groupId)
        {
			List<Genres> genres = new List<Genres>();
			
			
			using (ONSTAGEContext context = new ONSTAGEContext())
			{
				var genre = (from gg in context.GroupsGenres
								join g in context.Genres
								on gg.IdGenres equals g.Id
								where gg.IdGroups == groupId
								select new {
									Id = g.Id,
									Genre = g.Genre
								}).ToList();

				foreach (var g in genre)
				{	
					Genres genreItem = new Genres();
					genreItem.Id = g.Id;
					genreItem.Genre = g.Genre;
					genres.Add(genreItem);
				}

			}

			return genres;
        }

		public void createGroup(Groups group, List<string> groupComponents)
        {
			Groups groupItem = new Groups();
			
			groupItem = group;

			using (var context = new ONSTAGEContext())
			{
				context.Groups.Add(groupItem);
				context.SaveChanges();
				int id = groupItem.Id;

				foreach(var comp in groupComponents){
					GroupsMembers components = new GroupsMembers();
					components.Username = comp;
					components.IdGroups = id;
					context.GroupsMembers.Add(components);
					context.SaveChanges(); 
				}
			}

        }

		#endregion
    }
}
