using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day14
{
    class Program
    {
        public const int FieldSize = 128;
        public static string HexvalueToBits(string hex)
        {
            var value = Convert.ToInt32(hex, 16);
            var bits = Convert.ToString(value, 2).PadLeft(4, '0');
            return bits;
        }

        public static void FormGroup(int groupCount, Point coords, String[] occupiedPositions, Int32[,] groupMap)
        {
            var positionsToCheck = new List<Point> { coords };
            while (positionsToCheck.Any())
            {
                var position = positionsToCheck.First();
                positionsToCheck.Remove(position);
                groupMap[position.Y, position.X] = groupCount;

                var top = new Point(position.X, position.Y - 1);
                if(NeedsToBeGrouped(top, occupiedPositions, groupMap) && !positionsToCheck.Contains(top))
                    positionsToCheck.Add(top);

                var bottom = new Point(position.X, position.Y + 1);
                if (NeedsToBeGrouped(bottom, occupiedPositions, groupMap) && !positionsToCheck.Contains(bottom))
                    positionsToCheck.Add(bottom);

                var left = new Point(position.X - 1, position.Y);
                if (NeedsToBeGrouped(left, occupiedPositions, groupMap) && !positionsToCheck.Contains(left))
                    positionsToCheck.Add(left);

                var right = new Point(position.X + 1, position.Y);
                if (NeedsToBeGrouped(right, occupiedPositions, groupMap) && !positionsToCheck.Contains(right))
                    positionsToCheck.Add(right);
            }
        }

        public static bool NeedsToBeGrouped(Point point, String[] occupiedPositions, Int32[,] groupMap)
        {
            var inBounds = point.Y >= 0 && point.Y < FieldSize && point.X >= 0 && point.X < FieldSize;
            var needsToBeGrouped = inBounds && occupiedPositions[point.Y][point.X] == '1' && groupMap[point.Y, point.X] == 0;
            return needsToBeGrouped;
        }

        static void Main(string[] args)
        {
            var input = "ffayrhll";
            var hasher = new KnotHasher();
            var lines = new String[FieldSize];

            for (int row = 0; row < FieldSize; row++)
            {
                var hashInput = $"{input}-{row}";
                var hash = hasher.Hash(hashInput);

                var bitstring = String.Empty;
                foreach (var c in hash)
                    bitstring += HexvalueToBits(c.ToString());

                lines[row] = bitstring;
            }

            var filled = lines.SelectMany(l => l).Count(c => c == '1');
            Console.WriteLine(filled);

            var groupMap = new Int32[FieldSize, FieldSize];
            var groupCount = 0;
            for (int row = 0; row < FieldSize; row++)
                for (int column = 0; column < FieldSize; column++)
                    if (NeedsToBeGrouped(new Point(column, row), lines, groupMap))
                        FormGroup(++groupCount, new Point(column, row), lines, groupMap);

            Console.WriteLine(groupCount);
            Console.ReadKey();
        }
    }
}
