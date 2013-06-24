using System;

namespace WordCountReducer
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            string prevWord = null;
            int count = 0;

            while ((line = Console.ReadLine()) != null)
            {
                if (!line.Contains("\t")) { continue; }
                    
                var word = line.Split('\t')[0];
                var cnt = Convert.ToInt32(line.Split('\t')[1]);

                if (prevWord != word)
                {
                    if (prevWord != null) { 
                        Console.WriteLine("{0}\t{1}", prevWord, count);
                    }

                    prevWord = word;
                    count = cnt;
                }
                else
                    count += cnt;
            }

            Console.WriteLine("{0}\t{1}", prevWord, count);
        }
    }
}
