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
    public class HomeDataAccess
    {
		#region Proprietà

        public IDatabase Db { get; set; }
        
		#endregion

		#region SQL Command Source

		private const String getRunningEventsQuery = @"
			SELECT *
			FROM {0}EVENTS
			WHERE RUNNING = 1
		";
		
		private const String setStopEventQuery = @"
			UPDATE {0}EVENTS
			SET
				RUNNING = 0,
				DATE_STOP = NOW()
			WHERE ID = @ID
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

        public Events getEventById(int eventId)
        {
			Events eventItem = new Events();

			using (var context = new onliveContext())
			{
				eventItem = context.Events
					.Where(x => x.Id == eventId)
					.FirstOrDefault();
			}

            return eventItem;
		}

        public List<Events> getRunningEvents()
        {
			MySqlDataReader reader = null;
			List<Events> eventLst = new List<Events>();

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

						eventLst.Add(eventItem);
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

            return eventLst;
		}

        public Events createEvent(Events eventItem)
        {
			Events insEvent = new Events();

			using (var context = new onliveContext())
			{
				insEvent.Name = eventItem.Name;
				insEvent.Description = eventItem.Description;
				insEvent.DateSet = eventItem.DateSet;

				context.Events.Add(insEvent);
				context.SaveChanges();
			}

			return insEvent;
        }

        public int calculatePort()
        {
			int port = -1;

			using (var context = new onliveContext())
			{
				Jports jports = context.Jports
					.Where(x => x.Running == false)
					.FirstOrDefault();

				jports.Running = true;
				context.SaveChanges();

				port = jports.Port;
			}

			return port;
        }

        public void freePort(int eventId)
        {
			using (var context = new onliveContext())
			{
				Events eventItem = context.Events
					.Where(x => x.Id == eventId)
					.FirstOrDefault();

				Jports jports = context.Jports
					.Where(x => x.Port == eventItem.Port)
					.FirstOrDefault();

				jports.Running = false;
				context.SaveChanges();
			}
        }

        public void setPort(int eventId, int port)
        {
			using (var context = new onliveContext())
			{
				Events updevent = context.Events
					.Where(x => x.Id == eventId)
					.FirstOrDefault();

				updevent.Port = port;
				context.SaveChanges();
			}
        }

        public void setStartEvent(int eventId, int pid)
        {
			using (var context = new onliveContext())
			{
				Events updEvent = context.Events
					.Where(x => x.Id == eventId)
					.FirstOrDefault();
				
				updEvent.Pid = pid;
				updEvent.DateStart = DateTime.Now;
				updEvent.Running = true;

				context.SaveChanges();
			}
        }

        public void setStopEvent(int eventId)
        {
			try
			{
            	MySqlCommand command = new MySqlCommand();

                InitDB();
                
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat(setStopEventQuery, Db.DbConfig.ConnectionPrefix);

				String commandText = sb.ToString();
				command.CommandType = System.Data.CommandType.Text;
				command.CommandText = commandText;
				
				DatabaseConfig databaseConfig = new DatabaseConfig();
				databaseConfig.AddParameterToCommand(command, "@ID", MySqlDbType.Int32, eventId);

				Db.ExecuteNonQuery(command);
            }
            catch (Exception exc)
            {
				throw new Exception(exc.Message, exc);
            }
            finally
            {
                ReleseDB();
            }
		}
		
		#endregion
    }
}
