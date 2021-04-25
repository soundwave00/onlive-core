using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using onlive_core.Services;
using onlive_core.DbModels;
using onlive_core.Entities;

namespace onlive_core.Controllers
{
    [Produces("application/json")]
    [Route("api/JamulusController")]
    public class JamulusController
    {
		#region Proprietà

        private readonly ILogger<JamulusController> _logger;
        
		#endregion
				
		#region Costruttori

        public JamulusController(ILogger<JamulusController> logger)
        {
        	_logger = logger;
        }

		#endregion

        #region Metodi

        [HttpPost]
        [HttpOptions]
        [Route("startLive")]
        public Response startLive([FromBody]Live req)
        {
			Response response = new Response();

            try
			{
				JamulusService jamulusService = new JamulusService();
				//response = jamulusService.startLive(req);
				response = jamulusService.startLive(req, "vlc");
			}
            catch (Exception exc)
            {
				response.rMessage = exc.Message;
            }

            return response;
        }

        [HttpPost]
        [HttpOptions]
        [Route("stopLive")]
        public Response stopLive([FromBody]Live req)
        {
			Response response = new Response();

            try
			{
				JamulusService jamulusService = new JamulusService();
				response = jamulusService.stopLive(req);
			}
            catch (Exception exc)
            {
				response.rMessage = exc.Message;
            }

            return response;
		}

        [HttpPost]
        [HttpOptions]
        [Route("stopAllLive")]
        public Response stopAllLive()
        {
			Response response = new Response();

            try
			{
				JamulusService jamulusService = new JamulusService();
				response = jamulusService.stopAllLive();
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
