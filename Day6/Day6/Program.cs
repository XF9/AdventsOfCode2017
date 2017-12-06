using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var blocks = new List<int>();
            var line = System.IO.File.ReadAllLines(@"input.txt")[0];
            foreach (var stringValue in line.Split(new Char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries))
                if (Int32.TryParse(stringValue, out var intValue))
                    blocks.Add(intValue);

            var state = blocks.ToArray();
            var states = new List<Int32[]>();

            while (!states.Any(s => s.SequenceEqual(state)))
            {
                states.Add((Int32[])state.Clone());
                var distributionIndex = Array.IndexOf(state, state.Max());

                var value = state[distributionIndex];
                state[distributionIndex] = 0;
                while (value > 0)
                {
                    value--;
                    distributionIndex = (distributionIndex + 1) % state.Length;
                    state[distributionIndex]++;
                }
            }

            Console.WriteLine(states.Count);
            Console.WriteLine(states.Count - states.IndexOf(states.Single(s => s.SequenceEqual(state))));
            Console.ReadKey();
        }
    }
}
