namespace WebNoteNoSQL.Code.DbStartScript
{
    using System.IO;

    public static class DatabaseStartScriptStringExtensios
    {
        private const int MaxLevelOfRecursiveSearchesForScript = 4;

        public static string FindFileRecursively(this string scriptPath, string scriptName, int currentLevel = 0)
        {
            if (currentLevel >= MaxLevelOfRecursiveSearchesForScript)
            {
                throw new FileNotFoundException(
                    "The start script was not found. Last try: " + Path.GetFullPath(scriptPath));
            }

            if (File.Exists(scriptPath + "\\" + "Databases\\" + scriptName))
            {
                return scriptPath + "\\" + "Databases\\" + scriptName;
            }

            return FindFileRecursively(scriptPath + "\\..", scriptName, currentLevel + 1);
        }

        public static string GetFullPath(this string path)
        {
            return Path.GetFullPath(path);
        }
    }
}