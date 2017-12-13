using System;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    class Program
    {
        public struct Layer
        {
            public int Depth;
            public int Range;
        }

        public static Layer ParseLine(string line)
        {
            var seperator = line.IndexOf(":");
            return new Layer
            {
                Depth = Int32.Parse(line.Substring(0, seperator)),
                Range = Int32.Parse(line.Substring(seperator + 2))
            };
        }

        public static (int severity, bool caught) CalculateSeverity(IEnumerable<Layer> layers, int delay = 0)
        {
            var severity = 0;
            bool caught = false;
            foreach (var layer in layers)
            {
                if ((delay + layer.Depth) % ((layer.Range - 1) * 2) == 0)
                {
                    severity += layer.Range * layer.Depth;
                    caught = true;
                }
            }
            return (severity, caught);
        }

        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines("input.txt").Select(ParseLine);

            Console.WriteLine(CalculateSeverity(input).severity);

            var delay = 0;
            while (CalculateSeverity(input, delay).caught)
                delay++;

            Console.WriteLine(delay);

            Console.ReadKey();
        }
    }
}
