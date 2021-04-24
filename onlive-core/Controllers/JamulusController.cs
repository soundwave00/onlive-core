using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using onlive_core.Services;

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
        public string testLive()
        {
			JamulusService jamulusService = new JamulusService();
			string response = jamulusService.testLive();

            return response;
        }

        [HttpPost]
        [HttpOptions]
        [Route("stopAllLive")]
        public string stopAllLive()
        {
			JamulusService jamulusService = new JamulusService();
			string response = jamulusService.stopAllLive();

            return response;
		}
		
		#endregion
    }
}
