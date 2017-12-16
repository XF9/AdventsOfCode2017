using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Day16
{
    class Program
    {
        static (char command, string param1, string param2) ParseMove(string move)
        {
            if (move[0] == 's')
                return (move[0], move.Substring(1), "");

            return (move[0], move.Substring(1, move.IndexOf("/") - 1).ToLower(), move.Substring(move.IndexOf("/") + 1).ToLower());
        }

        static IEnumerable<Char> GetPrograms(int length)
        {
            for (int i = 0; i < length; i++)
                yield return (char) ('a' + i);
        }

        static void SwapItems(List<char> list, int indexA, int indexB)
        {
            var a = list[indexA];
            var b = list[indexB];
            list[indexB] = a;
            list[indexA] = b;
        }

        static void Dance(List<Char> programs, List<(char command, string param1, string param2)> commands)
        {
            foreach (var command in commands)
            {
                switch (command.command)
                {
                    case 's':
                        for (int i = 0; i < Int32.Parse(command.param1); i++)
                        {
                            var last = programs.Last();
                            programs.Remove(last);
                            programs.Insert(0, last);
                        }
                        break;
                    case 'x':
                        SwapItems(programs, Int32.Parse(command.param1), Int32.Parse(command.param2));
                        break;
                    case 'p':
                        SwapItems(programs, programs.IndexOf(Convert.ToChar(command.param1)), programs.IndexOf(Convert.ToChar(command.param2)));
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        static void Main(string[] args)
        {
            var commands = System.IO.File.ReadAllLines("input.txt")[0].Split(',').Select(ParseMove).ToList();
            var programs = GetPrograms(16).ToList();

            var programCopy = new List<Char>(programs);
            Dance(programCopy, commands);

            Console.WriteLine(String.Join("", programCopy));

            programCopy = new List<char>(programs);
            var itteration = 0;
            do
            {
                itteration++;
                Dance(programCopy, commands);
            } while (!programCopy.SequenceEqual(programs));

            for (int i =0; i < 1000000000 % itteration; i++)
                Dance(programCopy, commands);

            Console.WriteLine(String.Join("", programCopy));
            Console.ReadKey();
        }
    }
}
