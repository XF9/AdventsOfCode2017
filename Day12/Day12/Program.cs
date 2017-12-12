using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    class Program
    {
        static (int Id, List<int> Connections) Parse(string line)
        {
            var delimiterId = line.IndexOf(" ");
            var id = Int32.Parse(line.Substring(0, delimiterId));
            var connections = line.Substring(delimiterId + 5).Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries).Select(x => Int32.Parse(x)).ToList();
            return(id, connections);
        }

        static List<int> CreateGroup(Dictionary<int, List<int>> connectionTable, int origin)
        {
            var connections = new List<int>();
            var connectionsToCheck = new List<int> { origin };

            while (connectionsToCheck.Any())
            {
                var connectionToCheck = connectionsToCheck.First();

                foreach (var child in connectionTable[connectionToCheck])
                {
                    if (!connections.Contains(child) && !connectionsToCheck.Contains(child))
                        connectionsToCheck.Add(child);
                }

                connectionsToCheck.Remove(connectionToCheck);
                connections.Add(connectionToCheck);
            }

            return connections;
        }

        static void Main(string[] args)
        {
            var connectionTable = new Dictionary<int, List<int>>();

            var input = System.IO.File.ReadAllLines("input.txt");
            foreach (var line in input)
            {
                var entry = Parse(line);
                connectionTable.Add(entry.Id, entry.Connections);
            }

            // Teil 1
            var result = CreateGroup(connectionTable, 0);
            Console.WriteLine(result.Count);

            // Teil 2
            var groupcount = 0;
            var nodes = connectionTable.Keys.ToList();
            while (nodes.Any())
            {
                var group = CreateGroup(connectionTable, nodes.First());
                nodes = nodes.Except(group).ToList();
                groupcount++;
            }

            Console.WriteLine(groupcount);
            Console.ReadKey();
        }
    }
}
