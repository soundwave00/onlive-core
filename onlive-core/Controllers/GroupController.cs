using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using onlive_core.Services;
using onlive_core.Models;

namespace onlive_core.Controllers
{
    [Produces("application/json")]
    [Route("api/GroupController")]
    public class GroupController
    {
		#region Proprietà

        private readonly ILogger<GroupController> _logger;
        
		#endregion
				
		#region Costruttori

        public GroupController(ILogger<GroupController> logger)
        {
        	_logger = logger;
        }

		#endregion

        #region Metodi

        [HttpPost]
        [HttpOptions]
        [Route("getGroup")]
		public GetGroupResponse getGroup([FromBody]GetGroupRequest req)
        {
			GetGroupResponse response = new GetGroupResponse();

            try
			{
				GroupService groupService = new GroupService();
				response = groupService.getGroup(req);
			}
            catch (Exception exc)
            {
                response.rCode = -1;
				response.rMessage = exc.Message;
            }

            return response;
        }
		
		#endregion
    }
}
