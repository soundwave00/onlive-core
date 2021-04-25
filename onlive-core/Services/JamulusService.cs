using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

using onlive_core.DataAccess;
using onlive_core.DbModels;
using onlive_core.Entities;

namespace onlive_core.Services
{
    public class JamulusService
    {
        #region Metodi pubblici

        public Response startLive(Live req, string bash = null)
        {
			Response response = new Response();

			//CHECK SESSION(?)
			/* */

			JamulusDataAccess jamulusDataAccess = new JamulusDataAccess();

			//TMP CREATE LIVE
			Live live = jamulusDataAccess.createLive(req);
			
			int port = setPort(live.Id);

			int pid = startJamulusProcess(port, bash);
			
			jamulusDataAccess.setStartLive(live.Id, pid);

			response.rMessage = "Live " + live.Name + " started";

			return response;
        }
		
        public Response stopLive(Live req)
        {
			Response response = new Response();

			JamulusDataAccess jamulusDataAccess = new JamulusDataAccess();
			Live live = jamulusDataAccess.getLiveById(req.Id);

			try
			{
				stopJamulusProcess((int)live.Pid);
			}
			catch (Exception exc) {}

			jamulusDataAccess.setStopLive(req.Id);

			jamulusDataAccess.freePort(req.Id);

			response.rMessage = "Live " + live.Name + " stopped";

            return response;
		}
		
        public Response stopAllLive()
        {
			Response response = new Response();

			JamulusDataAccess jamulusDataAccess = new JamulusDataAccess();
			List<Live> lstLive = jamulusDataAccess.getRunningLive();

			foreach(Live live in lstLive){
				try
				{
					stopJamulusProcess((int)live.Pid);
				}
				catch (Exception exc) {}

				jamulusDataAccess.setStopLive(live.Id);

				jamulusDataAccess.freePort(live.Id);
			}

			response.rMessage = "All live stopped";

            return response;
		}

		#endregion

		#region Metodi privati

        public int setPort(int liveId)
        {
			int port = -1;

			JamulusDataAccess jamulusDataAccess = new JamulusDataAccess();
			port = jamulusDataAccess.calculatePort();

			if (port < 0)
				throw new Exception("Port not found");

			jamulusDataAccess.setPort(liveId, port);

			return port;
		}

		public int startJamulusProcess(int port, string bash)
		{
			string command = "/home/riddorck/sviluppi/jamulus/Jamulus -s -n -F -T --streamto {PORT} '-f mp3 icecast://source:root@localhost:80/stream'";
			if (!string.IsNullOrEmpty(bash))
				command = bash;

			string strPort = port > 0 ? new String("-p " + port.ToString()) : "";
			command = command.Replace("{PORT}", strPort);

            command = "-c \"" + command + " & echo $!\"";

			Process process = new Process();
			process.StartInfo.FileName = "/bin/bash";
			process.StartInfo.Arguments = command;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.CreateNoWindow = true;
			process.Start();

			string pidStr = process.StandardOutput.ReadLine();
			int pid = string.IsNullOrEmpty(pidStr) ? -1 : int.Parse(pidStr);

			if (pid < 0)
				throw new Exception("Process not started");

			return pid;
		}

        public void stopJamulusProcess(int pid)
        {
			Process process = Process.GetProcessById(pid);
			process.Kill();
		}
		
		#endregion
    }
}
