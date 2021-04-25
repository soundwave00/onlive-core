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

		private const String setStartLiveQuery = @"
			UPDATE {0}LIVE 
			SET
				PID = @PID,
				DATE_START = NOW(),
				RUNNING = 1
			WHERE ID = @ID
		";

		private const String getRunningLiveQuery = @"
			SELECT PID FROM {0}LIVE
			WHERE RUNNING = 1
		";
		
		private const String setStopLiveQuery = @"
			UPDATE {0}LIVE
			SET
				RUNNING = 0,
				DATE_STOP = NOW()
			WHERE PID = @PID
		";

		#endregion

		#region InitDB

        private void InitDB()
        {
            if (Db == null)
            {
				Db = new IDatabase();
                Db.Open();
            }
        }

        #endregion

        #region ReleseDB

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

        public List<int> getRunningLive()
        {
			List<int> pids = new List<int>();
			MySqlDataReader reader = null;

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
						int pid = reader.GetInt32(reader.GetOrdinal("PID"));
						pids.Add(pid);
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

            return pids;
		}

        public void setStartLive(int pid)
        {
			int id = 0;

			//TMP CREAZIONE LIVE
			using (var context = new onliveContext())
			{
				Live live = new Live();
				live.Name = "Prova";
				live.Description = "Prova Descrizione";
				live.DateSet = new DateTime();
				live.DateStart = null;
				live.Pid = null;
				live.Running = false;

				context.Live.Add(live);
				context.SaveChanges();

				id = live.Id;
			}
			//TMP CREAZIONE LIVE

			using (var context = new onliveContext())
			{
				Live live = context.Live
					.Where(x => x.Id == id)
					.FirstOrDefault();
				
				live.Pid = pid;
				live.DateStart = new DateTime();
				live.Running = true;

				context.SaveChanges();
			}
        }

        public void setStopLive(int pid)
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
				databaseConfig.AddParameterToCommand(command, "@PID", MySqlDbType.Int32, pid);

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
