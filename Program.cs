using System;
using System.Diagnostics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace onlive_core
{
    public class Program
    {
        public static void Main(string[] args)
        {
						//BuildWebHost(args).Run();

						launchProcess("gnome-terminal");
						launchProcess("gnome-terminal");
						launchProcess("gnome-terminal");
						
						//"-c /home/riddorck/sviluppi/jamulus/Jamulus -s -n -F -T --streamto \"-f mp3 icecast://source:root@localhost:80/stream\""
        }

				/*
				public static IWEBHost BuildWebHost(string[] args) =>
						WebHost.CreateDefaultBuilder(args)
								.UseStartup<Startup>()
								.UseIISIntegration()
								.Build();
				*/

				public static void launchProcess(string cmdInput){
						string command = "-c " + cmdInput;

						Process process = new Process();
						process.StartInfo.FileName = "/bin/bash";
						process.StartInfo.Arguments = command;
						process.StartInfo.UseShellExecute = false;
						//process.StartInfo.RedirectStandardOutput = true;
        		//process.StartInfo.CreateNoWindow = true;
						process.Start();
						Console.WriteLine(command);

						/*
						while (!process.StandardOutput.EndOfStream)
						{
								string line = process.StandardOutput.ReadLine();
								Console.WriteLine(line);
						}
						*/
				}
    }
}
