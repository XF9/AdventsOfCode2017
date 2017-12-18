using System;
using System.Collections.Generic;
using System.Linq;

namespace Day18
{
    public struct Command
    {
        public string CommandName { get; set; }
        public Char Register { get; set; }
        public string Value { get; set; }
    }

    class Program
    {
        public List<Int64> Queue { get; }
        public List<Int64> QueueTo { get; set; }

        public bool Waiting { get; private set; }
        public Int32 QueueCommandCount { get; private set; }

        private readonly int _programId;
        private Int32 _index;
        private readonly List<Command> _commands;
        private readonly Dictionary<Char, Int64> _register;

        public Program(int programId, IEnumerable<Command> commands)
        {
            _programId = programId;
            _commands = commands.ToList();
            _register = new Dictionary<Char, Int64>();
            Queue = new List<Int64>();
        }

        private Int64 ParseRegisterValue(Char commandValue)
        {
            return ParseRegisterValue(commandValue.ToString());
        }

        private Int64 ParseRegisterValue(string commandValue)
        {
            if (!Int64.TryParse(commandValue, out var value))
                value = _register.Keys.Contains(commandValue[0]) ? _register[commandValue[0]] : (Int64)_programId;

            return value;
        }

        public void Tick()
        {
            if (_index < 0 || _index > _commands.Count)
            {
                Waiting = true;
                return;
            }

            var command = _commands[_index];
            switch (command.CommandName)
            {
                case "set":
                    _register[command.Register] = ParseRegisterValue(command.Value);
                    break;
                case "add":
                    _register[command.Register] = checked(ParseRegisterValue(command.Register) + ParseRegisterValue(command.Value));
                    break;
                case "mul":
                    _register[command.Register] = checked(ParseRegisterValue(command.Register) * ParseRegisterValue(command.Value));
                    break;
                case "mod":
                    _register[command.Register] = ParseRegisterValue(command.Register) % ParseRegisterValue(command.Value);
                    break;
                case "jgz":
                    if (ParseRegisterValue(command.Register) > 0)
                    {
                        _index += (int) ParseRegisterValue(command.Value);
                        return;
                    }
                    break;
                case "snd":
                    QueueTo.Add(ParseRegisterValue(command.Register));
                    QueueCommandCount++;
                    break;
                case "rcv":
                    if (!Queue.Any())
                    {
                        Waiting = true;
                        return;
                    }
                    else
                    {
                        _register[command.Register] = Queue.First();
                        Queue.Remove(Queue.First());
                        Waiting = false;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            _index++;
        }
    }

    class Runner
    {
        static void Main(string[] args)
        {
            var commands = System.IO.File.ReadLines("input.txt").Select(l => new Command
            {
                CommandName = l.Substring(0, 3),
                Register = l.Substring(4,1)[0],
                Value = l.Length > 5 ? l.Substring(6) : ""
            })
            .ToList();

            var queueTeil1 = new List<Int64>();
            Program program = new Program(0, commands);
            program.QueueTo = queueTeil1;

            while(!program.Waiting)
                program.Tick();

            Console.WriteLine(queueTeil1.Last());

            Program program0 = new Program(0, commands);
            Program program1 = new Program(1, commands);
            program0.QueueTo = program1.Queue;
            program1.QueueTo = program0.Queue;

            while (!program0.Waiting || !program1.Waiting)
            {
                program0.Tick();
                program1.Tick();
            }

            Console.WriteLine(program1.QueueCommandCount);
            Console.ReadKey();
        }
    }
}
