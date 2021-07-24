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
				Session.checkCodToken(req.ctx);
				
				GroupService groupService = new GroupService();
				response = groupService.getGroup(req);
			}
            catch (Exception exc)
            {
                response.rCode = -1;
				response.rTitle = exc.Message;
				response.rMessage = exc.InnerException.Message;
            }

            return response;
        }

        [HttpPost]
        [HttpOptions]
        [Route("getMyGroup")]
		public GetGroupResponse getMyGroup([FromBody]GetMyGroupRequest req)
        {
			GetGroupResponse response = new GetGroupResponse();

            try
			{
				Session.checkCodToken(req.ctx);
				
				GroupService groupService = new GroupService();
				response = groupService.getMyGroup(req);
			}
            catch (Exception exc)
            {
                response.rCode = -1;
				response.rTitle = exc.Message;
				response.rMessage = exc.InnerException.Message;
            }

            return response;
        }

        [HttpPost]
        [HttpOptions]
        [Route("getUserGroup")]
		public GetGroupResponse getUserGroup([FromBody]GetMyGroupRequest req)
        {
			GetGroupResponse response = new GetGroupResponse();

            try
			{
				Session.checkCodToken(req.ctx);
				
				GroupService groupService = new GroupService();
				response = groupService.getUserGroup(req);
			}
            catch (Exception exc)
            {
                response.rCode = -1;
				response.rTitle = exc.Message;
				response.rMessage = exc.InnerException.Message;
            }

            return response;
        }
		
		#endregion
    }
}
