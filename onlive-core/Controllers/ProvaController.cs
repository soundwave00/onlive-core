using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using onlive_core.Db;
using MySql.Data.MySqlClient;

namespace onlive_core.Controllers
{
    [Produces("application/json")]
    [Route("api/Prova")]
    public class ProvaController : ControllerBase
    {
        #region Proprietà
        private readonly ILogger<ProvaController> _logger;
        public IDatabase Db { get; set; }
        #endregion
				
		#region Costruttori
        public ProvaController(ILogger<ProvaController> logger)
        {
        	_logger = logger;
        }
		#endregion

		#region SQL Command Source
		private const String insertPidQuery = @"
			INSERT INTO {0}LIVE (PID)
			VALUES (@PID)
		";
		private const String getPidsQuery = @"
			SELECT * FROM {0}LIVE
		";
		private const String deletePidsQuery = @"
			DELETE FROM {0}LIVE
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
        [HttpGet]
        [HttpOptions]
        [Route("run")]
        public string run()
        {
			List<int> pids = new List<int>();
            
			pids.Add(runProcess("gnome-terminal"));
            //pids.Add(runProcess("/home/riddorck/sviluppi/jamulus/Jamulus -s -n -F -T --streamto '-f mp3 icecast://source:root@localhost:80/stream'"));
            //pids.Add(runProcess("/home/riddorck/sviluppi/jamulus/Jamulus -s -n -F -T -p 22126 --streamto '-f mp3 icecast://source:root@localhost:80/stream'"));
            //pids.Add(runProcess("/home/riddorck/sviluppi/jamulus/Jamulus -s -n -F -T -p 22127 --streamto '-f mp3 icecast://source:root@localhost:80/stream'"));

            return "Opended pids: " + string.Join(",", pids);
        }

        [HttpGet]
        [HttpOptions]
        [Route("kill")]
        public string kill()
        {
			List<int> pids = new List<int>();

            MySqlCommand command = new MySqlCommand();
            MySqlDataReader reader = null;

            try
            {
                InitDB();
                
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat(getPidsQuery, Db.DbConfig.ConnectionPrefix);

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
                throw new Exception("Errore in getPidsQuery durante il metodo kill", exc);
            }
            finally
            {
                if (reader != null && !reader.IsClosed) {
					reader.Close();
					reader.Dispose();
				}
                ReleseDB();
            }

            foreach(int pid in pids){
				killProcess(pid);
			}

            return "Killed pids: " + string.Join(",", pids);
		}

        public int runProcess(string inputBash)
        {
            string commandBash = "-c \"" + inputBash + " & echo $!\"";

			string pidStr;
			int pid;

            Process process = new Process();
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = commandBash;
            process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
        	process.StartInfo.CreateNoWindow = true;
            process.Start();

			pidStr = process.StandardOutput.ReadLine();
			pid = int.Parse(pidStr);

            MySqlCommand command = new MySqlCommand();

            try
            {
                InitDB();
                
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat(insertPidQuery, Db.DbConfig.ConnectionPrefix);

				String commandText = sb.ToString();
				command.CommandType = System.Data.CommandType.Text;
				command.CommandText = commandText;
				
				DatabaseConfig databaseConfig = new DatabaseConfig();
				databaseConfig.AddParameterToCommand(command, "@PID", MySqlDbType.Int32, pid);

				Db.ExecuteReader(command);
            }
            catch (Exception exc)
            {
                throw new Exception("Errore in insertPidQuery durante il metodo run", exc);
            }
            finally
            {
                ReleseDB();
            }

			return pid;
        }

        public void killProcess(int pid)
        {
			Process process = Process.GetProcessById(pid);
            process.Kill();

            MySqlCommand command = new MySqlCommand();

            try
            {
                InitDB();
                
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat(deletePidsQuery, Db.DbConfig.ConnectionPrefix);

				String commandText = sb.ToString();
				command.CommandType = System.Data.CommandType.Text;
				command.CommandText = commandText;
				
				DatabaseConfig databaseConfig = new DatabaseConfig();
				databaseConfig.AddParameterToCommand(command, "@PID", MySqlDbType.Int32, pid);

				Db.ExecuteReader(command);
            }
            catch (Exception exc)
            {
                throw new Exception("Errore in deletePidsQuery durante il metodo kill", exc);
            }
            finally
            {
                ReleseDB();
            }
		}
		#endregion
    }
}
