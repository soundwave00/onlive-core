using System;
using onlive_core.DataAccess;
using onlive_core.DbModels;
using onlive_core.Entities;
using onlive_core.Models;
using System.Collections.Generic;

using System.Diagnostics; // Per il Process

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
                throw new Exception("Error getting event", exc.InnerException);
            }
			
			if (eventItem == null)
				throw new Exception("Event does not exist");

			getEventResponse.eventItem = eventItem;

			return getEventResponse;
        }

		public GetEventsResponse getEvents(GetEventsRequest req)
        {
			GetEventsResponse getEventsResponse = new GetEventsResponse();

			List<Events> eventsList = new List<Events>();

			try
			{
				EventDataAccess eventDataAccess = new EventDataAccess();
				eventsList = eventDataAccess.getEvents(req.dateFrom);
			}
			catch (Exception exc)
            {
                throw new Exception("Error getting event", exc.InnerException);
            }
			
			if (eventsList == null)
				throw new Exception("Event does not exist");

			getEventsResponse.eventsList = eventsList;

			return getEventsResponse;
        }

		public GetEventsResponse getGroupEvents(GetEventsRequest req)
        {
			GetEventsResponse getEventsResponse = new GetEventsResponse();

			List<Events> eventsList = new List<Events>();

			try
			{
				EventDataAccess eventDataAccess = new EventDataAccess();
				eventsList = eventDataAccess.getGroupEvents(req.groupId);
			}
			catch (Exception exc)
            {
                throw new Exception("Error getting event", exc.InnerException);
            }
			
			if (eventsList == null)
				throw new Exception("Event does not exist");

			getEventsResponse.eventsList = eventsList;

			return getEventsResponse;
        }

		public void createEvent(GetEventsRequest req)
        {
			EventDataAccess eventDataAccess = new EventDataAccess();

			try
			{
				eventDataAccess.createEvent(req.events, req.genres);
			}
			catch (Exception exc)
            {
                throw new Exception("Error getting genres", exc.InnerException);
            }
		}

		public Response startEvent(GetEventRequest req)
        {
			Response response = new Response();

			EventDataAccess eventDataAccess = new EventDataAccess();
			
			int port = setPort(req.id);
			int pid = startHomeProcess(port);
			
			eventDataAccess.setStartEvent(req.id, pid);

			response.rMessage = "Event started";

			return response;
        }

		public Response stopEvent(GetEventRequest req)
        {
			Response response = new Response();

			EventDataAccess eventDataAccess = new EventDataAccess();
			Events	eventItem = eventDataAccess.getEvent(req.id);

			try
			{
				stopHomeProcess((int)eventItem.Pid);
			}
			catch (Exception exc) {}

			eventDataAccess.setStopEvent(req.id);

			eventDataAccess.freePort(req.id);

			response.rMessage = "Event " + eventItem.Name + " stopped";

            return response;
		}

		#endregion

		#region Metodi privati
		public int setPort(int eventId)
        {
			int port = -1;

			EventDataAccess eventDataAccess = new EventDataAccess();
			port = eventDataAccess.calculatePort();

			if (port < 0)
				throw new Exception("Port not found");

			eventDataAccess.setPort(eventId, port);

			return port;
		}

		public int startHomeProcess(int port)
		{
			string command = "/home/soundwave/jamulus/Jamulus -s -n -F -T {PORT} --streamto '-f mp3 icecast://source:Tesisoundwave00@localhost:8000/{PORT2}'";
			/* if (!string.IsNullOrEmpty(bash))
				command = bash; */

			string strPort = port > 0 ? new String("-p " + port.ToString()) : "";
			command = command.Replace("{PORT}", strPort);

			command = command.Replace("{PORT2}", "onStage");
			
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
