using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using onlive_core.Services;
using onlive_core.DbModels;
using onlive_core.Entities;
using onlive_core.Models;

namespace onlive_core.Controllers
{
    [Produces("application/json")]
    [Route("api/UserController")]
    public class UserController
    {
		#region Proprietà

        private readonly ILogger<UserController> _logger;
        
		#endregion
				
		#region Costruttori

        public UserController(ILogger<UserController> logger)
        {
        	_logger = logger;
        }

		#endregion

        #region Metodi

        /*
        [HttpPost]
        [HttpOptions]
        [Route("login")]
        public LoginResponse login([FromBody]Request req)
        {
			LoginResponse response = new LoginResponse();

            try
			{
				UserService userService = new UserService();
				response = userService.login(req);
			}
            catch (Exception exc)
            {
				response.rMessage = exc.Message;
            }

            return response;
        }
        */

        [HttpPost]
        [HttpOptions]
        [Route("signup")]
		public Response signup([FromBody]SignupRequest req)
        {
			Response response = new Response();

            try
			{
				UserService userService = new UserService();
				userService.signup(req.user);
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
