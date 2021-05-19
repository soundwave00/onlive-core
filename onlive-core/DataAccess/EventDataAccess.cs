using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using onlive_core.DbModels;

namespace onlive_core.DataAccess
{
    public class EventDataAccess
    {
        #region Metodi

        public Events getEvent(int id)
        {
			Events eventItem = new Events();

			using (var context = new ONSTAGEContext())
			{
				eventItem = context.Events
					.Where(x => x.Id == id)
					.FirstOrDefault();
			}

			return eventItem;
        }

		public List<Events> getEvents(DateTime dateFrom)
        {
			List<Events> eventsList = new List<Events>();
			//Groups group = new Groups();

			using (var context = new ONSTAGEContext())
			{
				eventsList = context.Events
					.Where(x => x.DateStart >= dateFrom)
					.Where(x => x.DateStart <= dateFrom.AddMonths(1))
					.ToList();
			}

			return eventsList;
        }

		#endregion
    }
}
