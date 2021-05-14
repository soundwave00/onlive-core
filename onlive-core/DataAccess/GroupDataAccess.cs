using System.Linq;
using System.Data;

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

		#endregion
    }
}
