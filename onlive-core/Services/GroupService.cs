using System;
using onlive_core.DataAccess;
using onlive_core.DbModels;
using onlive_core.Models;

namespace onlive_core.Services
{
    public class GroupService
    {
        #region Metodi pubblici
		public GetGroupResponse getGroup(GetGroupRequest req)
        {
			GetGroupResponse getGroupResponse = new GetGroupResponse();

			Groups group = new Groups();

			try
			{
				GroupDataAccess groupDataAccess = new GroupDataAccess();
				group = groupDataAccess.getGroup(req.idEvents);
			}
			catch (Exception exc)
            {
                throw new Exception("Error getting group", exc.InnerException);
            }
			
			if (group == null)
				throw new Exception("Group does not exist");

			getGroupResponse.group = group;

			return getGroupResponse;
        }

		#endregion
    }
}
