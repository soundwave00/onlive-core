using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace onlive_core.Controllers
{
    [Produces("application/json")]
    [Route("api/Prova")]
    public class ProvaController : ControllerBase
    {
        private readonly ILogger<ProvaController> _logger;

        public ProvaController(ILogger<ProvaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [HttpOptions]
        [Route("prova")]
        public string prova()
        {
            launchProcess("gnome-terminal");
            //launchProcess("\"/home/riddorck/sviluppi/jamulus/Jamulus -s -n -F -T --streamto '-f mp3 icecast://source:root@localhost:80/stream\'\"");

            return "prova";
        }

        public static void launchProcess(string cmdInput)
        {
            string command = "-c " + cmdInput;

            Process process = new Process();
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = command;
            process.StartInfo.UseShellExecute = false;
            process.Start();

            Console.WriteLine(command);
        }
    }
}
