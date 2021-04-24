using System;
using System.Collections.Generic;
using System.Diagnostics;

using onlive_core.DataAccess;

namespace onlive_core.Services
{
    public class JamulusService
    {
        #region Metodi

        public string testLive()
        {
			List<int> pids = new List<int>();
            
			pids.Add(startLive("vlc"));
            //pids.Add(startLive("/home/riddorck/sviluppi/jamulus/Jamulus -s -n -F -T --streamto '-f mp3 icecast://source:root@localhost:80/stream'"));
            //pids.Add(startLive("/home/riddorck/sviluppi/jamulus/Jamulus -s -n -F -T -p 22126 --streamto '-f mp3 icecast://source:root@localhost:80/stream'"));
            //pids.Add(startLive("/home/riddorck/sviluppi/jamulus/Jamulus -s -n -F -T -p 22127 --streamto '-f mp3 icecast://source:root@localhost:80/stream'"));

            return "Opended pids: " + string.Join(",", pids);
        }
		
        public string stopAllLive()
        {
			JamulusDataAccess jamulusDataAccess = new JamulusDataAccess();
			List<int> pids = jamulusDataAccess.getRunningLive();

            foreach(int pid in pids){
				stopLive(pid);
			}

            return "Killed pids: " + string.Join(",", pids);
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
            try
			{
				Process process = Process.GetProcessById(pid);
				process.Kill();
			}
            catch (Exception exc)
            {
				Console.WriteLine("Errore durante il metodo kill: " + exc);
                //throw new Exception("Errore durante il metodo kill", exc);
            }

			JamulusDataAccess jamulusDataAccess = new JamulusDataAccess();
			jamulusDataAccess.setStopLive(pid);
		}
		
		#endregion
    }
}
