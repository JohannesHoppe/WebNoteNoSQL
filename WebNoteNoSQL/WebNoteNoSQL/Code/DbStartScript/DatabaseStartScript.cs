using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

namespace WebNoteNoSQL.Code.DbStartScript
{
    /// <summary>
    /// Normally you would use as a windows service,
    /// but for demonstrations a server in a console is much more "visible"
    /// </summary>
    public class DatabaseStartScript
    {
        private const string RedisProcessName = "redis-server";
        private const string RedisScriptName = "Redis_start.cmd";
        private const int RedisServerPort = 6379;

        private const string MongoDbProcessName = "mongod";
        private const string MongoDbScriptName = "MongoDB_start.cmd";
        private const int MongoDbServerPort = 27017;

        private const string RavenDbProcessName = "Raven.Server";
        private const string RavenDbScriptName = "RavenDb_start.cmd";
        private const int RavenDbServerPort = 8080;

        private DatabaseStartScript(string processName, string scriptName, int serverPort)
        {
            ProcessName = processName;
            ScriptPath = string.IsNullOrEmpty(scriptName)
                             ? String.Empty
                             : GetCurrentExecutingDirectory().FindFileRecursively(scriptName).GetFullPath();
            ServerPort = serverPort;
        }

        public string ProcessName { get; private set; }

        public string ScriptPath { get; private set; }

        public int ServerPort { get; private set; }

        public static DatabaseStartScript ForSqlServer()
        {
            return new DatabaseStartScript(string.Empty, string.Empty, 0);
        }

        public static DatabaseStartScript ForRedis()
        {
            return new DatabaseStartScript(RedisProcessName, RedisScriptName, RedisServerPort);
        }

        public static DatabaseStartScript ForMongoDb()
        {
            return new DatabaseStartScript(MongoDbProcessName, MongoDbScriptName, MongoDbServerPort);
        }

        public static DatabaseStartScript ForRavenDb()
        {
            return new DatabaseStartScript(RavenDbProcessName, RavenDbScriptName, RavenDbServerPort);
        }

        public static string GetCurrentExecutingDirectory()
        {
            string filePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetDirectoryName(filePath);
        }
    }
}