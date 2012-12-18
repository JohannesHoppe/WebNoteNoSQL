using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace WebNoteNoSQL.Code.DbStartScript
{
    using System.Threading;

    /// <summary>
    /// Normally you would use as a windows service,
    /// but for demonstrations a server in a console is much more "visible"
    /// </summary>
    public class DatabaseServerProcess
    {
        private readonly DatabaseStartScript script;

        public DatabaseServerProcess(DatabaseStartScript script)
        {
            this.script = script;
        }

        public void Start()
        {
            if (string.IsNullOrEmpty(script.ScriptPath) || ProcessIsRunning(script.ProcessName))
            {
                return;
            }

            if (!IsServerPortAvailable(script.ServerPort))
            {
                throw new ArgumentException(String.Format("Port {0} is already blocked. So it won't be possible to start {1}.", script.ServerPort, script.ProcessName));
            }

            StartServer(script.ScriptPath);
        }

        private static bool ProcessIsRunning(string processName)
        {
            return Process.GetProcessesByName(processName).Any();
        }

        /// <summary>
        /// Evaluate current system tcp connections
        /// </summary>
        private static bool IsServerPortAvailable(int portNumber)
        {
            IPEndPoint[] tcpConnInfoArray = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
            return tcpConnInfoArray.All(endpoint => endpoint.Port != portNumber);
        }

        private static void StartServer(string scriptPath)
        {
            string workingDirectory = Path.GetDirectoryName(scriptPath) ?? String.Empty;

            ProcessStartInfo startInfo = new ProcessStartInfo(scriptPath)
                {
                    ErrorDialog = true,
                    WorkingDirectory = workingDirectory
                };

            Process.Start(startInfo);
            Thread.Sleep(1000);
        }
    }
}