using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;

using onlive_core.DbModels;
using onlive_core.Db;

namespace onlive_core.DataAccess
{
    public class EventDataAccess
    {
		#region Proprietà

        public IDatabase Db { get; set; }
        
		#endregion

		#region SQL Command Source

		private const String getRunningEventsQuery = @"
			SELECT E.*
			FROM {0}EVENTS E
			JOIN {0}EVENTS_GENRES EG
			ON E.ID = EG.ID_EVENTS
			JOIN {0}GENRES G 
			ON EG.ID_GENRES = G.ID
			WHERE G.ID IN (6,2)
			AND E.DATE_SET >= NOW() AND E.DATE_SET <= NOW() + INTERVAL 1 MONTH
		";

		#endregion

		#region DB
		private void InitDB()
        {
            if (Db == null)
            {
				Db = new IDatabase();
                Db.Open();
            }
        }

        private void ReleseDB()
        {
            if (Db != null)
            {
                Db.Close();
                Db = null;
            }
        }

        #endregion

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
			List<Genres> genresList = new List<Genres>();
			//Groups group = new Groups();

			/*using (var context = new ONSTAGEContext())
			{
				genresList = context.EventsGenres
					.Join(
						context.Events,
						eventGenres => eventGenres.IdEvents,
						eventItem => eventItem.Id,
						(eventGenres,eventItem) => new
						{
							IdGenres = eventGenres.IdGenres,
							DateStart = eventItem.DateStart,
							Name = eventItem.Name
						}
					)
					.Where(x => x.DateStart >= dateFrom)
					.Where(x => x.DateStart <= dateFrom.AddMonths(1))
					.Join(
						context.Genres,
						eventGenres => eventGenres.IdGenres,
						genres => genres.Id,
						(eventGenres, genres) => new Genres
						{	
							Id = genres.Id,
							Genre = genres.Genre,
							EventsGenres.IdEvents = eventGenres.IdEvents
						}
					)
					.ToList();
			}*/







			
			MySqlDataReader reader = null;

            try
            {
                InitDB();

				MySqlCommand command = new MySqlCommand();
                
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat(getRunningEventsQuery, Db.DbConfig.ConnectionPrefix);

				String commandText = sb.ToString();
				command.CommandType = System.Data.CommandType.Text;
				command.CommandText = commandText;

				reader = Db.ExecuteReader(command);
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						Events eventItem = new Events();

						eventItem.Id = reader.GetInt32(reader.GetOrdinal("ID"));
						eventItem.Name = reader.GetString(reader.GetOrdinal("NAME"));
						eventItem.Description = reader.GetString(reader.GetOrdinal("DESCRIPTION"));
						eventItem.DateSet = reader.GetDateTime(reader.GetOrdinal("DATE_SET"));
						eventItem.Running = reader.GetBoolean(reader.GetOrdinal("RUNNING"));
						if(!reader.IsDBNull(reader.GetOrdinal("PID"))) eventItem.Pid = reader.GetInt32(reader.GetOrdinal("PID"));
						if(!reader.IsDBNull(reader.GetOrdinal("PORT"))) eventItem.Port = reader.GetInt32(reader.GetOrdinal("PORT"));
						if(!reader.IsDBNull(reader.GetOrdinal("DATE_START"))) eventItem.DateStart = reader.GetDateTime(reader.GetOrdinal("DATE_START"));
						if(!reader.IsDBNull(reader.GetOrdinal("DATE_STOP"))) eventItem.DateStop = reader.GetDateTime(reader.GetOrdinal("DATE_STOP"));

						eventsList.Add(eventItem);
					}
				}
            }
            catch (Exception exc)
            {
				throw new Exception(exc.Message, exc);
            }
            finally
            {
                if (reader != null && !reader.IsClosed) {
					reader.Close();
					reader.Dispose();
				}
                ReleseDB();
            }

			return eventsList;
        }

		public List<Events> getGroupEvents(int groupId)
        {
			List<Events> eventsList = new List<Events>();
			DateTime localDate = DateTime.Now;

			using (var context = new ONSTAGEContext())
			{
				eventsList = context.Events
					.Where(x => x.IdGroups == groupId)
					.Where(x => x.DateSet >= localDate)
					.Select(x => x)
					.ToList();
			}

			return eventsList;
        }

		#endregion
    }
}
