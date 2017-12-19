using System;
using System.Drawing;

namespace Day19
{
    class Program
    {
        static Char GetSymbolFromMap(String[] map, Point position)
        {
            return GetSymbolFromMap(map, position, new Point(0, 0));
        }

        static Char GetSymbolFromMap(String[] map, Point position, Point direction)
        {
            var posX = position.X + direction.X;
            var posY = position.Y + direction.Y;

            if (posY < 0 || posY >= map.Length || posX < 0 || posX >= map[posY].Length)
                return ' ';

            return map[posY][posX];
        }

        static void Main(string[] args)
        {
            var map = System.IO.File.ReadAllLines("input.txt");
            var currentPosition = new Point {X = map[0].IndexOf('|')};
            var direction = new Point(0, 1);
            var solution = String.Empty;
            var running = true;
            var stepCount = 0;

            while (running)
            {
                var currentSymbol = GetSymbolFromMap(map, currentPosition);
                var nextSymbol = GetSymbolFromMap(map, currentPosition, direction);

                if (currentSymbol == ' ')
                    running = false;

                if (char.IsLetter(currentSymbol))
                    solution += currentSymbol;

                if (currentSymbol == '+' && nextSymbol == ' ')
                {
                    if (direction.X != 0)
                        direction = GetSymbolFromMap(map, currentPosition, new Point(0, 1)) != ' ' ? new Point(0, 1) : new Point(0, -1);
                    else
                        direction = GetSymbolFromMap(map, currentPosition, new Point(1, 0)) != ' ' ? new Point(1, 0) : new Point(-1, 0);
                }

                currentPosition.X += direction.X;
                currentPosition.Y += direction.Y;
                stepCount++;
            }

            Console.WriteLine(solution);
            Console.WriteLine(stepCount - 1);
            Console.ReadKey();
        }
    }
}
