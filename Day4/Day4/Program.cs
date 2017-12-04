using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var valid = 0;
            var input = System.IO.File.ReadLines(@"input.txt").ToList();

            foreach (var line in input)
            {
                var words = line.Split(new Char[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (words.Count == words.Distinct().Count())
                    valid++;
            }

            Console.WriteLine(valid);

            valid = 0;
            foreach (var line in input)
            {
                var words = line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var orderedWords = new List<String>();
                foreach(var word in words)
                    orderedWords.Add(String.Concat(word.OrderBy(c => c)));

                if (orderedWords.Count == orderedWords.Distinct().Count())
                    valid++;
            }

            Console.WriteLine(valid);
            Console.ReadKey();
        }
    }
}
