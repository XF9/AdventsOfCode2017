using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var instructions = new List<int>();

            var input = System.IO.File.ReadLines(@"input.txt").ToList();
            foreach(var line in input)
                if(Int32.TryParse(line,out var value))
                    instructions.Add(value);

            // Teil 1

            var index = 0;
            var jumpCounter = 0;
            var instructionsArray = instructions.ToArray();
            while (index >= 0 && index < instructionsArray.Length)
            {
                instructionsArray[index]++;
                index += instructionsArray[index] - 1;
                jumpCounter++;
            }

            Console.WriteLine(jumpCounter);

            // Teil 2

            index = 0;
            jumpCounter = 0;
            instructionsArray = instructions.ToArray();
            while (index >= 0 && index < instructionsArray.Length)
            {
                var offset = instructionsArray[index];
                instructionsArray[index] = offset >= 3 ? offset - 1 : offset + 1;
                index += offset;
                jumpCounter++;
            }

            Console.WriteLine(jumpCounter);

            Console.ReadKey();
        }
    }
}
