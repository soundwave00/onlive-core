using System;
using System.Collections.Generic;
using System.Diagnostics;

using onlive_core.DataAccess;
using onlive_core.DbModels;

namespace onlive_core.Services
{
    public class JamulusService
    {
        #region Metodi

        public string testLive(Live req)
        {
			string response;
            
			List<int> pids = new List<int>();

			if(req.Name == "vlc") //TMP
			pids.Add(startLive(req.Name));
			//pids.Add(startLive("/home/riddorck/sviluppi/jamulus/Jamulus -s -n -F -T --streamto '-f mp3 icecast://source:root@localhost:80/stream'"));
			//pids.Add(startLive("/home/riddorck/sviluppi/jamulus/Jamulus -s -n -F -T -p 22126 --streamto '-f mp3 icecast://source:root@localhost:80/stream'"));
			//pids.Add(startLive("/home/riddorck/sviluppi/jamulus/Jamulus -s -n -F -T -p 22127 --streamto '-f mp3 icecast://source:root@localhost:80/stream'"));
		
			if(pids.Count > 0)
				response = "Started live pids: " + string.Join(",", pids);
			else
				response = "No started live";

            return response;
        }
		
        public string stopAllLive()
        {
			string response;

			JamulusDataAccess jamulusDataAccess = new JamulusDataAccess();
			List<int> pids = jamulusDataAccess.getRunningLive();

			foreach(int pid in pids){
				stopLive(pid);
			}

			if(pids.Count > 0)
				response = "Stopped live pids: " + string.Join(",", pids);
			else
				response = "No stopped live";

            return response;
		}

        public int startLive(string inputBash)
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
			
			JamulusDataAccess jamulusDataAccess = new JamulusDataAccess();
			jamulusDataAccess.setStartLive(pid);

			return pid;
        }

        public void stopLive(int pid)
        {
			Process process = Process.GetProcessById(pid);
			process.Kill();

			JamulusDataAccess jamulusDataAccess = new JamulusDataAccess();
			jamulusDataAccess.setStopLive(pid);
		}
		
		#endregion
    }
}
