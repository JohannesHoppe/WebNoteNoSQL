using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordCountMapper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string line;
            var regex = new Regex("[a-zA-Z]+");

            while ((line = Console.ReadLine()) != null)
            {
                foreach (Match match in regex.Matches(line))
                {
                    Console.WriteLine("{0}\t1", match.Value.ToLower());
                }
            }
        }

        static IConsole _console;
        public static IConsole Console
        {
            get { return _console ?? (_console = new ConsoleWrapper()); }
            set { _console = value; }
        }
    }
}
