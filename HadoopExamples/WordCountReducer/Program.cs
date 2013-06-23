using System;
using System.Text.RegularExpressions;

namespace WordCountReducer
{
  class Program
  {
    static void Main(string[] args)
    {
      string line;
      var regex = new Regex("[a-zA-Z]+");
 
      // Einlesen des Hadoop Datenstroms
      while ((line = Console.ReadLine()) != null)
      {
        foreach (Match match in regex.Matches(line))
        {
          // Schreiben in den Hadoop Datenstrom
          Console.WriteLine("{0}\t1", match.Value.ToLower());
        }
      }
    }
}
