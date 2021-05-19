using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using onlive_core.Services;
using onlive_core.Models;

namespace onlive_core.Controllers
{
    [Produces("application/json")]
    [Route("api/EventController")]
    public class EventController
    {
		#region Proprietà

        private readonly ILogger<EventController> _logger;
        
		#endregion
				
		#region Costruttori

        public EventController(ILogger<EventController> logger)
        {
        	_logger = logger;
        }

		#endregion

        #region Metodi

        [HttpPost]
        [HttpOptions]
        [Route("getEvent")]
		public GetEventResponse getEvent([FromBody]GetEventRequest req)
        {
			GetEventResponse response = new GetEventResponse();

            try
			{
				EventService eventService = new EventService();
				response = eventService.getEvent(req);
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
        [Route("getEvents")]
		public GetEventsResponse getEvents([FromBody]GetEventsRequest req)
        {
			GetEventsResponse response = new GetEventsResponse();

            try
			{
				EventService eventsService = new EventService();
				response = eventsService.getEvents(req);
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
