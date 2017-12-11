using System;
using System.Collections.Generic;
using System.Linq;

namespace Day11
{
    class Program
    {
        struct Vector3
        {
            public int N_S;
            public int NE_SW;
            public int NW_SE;
        }

        static int Distance(Vector3 position)
        {
            return Math.Max(Math.Abs(position.N_S), Math.Max(Math.Abs(position.NE_SW), Math.Abs(position.NW_SE)));
        }

        static void Main(string[] args)
        {
            var possition = new Vector3();
            var maxDistance = 0;
            var commands = System.IO.File.ReadAllText("input.txt").Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var command in commands)
            {
                if (command == "n")
                {
                    possition.NE_SW++;
                    possition.NW_SE++;
                }
                if (command == "s")
                {
                    possition.NE_SW--;
                    possition.NW_SE--;
                }

                if (command == "ne")
                {
                    possition.N_S++;
                    possition.NW_SE++;
                }
                if (command == "sw")
                {
                    possition.N_S--;
                    possition.NW_SE--;
                }

                if (command == "se")
                {
                    possition.N_S++;
                    possition.NE_SW--;
                }
                if (command == "nw")
                {
                    possition.N_S--;
                    possition.NE_SW++;
                }

                maxDistance = Math.Max(maxDistance, Distance(possition));
            }

            Console.WriteLine(Distance(possition));
            Console.WriteLine(maxDistance);
            Console.ReadKey();
        }
    }
}
