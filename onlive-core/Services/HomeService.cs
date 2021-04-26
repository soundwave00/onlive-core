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
    public class HomeService
    {
        #region Metodi pubblici

        public Response startEvent(Events req, string bash = null)
        {
			Response response = new Response();

			//CHECK SESSION(?)
			/* */

			HomeDataAccess homeDataAccess = new HomeDataAccess();

			//TMP CREATE EVENT
			Events eventItem = homeDataAccess.createEvent(req);
			
			int port = setPort(eventItem.Id);

			int pid = startHomeProcess(port, bash);
			
			homeDataAccess.setStartEvent(eventItem.Id, pid);

			response.rMessage = "Event " + eventItem.Name + " started";

			return response;
        }
		
        public Response stopEvent(Events req)
        {
			Response response = new Response();

			HomeDataAccess homeDataAccess = new HomeDataAccess();
			Events eventItem = homeDataAccess.getEventById(req.Id);

			try
			{
				stopHomeProcess((int)eventItem.Pid);
			}
			catch (Exception exc) {}

			homeDataAccess.setStopEvent(req.Id);

			homeDataAccess.freePort(req.Id);

			response.rMessage = "Event " + eventItem.Name + " stopped";

            return response;
		}
		
        public Response stopAllEvents()
        {
			Response response = new Response();

			HomeDataAccess homeDataAccess = new HomeDataAccess();
			List<Events> eventLst = homeDataAccess.getRunningEvents();

			foreach(Events eventItem in eventLst){
				try
				{
					stopHomeProcess((int)eventItem.Pid);
				}
				catch (Exception exc) {}

				homeDataAccess.setStopEvent(eventItem.Id);

				homeDataAccess.freePort(eventItem.Id);
			}

			response.rMessage = "All event stopped";

            return response;
		}

		#endregion

		#region Metodi privati

        public int setPort(int eventId)
        {
			int port = -1;

			HomeDataAccess homeDataAccess = new HomeDataAccess();
			port = homeDataAccess.calculatePort();

			if (port < 0)
				throw new Exception("Port not found");

			homeDataAccess.setPort(eventId, port);

			return port;
		}

		public int startHomeProcess(int port, string bash)
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

        public void stopHomeProcess(int pid)
        {
			Process process = Process.GetProcessById(pid);
			process.Kill();
		}
		
		#endregion
    }
}
