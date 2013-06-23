using System;

namespace WordCountMapper
{
    public class ConsoleWrapper : IConsole
    {
        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
