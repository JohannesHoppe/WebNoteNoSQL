using System;
using System.Text.RegularExpressions;

namespace WordCountReducer
{
    internal class Program
    {
        private static void Main(string[] args)
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
    }
}
