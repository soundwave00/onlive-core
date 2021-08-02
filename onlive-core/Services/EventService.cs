using System;
using onlive_core.DataAccess;
using onlive_core.DbModels;
using onlive_core.Entities;
using onlive_core.Models;
using System.Collections.Generic;

namespace onlive_core.Services
{
    public class EventService
    {
        #region Metodi pubblici
		public GetEventResponse getEvent(GetEventRequest req)
        {
			GetEventResponse getEventResponse = new GetEventResponse();

			Events eventItem = new Events();

			try
			{
				EventDataAccess eventDataAccess = new EventDataAccess();
				eventItem = eventDataAccess.getEvent(req.id);
			}
			catch (Exception exc)
            {
                throw new Exception("Error getting event", exc.InnerException);
            }
			
			if (eventItem == null)
				throw new Exception("Event does not exist");

			getEventResponse.eventItem = eventItem;

			return getEventResponse;
        }

		public GetEventsResponse getEvents(GetEventsRequest req)
        {
			GetEventsResponse getEventsResponse = new GetEventsResponse();

			List<Events> eventsList = new List<Events>();

			try
			{
				EventDataAccess eventDataAccess = new EventDataAccess();
				eventsList = eventDataAccess.getEvents(req.dateFrom);
			}
			catch (Exception exc)
            {
                throw new Exception("Error getting event", exc.InnerException);
            }
			
			if (eventsList == null)
				throw new Exception("Event does not exist");

			getEventsResponse.eventsList = eventsList;

			return getEventsResponse;
        }

		public GetEventsResponse getGroupEvents(GetEventsRequest req)
        {
			GetEventsResponse getEventsResponse = new GetEventsResponse();

			List<Events> eventsList = new List<Events>();

			try
			{
				EventDataAccess eventDataAccess = new EventDataAccess();
				eventsList = eventDataAccess.getGroupEvents(req.groupId);
			}
			catch (Exception exc)
            {
                throw new Exception("Error getting event", exc.InnerException);
            }
			
			if (eventsList == null)
				throw new Exception("Event does not exist");

			getEventsResponse.eventsList = eventsList;

			return getEventsResponse;
        }

		#endregion
    }
}
