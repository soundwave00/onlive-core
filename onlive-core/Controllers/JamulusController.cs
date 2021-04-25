using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using onlive_core.Services;
using onlive_core.DbModels;

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
        [Route("testLive")]
        public string testLive([FromBody]Live req)
        {
			string response;

            try
			{
				JamulusService jamulusService = new JamulusService();
				response = jamulusService.testLive(req);
			}
            catch (Exception exc)
            {
				response = exc.Message;
            }

            return response;
        }

        [HttpPost]
        [HttpOptions]
        [Route("stopAllLive")]
        public string stopAllLive()
        {
			string response;

            try
			{
				JamulusService jamulusService = new JamulusService();
				response = jamulusService.stopAllLive();
			}
            catch (Exception exc)
            {
				response = exc.Message;
            }

            return response;
		}
		
		#endregion
    }
}
