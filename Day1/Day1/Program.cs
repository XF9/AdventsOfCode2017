using System;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sum = 0;
            var input = System.IO.File.ReadAllText(@"input.txt");

            for (var index = 0; index < input.Length; index++)
                if(input[index] == input[(index + 1) % input.Length])
                    sum += Int32.Parse(input[index].ToString());
            Console.WriteLine(sum);

            sum = 0;

            for (var index = 0; index < input.Length; index++)
                if (input[index] == input[(index + input.Length/2) % input.Length])
                    sum += Int32.Parse(input[index].ToString());
            Console.WriteLine(sum);

            Console.ReadKey();
        }
    }
}
