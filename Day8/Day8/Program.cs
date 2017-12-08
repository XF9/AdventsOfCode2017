using System;
using System.Collections.Generic;
using System.Linq;

namespace Day8
{
    class Program
    {
        public struct Command
        {
            public string Register { get; set; }
            public string CommandName { get; set; }
            public int Value { get; set; }
            public string ConditionParam1 { get; set; }
            public int ConditionParam2 { get; set; }
            public string Condition { get; set; }
        }

        public static Command ParseCommand(string line)
        {
            var chuncks = line.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            return new Command
            {
                Register = chuncks[0],
                CommandName = chuncks[1],
                Value = Int32.Parse(chuncks[2]),
                ConditionParam1 = chuncks[4],
                Condition = chuncks[5],
                ConditionParam2 = Int32.Parse(chuncks[6])
            };
        }

        static void Main(string[] args)
        {
            var register = new Dictionary<String, Int32>();
            var maxValueOverall = 0;

            foreach (var line in System.IO.File.ReadLines("input.txt"))
            {
                var command = ParseCommand(line);
                var canBeExecuted = false;
                register.TryGetValue(command.ConditionParam1, out var param1);
                var param2 = command.ConditionParam2;

                switch (command.Condition)
                {
                    case "<":
                        canBeExecuted = param1 < param2;
                        break;
                    case "<=":
                        canBeExecuted = param1 <= param2;
                        break;
                    case ">":
                        canBeExecuted = param1 > param2;
                        break;
                    case ">=":
                        canBeExecuted = param1 >= param2;
                        break;
                    case "==":
                        canBeExecuted = param1 == param2;
                        break;
                    case "!=":
                        canBeExecuted = param1 != param2;
                        break;
                    default:
                        throw new NotImplementedException();
                }

                if (canBeExecuted)
                {
                    register.TryGetValue(command.Register, out var oldValue);
                    switch (command.CommandName)
                    {
                        case "inc":
                            register[command.Register] = oldValue + command.Value;
                            break;
                        case "dec":
                            register[command.Register] = oldValue - command.Value;
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    maxValueOverall = register[command.Register] > maxValueOverall ? register[command.Register] : maxValueOverall;
                }
            }

            var highesValue = register.Values.Max();
            Console.WriteLine(highesValue);
            Console.WriteLine(maxValueOverall);
            Console.ReadKey();
        }
    }
}
