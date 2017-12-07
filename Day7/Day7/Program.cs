using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    public class RawNode
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public IEnumerable<string> Children { get; set; }
    }

    public class Node
    {
        public string Name { get; }
        public int Weight { get; }

        public List<Node> Children { get; }

        public int TotalWeight
        {
            get { return Weight + Children.Sum(c => c.TotalWeight); }
        }

        public bool EvenWeightDistribution
        {
            get { return !Children.Any() || Children.All(c => c.TotalWeight == Children.First().TotalWeight); }
        }

        public IEnumerable<Node> AllChildren
        {
            get { return Children.SelectMany(c => c.AllChildren).Concat(new List<Node>{this}); }
        }

        public Node(RawNode node, IEnumerable<RawNode> nodes)
        {
            Name = node.Name;
            Weight = node.Weight;

            Children = new List<Node>();
            foreach (var child in node.Children)
                Children.Add(new Node(nodes.Single(n => child == n.Name), nodes));
        }
    }

    class Program
    {
        public static Node ParseLines(IEnumerable<string> lines)
        {
            var nodes = new List<RawNode>();
            foreach (var line in lines)
            {
                var endName = line.IndexOf(" ");
                var startWeight = line.IndexOf("(") + 1;
                var endWeight = line.IndexOf(")");
                var startChildren = line.IndexOf("-> ") + 3;
                var lengthChildren = line.Length - startChildren;

                var children = startChildren > 2 ? line.Substring(startChildren, lengthChildren) : String.Empty;

                nodes.Add(new RawNode
                {
                    Name = line.Substring(0, endName),
                    Weight = Int32.Parse(line.Substring(startWeight, endWeight - startWeight)),
                    Children = children.Split(new String[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList()
                });
            }

            var root = nodes.Single(r => nodes.All(n => !n.Children.Contains(r.Name)));

            return new Node(root, nodes);
        }

        static void Main(string[] args)
        {
            var root = ParseLines(System.IO.File.ReadLines(@"input.txt"));
            Console.WriteLine(root.Name);

            var unbalancedChildren = root.AllChildren.First(c => !c.EvenWeightDistribution).Children;

            var traitor =
                unbalancedChildren.FirstOrDefault(c => c.TotalWeight != unbalancedChildren.First().TotalWeight) ??
                unbalancedChildren.First();

            var difference = traitor.TotalWeight - unbalancedChildren.First(c => c.TotalWeight != traitor.Weight).TotalWeight;

            Console.WriteLine(traitor.Weight - difference);

            Console.ReadKey();
        }
    }
}
