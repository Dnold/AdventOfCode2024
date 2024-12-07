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

    public static class Day7
    {
        public static List<Tuple<long, Node>> input;
        public static List<int[]> calibrationValues = new List<int[]>();
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
                for (int j = 0; j < calibrationValues[i].Length; j++)
                {
                    Console.Write(calibrationValues[i][j] + " ");
                }
                Console.WriteLine("Expected Result: " + input[i].Item1);

                if (ValidSolutionExists(input[i].Item2, input[i].Item1))
                {

                    Console.WriteLine("Valid Solution Exists");
                    count += input[i].Item1;
                }
            }
            Console.WriteLine("Number of Valid Solutions: " + count);
        }

        public static void Part2()
        {
            
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

        public static List<Tuple<long, Node>> GetInput(string path)
        {
            List<Tuple<long, Node>> result = new List<Tuple<long, Node>>();

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
                    calibrationValues.Add(ints.ToArray());
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

                    result.Add(new Tuple<long, Node>(expectedResult, startNode));
                }
            }

            return result;
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
