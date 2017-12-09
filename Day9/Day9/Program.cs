using System;

namespace Day9
{
    class Program
    {
        public static (int Score, int GarbageLength) Solve(string group)
        {
            var depth = 1;
            var score = 0;
            var readingGarbage = false;
            var garbageLength = 0;

            for (int index = 0; index < group.Length; index++)
            {
                switch (group[index])
                {
                    case '!':
                        index++;
                        break;
                    case '{':
                        if (!readingGarbage)
                            score += depth++;
                        else
                            garbageLength++;
                        break;
                    case '}':
                        if(!readingGarbage)
                            depth--;
                        else
                            garbageLength++;
                        break;
                    case '<':
                        if(!readingGarbage)
                            readingGarbage = true;
                        else
                            garbageLength++;
                        break;
                    case '>':
                        readingGarbage = false;
                        break;
                    default:
                        if (readingGarbage)
                            garbageLength++;
                        break;
                }
            }

            return (score, garbageLength);
        }

        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText("input.txt");
            var result = Solve(input);
            Console.WriteLine(result.Score);
            Console.WriteLine(result.GarbageLength);
            Console.ReadKey();
        }
    }
}
