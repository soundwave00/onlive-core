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
    public class JamulusDataAccess
    {
		#region Proprietà

        public IDatabase Db { get; set; }
        
		#endregion

		#region SQL Command Source

		private const String getRunningLiveQuery = @"
			SELECT *
			FROM {0}LIVE
			WHERE RUNNING = 1
		";
		
		private const String setStopLiveQuery = @"
			UPDATE {0}LIVE
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

        public Live getLiveById(int liveId)
        {
			Live live = new Live();

			using (var context = new onliveContext())
			{
				live = context.Live
					.Where(x => x.Id == liveId)
					.FirstOrDefault();
			}

            return live;
		}

        public List<Live> getRunningLive()
        {
			MySqlDataReader reader = null;
			List<Live> lstLive = new List<Live>();

            try
            {
                InitDB();

				MySqlCommand command = new MySqlCommand();
                
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat(getRunningLiveQuery, Db.DbConfig.ConnectionPrefix);

				String commandText = sb.ToString();
				command.CommandType = System.Data.CommandType.Text;
				command.CommandText = commandText;

				reader = Db.ExecuteReader(command);
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						Live live = new Live();

						live.Id = reader.GetInt32(reader.GetOrdinal("ID"));
						live.Name = reader.GetString(reader.GetOrdinal("NAME"));
						live.Description = reader.GetString(reader.GetOrdinal("DESCRIPTION"));
						live.DateSet = reader.GetDateTime(reader.GetOrdinal("DATE_SET"));
						live.Running = reader.GetBoolean(reader.GetOrdinal("RUNNING"));
						if(!reader.IsDBNull(reader.GetOrdinal("PID"))) live.Pid = reader.GetInt32(reader.GetOrdinal("PID"));
						if(!reader.IsDBNull(reader.GetOrdinal("PORT"))) live.Port = reader.GetInt32(reader.GetOrdinal("PORT"));
						if(!reader.IsDBNull(reader.GetOrdinal("DATE_START"))) live.DateStart = reader.GetDateTime(reader.GetOrdinal("DATE_START"));
						if(!reader.IsDBNull(reader.GetOrdinal("DATE_STOP"))) live.DateStop = reader.GetDateTime(reader.GetOrdinal("DATE_STOP"));

						lstLive.Add(live);
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

            return lstLive;
		}

        public Live createLive(Live live)
        {
			Live insLive = new Live();

			using (var context = new onliveContext())
			{
				insLive.Name = live.Name;
				insLive.Description = live.Description;
				insLive.DateSet = live.DateSet;

				context.Live.Add(insLive);
				context.SaveChanges();
			}

			return insLive;
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

        public void freePort(int liveId)
        {
			using (var context = new onliveContext())
			{
				Live live = context.Live
					.Where(x => x.Id == liveId)
					.FirstOrDefault();

				Jports jports = context.Jports
					.Where(x => x.Port == live.Port)
					.FirstOrDefault();

				jports.Running = false;
				context.SaveChanges();
			}
        }

        public void setPort(int liveId, int port)
        {
			using (var context = new onliveContext())
			{
				Live updlive = context.Live
					.Where(x => x.Id == liveId)
					.FirstOrDefault();

				updlive.Port = port;
				context.SaveChanges();
			}
        }

        public void setStartLive(int liveId, int pid)
        {
			using (var context = new onliveContext())
			{
				Live updLive = context.Live
					.Where(x => x.Id == liveId)
					.FirstOrDefault();
				
				updLive.Pid = pid;
				updLive.DateStart = DateTime.Now;
				updLive.Running = true;

				context.SaveChanges();
			}
        }

        public void setStopLive(int liveId)
        {
			try
			{
            	MySqlCommand command = new MySqlCommand();

                InitDB();
                
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat(setStopLiveQuery, Db.DbConfig.ConnectionPrefix);

				String commandText = sb.ToString();
				command.CommandType = System.Data.CommandType.Text;
				command.CommandText = commandText;
				
				DatabaseConfig databaseConfig = new DatabaseConfig();
				databaseConfig.AddParameterToCommand(command, "@ID", MySqlDbType.Int32, liveId);

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
