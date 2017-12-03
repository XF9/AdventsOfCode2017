using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Day3
{
    class Program
    {
        struct Point
        {
            public int X;
            public int Y;
        }

        static void Main(string[] args)
        {
            var input = Int32.Parse(Console.ReadLine());

            var direction = 3;
            var shell = 0;

            var length = -1;
            var min = 1;
            var max = 1;
            var center = 1;

            while (input > max)
            {
                direction = (direction + 1) % 4;
                if (direction == 0)
                {
                    shell++;
                    length += 2;
                }

                min = max + 1;
                max = min + length;
                center = min + (max - min) / 2;
            }

            Console.WriteLine(Math.Abs(center - input) + shell);

            //
            // Teil 2
            //

            input = Int32.Parse(Console.ReadLine());
            var values = new Dictionary<Point, Int32>();
            var currentPosition = new Point
            {
                X = 0,
                Y = 0
            };
            direction = 3;
            length = -1;
            var lastValue = 0;

            values.Add(new Point{X = 0, Y = 0}, 1);

            while (true)
            {
                direction = (direction + 1) % 4;
                if (direction == 0)
                {
                    length += 2;
                    currentPosition.X++;
                    currentPosition.Y--;
                }

                for (int i = 0; i <= length; i++)
                {
                    if (direction == 0)
                        currentPosition.Y++;
                    if (direction == 1)
                        currentPosition.X--;
                    if (direction == 2)
                        currentPosition.Y--;
                    if (direction == 3)
                        currentPosition.X++;

                    values.TryGetValue(new Point { X = currentPosition.X - 1, Y = currentPosition.Y - 1 }, out var topLeft);
                    values.TryGetValue(new Point { X = currentPosition.X + 0, Y = currentPosition.Y - 1 }, out var topCenter);
                    values.TryGetValue(new Point { X = currentPosition.X + 1, Y = currentPosition.Y - 1 }, out var topRight);
                    values.TryGetValue(new Point { X = currentPosition.X - 1, Y = currentPosition.Y + 0 }, out var middleLeft);
                    values.TryGetValue(new Point { X = currentPosition.X + 1, Y = currentPosition.Y + 0 }, out var middleRight);
                    values.TryGetValue(new Point { X = currentPosition.X - 1, Y = currentPosition.Y + 1 }, out var bottomLeft);
                    values.TryGetValue(new Point { X = currentPosition.X + 0, Y = currentPosition.Y + 1 }, out var bottomCenter);
                    values.TryGetValue(new Point { X = currentPosition.X + 1, Y = currentPosition.Y + 1 }, out var bottomRight);

                    lastValue = topLeft
                        + topCenter
                        + topRight
                        + middleLeft
                        + middleRight
                        + bottomLeft
                        + bottomCenter
                        + bottomRight;

                    values.Add(new Point { X = currentPosition.X, Y = currentPosition.Y }, lastValue);

                    if (lastValue > input)
                        break;
                }

                if(lastValue > input)
                    break;
            }
            Console.WriteLine(lastValue);
            Console.ReadKey();
        }
    }
}
