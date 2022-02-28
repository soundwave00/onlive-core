using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using onlive_core.Services;
using onlive_core.Models;
using onlive_core.Entities;
using onlive_core.DbModels;

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
				Session.checkCodToken(req.ctx);
				
				EventService eventService = new EventService();
				response = eventService.getEvent(req);
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
				response.rTitle = exc.Message;
				response.rMessage = exc.InnerException.Message;
            }

            return response;
        }

        [HttpPost]
        [HttpOptions]
        [Route("getGroupEvents")]
		public GetEventsResponse getGroupEvents([FromBody]GetEventsRequest req)
        {
			GetEventsResponse response = new GetEventsResponse();

            try
			{
				EventService eventsService = new EventService();
				response = eventsService.getGroupEvents(req);
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
        [Route("createEvent")]
		public GetEventsResponse createEvent([FromBody]GetEventsRequest req)
        {
			GetEventsResponse response = new GetEventsResponse();

            try
			{
				EventService eventsService = new EventService();
				eventsService.createEvent(req);
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
        [Route("startEvent")]
        public Response startEvent([FromBody]GetEventRequest req)
        {
			Response response = new Response();

            try
			{
				EventService eventService = new EventService();
				//response = HomeService.startEvent(req);
				response = eventService.startEvent(req);
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
        public Response stopEvent([FromBody]GetEventRequest req)
        {
			Response response = new Response();

            try
			{
				EventService eventService = new EventService();
				response = eventService.stopEvent(req);
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
