using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using onlive_core.Services;
using onlive_core.DbModels;
using onlive_core.Entities;

namespace onlive_core.Controllers
{
    [Produces("application/json")]
    [Route("api/HomeController")]
    public class HomeController
    {
		#region Proprietà

        private readonly ILogger<HomeController> _logger;
        
		#endregion
				
		#region Costruttori

        public HomeController(ILogger<HomeController> logger)
        {
        	_logger = logger;
        }

		#endregion

        #region Metodi

        [HttpPost]
        [HttpOptions]
        [Route("startEvent")]
        public Response startEvent([FromBody]Events req)
        {
			Response response = new Response();

            try
			{
				HomeService homeService = new HomeService();
				//response = HomeService.startEvent(req);
				response = homeService.startEvent(req, "vlc");
			}
            catch (Exception exc)
            {
				response.rMessage = exc.Message;
            }

            return response;
        }

        [HttpPost]
        [HttpOptions]
        [Route("stopEvent")]
        public Response stopEvent([FromBody]Events req)
        {
			Response response = new Response();

            try
			{
				HomeService homeService = new HomeService();
				response = homeService.stopEvent(req);
			}
            catch (Exception exc)
            {
				response.rMessage = exc.Message;
            }

            return response;
		}

        [HttpPost]
        [HttpOptions]
        [Route("stopAllEvents")]
        public Response stopAllEvents()
        {
			Response response = new Response();

            try
			{
				HomeService homeService = new HomeService();
				response = homeService.stopAllEvents();
			}
            catch (Exception exc)
            {
				response.rMessage = exc.Message;
            }

            return response;
		}
		
		#endregion
    }
}
