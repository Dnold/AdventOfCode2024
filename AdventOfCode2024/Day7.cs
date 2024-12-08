using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Node
    {
        public Node() { }
        public Node(long value, string operation = "")
        {
            this.value = value;
            this.operation = operation;
        }
        public long value;
        public string operation;
        public Node left;
        public Node right;
    }
    public class NodeTripple
    {
        public NodeTripple() { }
        public NodeTripple(long value, string operation = "")
        {
            this.value = value;
            this.operation = operation;
        }
        public long value;
        public string operation;
        public NodeTripple left;
        public NodeTripple middle;
        public NodeTripple right;
    }

    public static class Day7
    {
        public static List<Tuple<long, List<int>>> input;
        public static void Run()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(basePath, "Input", "Day7.txt");
            input = GetInput(relativePath);
            Console.WriteLine("#####################################################################################################");
            Console.WriteLine("Day 7");
            Console.WriteLine("-----------------------------------------");
            Part1();
            Console.WriteLine("------------------------------------------");
            Part2();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("#####################################################################################################");
        }

        public static void Part1()
        {
            Console.WriteLine("Part 1");
            long count = 0;
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Item2.Count; j++)
                {
                    Console.Write(input[i].Item2[j] + " ");
                }
                Node constructedTree = ConstructTree(input[i].Item2);
                Console.WriteLine("Expected Result: " + input[i].Item1);
                if (ValidSolutionExists(constructedTree, input[i].Item1))
                {
                    Console.WriteLine("Valid Solution Exists");
                    count += input[i].Item1;
                }
            }
            Console.WriteLine("Sum of Valid Solutions: " + count);
        }
        public static void Part2()
        {
            input = GetInput(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Input", "Day7.txt"));
            long count = 0;
            Console.WriteLine("Part 2");
            foreach (var item in input)
            {
                // Hier können Sie eine andere Methode zur Baumkonstruktion aufrufen
                NodeTripple root = ConstructTreeTripple(item.Item2);

                Console.WriteLine("Expected Result: " + item.Item1);
                if (ValidSolutionExistsTripple(root, item.Item1)){
                    Console.WriteLine("Valid Solution Exists");
                    PrintTreeTripple(root);
                   
                    count += item.Item1;
                }
            }
            Console.WriteLine("Sum of Valid Solutions: " + count);
        }

        public static List<Tuple<long, List<int>>> GetInput(string path)
        {
            List<Tuple<long, List<int>>> result = new List<Tuple<long, List<int>>>();

            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] data = line.Split(":");
                    long expectedResult = long.Parse(data[0]);
                    string[] values = data[1].Split(" ");
                    List<string> curedValues = values.ToList();
                    curedValues.RemoveAll(x => x == " " || x == "");
                    values = curedValues.ToArray();
                    List<int> ints = new List<int>();
                    foreach (string value in values)
                    {
                        int val = int.Parse(value);
                        ints.Add(val);
                    }
                    result.Add(new Tuple<long, List<int>>(expectedResult, ints));
                }
            }

            return result;
        }
        public static bool ValidSolutionExists(Node node, long expectedResult)
        {
            if (node == null)
            {
                return false;
            }
            if (node.left == null && node.right == null)
            {
                return node.value == expectedResult;
            }
            return ValidSolutionExists(node.left, expectedResult) || ValidSolutionExists(node.right, expectedResult);
        }
        public static bool ValidSolutionExistsTripple(NodeTripple node, long expectedResult)
        {
            if (node == null)
            {
                return false;
            }
            if (node.left == null && node.right == null && node.middle == null)
            {
                return node.value == expectedResult;
            }
            return ValidSolutionExistsTripple(node.left, expectedResult) || ValidSolutionExistsTripple(node.right, expectedResult) || ValidSolutionExistsTripple(node.middle, expectedResult);
        }
        public static Node ConstructTree(List<int> ints)
        {
            if (ints == null || ints.Count == 0)
            {
                return null;
            }

            Node startNode = new Node(ints[0]);
            Queue<Node> nodesToProcess = new Queue<Node>();
            nodesToProcess.Enqueue(startNode);
            ints.RemoveAt(0);

            while (ints.Count > 0)
            {
                int currentValue = ints[0];
                ints.RemoveAt(0);
                int count = nodesToProcess.Count;

                for (int i = 0; i < count; i++)
                {
                    Node currentNode = nodesToProcess.Dequeue();
                    currentNode.left = new Node(currentNode.value * currentValue, "*" + currentValue);
                    currentNode.right = new Node(currentNode.value + currentValue, "+" + currentValue);
                    nodesToProcess.Enqueue(currentNode.left);
                    nodesToProcess.Enqueue(currentNode.right);
                }
            }

            return startNode;
        }
        public static NodeTripple ConstructTreeTripple(List<int> ints)
        {
            if (ints == null || ints.Count == 0)
            {
                return null;
            }
            NodeTripple startNode = new NodeTripple(ints[0]);
            Queue<NodeTripple> nodesToProcess = new Queue<NodeTripple>();
            nodesToProcess.Enqueue(startNode);
            ints.RemoveAt(0);
            while (ints.Count > 0)
            {
                int currentValue = ints[0];
                ints.RemoveAt(0);
                int count = nodesToProcess.Count;

                for (int i = 0; i < count; i++)
                {
                    NodeTripple currentNode = nodesToProcess.Dequeue();
                    currentNode.left = new NodeTripple(currentNode.value * currentValue, "*" + currentValue);
                    currentNode.middle = new NodeTripple(currentNode.value + currentValue, "+" + currentValue);
                    currentNode.right = new NodeTripple(long.Parse(currentNode.value.ToString() + currentValue.ToString()), "||" + currentValue);
                    nodesToProcess.Enqueue(currentNode.left);
                    nodesToProcess.Enqueue(currentNode.middle);
                    nodesToProcess.Enqueue(currentNode.right);
                }
            }
            return startNode;
        }
        public static void PrintTreeTripple(NodeTripple node, string indent = "", bool isLeft = true)
        {
            if (node != null)
            {
                Console.Write(indent + (isLeft ? "├──" : "└──") + node.value);
                if (!string.IsNullOrEmpty(node.operation))
                {
                    if (node.operation.StartsWith("+"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (node.operation.StartsWith("*"))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (node.operation.StartsWith("||"))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    Console.Write(" (" + node.operation + ")");
                    Console.ResetColor();
                }
                Console.WriteLine();
                indent += isLeft ? "│   " : "    ";
                PrintTreeTripple(node.left, indent, true);
                PrintTreeTripple(node.middle, indent, false);
                PrintTreeTripple(node.right, indent, false);
            }
        }
        public static void PrintTree(Node node, string indent = "", bool isLeft = true)
        {
            if (node != null)
            {
                Console.Write(indent + (isLeft ? "├──" : "└──") + node.value);
                if (!string.IsNullOrEmpty(node.operation))
                {
                    if (node.operation.StartsWith("+"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (node.operation.StartsWith("*"))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(" (" + node.operation + ")");
                    Console.ResetColor();
                }
                Console.WriteLine();
                indent += isLeft ? "│   " : "    ";
                PrintTree(node.left, indent, true);
                PrintTree(node.right, indent, false);
            }
        }
    }
}
