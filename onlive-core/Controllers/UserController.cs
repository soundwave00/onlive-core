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

        [HttpPost]
        [HttpOptions]
        [Route("login")]
        public LoginResponse login([FromBody]LoginRequest req)
        {
			LoginResponse response = new LoginResponse();

            try
			{
				UserService userService = new UserService();
				response = userService.login(req);
			}
            catch (Exception exc)
            {
                response.rCode = -1;
				response.rMessage = exc.Message;
            }

            return response;
        }

        [HttpPost]
        [HttpOptions]
        [Route("signup")]
		public Response signup([FromBody]SignupRequest req)
        {
			Response response = new Response();

            try
			{
				UserService userService = new UserService();
				userService.signup(req);
			}
            catch (Exception exc)
            {
                response.rCode = -1;
				response.rMessage = exc.Message;
            }

            return response;
        }

        [HttpPost]
        [HttpOptions]
        [Route("logout")]
        public Response logout([FromBody]Request req)
        {
			Response response = new Response();

            try
			{
				UserService userService = new UserService();
				userService.logout(req);
			}
            catch (Exception exc)
            {
                response.rCode = -1;
				response.rMessage = exc.Message;
            }

            return response;
        }

        [HttpPost]
        [HttpOptions]
        [Route("getUser")]
		public GetUserResponse getUser([FromBody]Request req)
        {
			GetUserResponse response = new GetUserResponse();

            try
			{
				UserService userService = new UserService();
				response = userService.getUser(req);
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
