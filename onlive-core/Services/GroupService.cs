using System;
using onlive_core.DataAccess;
using onlive_core.DbModels;
using onlive_core.Models;
using System.Collections.Generic;

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

		public GetGroupResponse getMyGroup(GetMyGroupRequest req)
        {
			GetGroupResponse getGroupResponse = new GetGroupResponse();

			Groups group = new Groups();

			try
			{
				GroupDataAccess groupDataAccess = new GroupDataAccess();
				group = groupDataAccess.getMyGroup(req.groupId);
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

		public GetGroupResponse getUserGroup(GetMyGroupRequest req)
        {
			GetGroupResponse getGroupResponse = new GetGroupResponse();

			//Groups group = new Groups();
			List<int> group = new List<int>();

			try
			{
				GroupDataAccess groupDataAccess = new GroupDataAccess();
				group = groupDataAccess.getUserGroup(req.ctx.user);
			}
			catch (Exception exc)
            {
                throw new Exception("Error getting group", exc.InnerException);
            }
			
			if (group == null)
				throw new Exception("Group does not exist");

			getGroupResponse.userGroup = group;

			return getGroupResponse;
        }


		#endregion
    }
}
