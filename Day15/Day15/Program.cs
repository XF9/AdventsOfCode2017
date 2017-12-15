using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    class Generator
    {
        private readonly int _acceptable;
        private readonly int _factor;
        private long Seed { get; set; }

        public Generator(int factor, int seed, int acceptable = -1)
        {
            _acceptable = acceptable;
            _factor = factor;
            Seed = seed;
        }

        public int Next()
        {
            do
            {
                Seed *= _factor;
                Seed %= 2147483647;
            } while (_acceptable != -1 && Seed % _acceptable != 0);
            return (int)Seed;
        }
    }

    class Program
    {
        static string ToBits(int value)
        {
            return Convert.ToString(value, 2);
        }

        static string GetLowestBits(Generator generator, int length)
        {
            var bitstring = ToBits(generator.Next());
            bitstring = bitstring.PadLeft(length, '0');
            return bitstring.Substring(bitstring.Length - length);
        }

        static void Main(string[] args)
        {
            // Teil 1
            var a = new Generator(16807, 516);
            var b = new Generator(48271, 190);
            var matchingBits = 0;

            for (int i = 0; i < 40000000; i++)
                if(GetLowestBits(a, 16) == GetLowestBits(b, 16))
                    matchingBits++;

            Console.WriteLine(matchingBits);

            // Teil 2
            a = new Generator(16807, 516, 4);
            b = new Generator(48271, 190, 8);
            matchingBits = 0;

            for (int i = 0; i < 5000000; i++)
                if (GetLowestBits(a, 16) == GetLowestBits(b, 16))
                    matchingBits++;

            Console.WriteLine(matchingBits);
            Console.ReadKey();
        }
    }
}
