using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;

using onlive_core.Db;
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

		#endregion
    }
}
