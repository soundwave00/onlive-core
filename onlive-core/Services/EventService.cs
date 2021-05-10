using System;
using onlive_core.DataAccess;
using onlive_core.DbModels;
using onlive_core.Entities;
using onlive_core.Models;

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
                throw new Exception("Error getting event");
            }
			
			if (eventItem == null)
				throw new Exception("Event does not exist");

			getEventResponse.eventItem = eventItem;

			return getEventResponse;
        }

		#endregion
    }
}
