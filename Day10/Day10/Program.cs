using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day10
{
    class Program
    {
        public static IEnumerable<int> ParseListInteger(string list)
        {
            foreach(var stringValue in list.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
                if (Int32.TryParse(stringValue, out var value))
                    yield return value;
        }

        public static IEnumerable<int> ParseListASCII(string text)
        {
            var result = new List<int>();
            foreach(var bytevalue in Encoding.ASCII.GetBytes(text))
                result.Add(bytevalue);

            result.AddRange(new List<int>{ 17, 31, 73, 47, 23 });
            return result;
        }

        public static IEnumerable<int> GetValueList()
        {
            for (int value = 0; value < 256; value++)
                yield return value;
        }

        public static void ShiftList(List<int> list, int offset)
        {
            for (int i = 0; i < offset; i++)
            {
                list.Add(list.First());
                list.RemoveAt(0);
            }
        }

        public static void UnshiftList(List<int> list, int shiftCount)
        {
            var shiftsLeft = list.Count - (shiftCount % list.Count);
            ShiftList(list, shiftsLeft);
        }

        public static (int ShiftCount, int Skip) Hash(List<int> values, List<int> lengths, int shiftCount = 0, int skip = 0)
        {
            foreach (var length in lengths)
            {
                var replacement = values.GetRange(0, length);
                replacement.Reverse();

                for (int index = 0; index < replacement.Count; index++)
                    values[index] = replacement[index];

                ShiftList(values, length + skip);
                shiftCount += length + skip;
                skip++;
            }

            return (shiftCount, skip);
        }

        public static IEnumerable<int> Compress(IEnumerable<int> list)
        {
            var copy = new List<int>(list);
            while (copy.Count() >= 16)
            {
                int groupValue = 0;
                foreach (int value in copy.Take(16))
                    groupValue ^= value;

                copy.RemoveRange(0, 16);
                yield return groupValue;
            }
        }

        public static string ToHex(IEnumerable<int> list)
        {
            var result = String.Empty;
            foreach (var item in list)
            {
                var hex = item.ToString("X");
                result += hex.Length == 1 ? "0" + hex : hex;
            }
            return result;
        }

        static void Main(string[] args)
        {
            var values = GetValueList().ToList();
            var lengths = ParseListInteger(System.IO.File.ReadAllText("input.txt")).ToList();
            var hashresult = Hash(values, lengths);
            UnshiftList(values, hashresult.ShiftCount);
            Console.WriteLine(values[0] * values[1]);

            values = GetValueList().ToList();
            lengths = ParseListASCII(System.IO.File.ReadAllText("input.txt")).ToList();

            var skip = 0;
            var shiftCount = 0;
            for (int i = 0; i < 64; i++)
            {
                var result = Hash(values, lengths, shiftCount, skip);
                shiftCount = result.ShiftCount;
                skip = result.Skip;
            }

            UnshiftList(values, shiftCount);
            var compressed = Compress(values).ToList();
            var hex = ToHex(compressed);

            Console.ReadKey();
        }
    }
}
