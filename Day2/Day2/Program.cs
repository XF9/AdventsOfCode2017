using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    class Program
    {
        private static IEnumerable<int> ToIntList(string line)
        {
            var split = line.Split(new Char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var stringValue in split)
                if (Int32.TryParse(stringValue, out var intValue))
                    yield return intValue;
        }

        static void Main(string[] args)
        {
            var input = System.IO.File.ReadLines(@"input.txt").ToList();

            var checksum = 0;
            foreach (var line in input)
            {
                var values = ToIntList(line).ToList();
                checksum += values.Max() - values.Min();
            }

            Console.WriteLine(checksum);

            checksum = 0;
            foreach (var line in input)
            {
                var values = ToIntList(line).ToList();
                foreach (var firstValue in values)
                    foreach(var secondValue in values)
                        if (firstValue != secondValue && firstValue % secondValue == 0)
                            checksum += firstValue / secondValue;
            }

            Console.WriteLine(checksum);

            Console.ReadKey();
        }
    }
}
