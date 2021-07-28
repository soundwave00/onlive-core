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

		#endregion
    }
}
