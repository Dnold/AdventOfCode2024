using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public static class Day9
    {
        public static char[] input = Array.Empty<char>();

        public static void Run()
        {
            Console.WriteLine("#########################");
            Console.WriteLine("Day9");
            Console.WriteLine("--------------------");
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(basePath, "Input", "Day9.txt");
            input = GetInput(relativePath);
            Part1();
            Console.WriteLine("--------------------");
            Part2();
            Console.WriteLine("##########################");
        }

        public static void Part1()
        {
            bool isEmptySpace = false;
            List<char> blocks = new List<char>();
            int id = 0;
            for (int i = 0; i < input.Length; i++)
            {
                int count = Convert.ToInt32(input[i] + "");

                if (!isEmptySpace)
                {
                    for (int j = 0; j < count; j++)
                    {
                        blocks.Add((id + "")[0]);
                    }
                    id++;
                }
                else
                {
                    for (int j = 0; j < count; j++)
                    {
                        blocks.Add('.');
                    }
                }
                isEmptySpace = !isEmptySpace;
            }

            // Debug: Print initial blocks
            Console.WriteLine("Initial Blocks: " + new string(blocks.ToArray()));

            // Move all non-empty blocks to the leftmost free space one at a time
            for (int i = blocks.Count - 1; i >= 0; i--)
            {
                if (blocks[i] != '.')
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (blocks[j] == '.')
                        {
                            blocks[j] = blocks[i];
                            blocks[i] = '.';
                            break;
                        }
                    }
                }
            }

            // Debug: Print blocks after moving non-empty blocks
            Console.WriteLine("Blocks After Moving Non-Empty: " + new string(blocks.ToArray()));

            // Find CheckSum
            long sum = 0;
            for (int i = 0; i < blocks.Count; ++i)
            {
                if (blocks[i] == '.')
                {
                    continue;
                }
                sum += Convert.ToInt32(blocks[i] + "") * i;
            }
            Console.WriteLine("Checksum: " + sum);
        }


        public static void Part2()
        {
            Console.WriteLine("Part2");
        }

        public static char[] GetInput(string path)
        {
            List<char> result = new List<char>();

            using (StreamReader sr = new StreamReader(path))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (char c in line)
                    {
                        result.Add(c);
                    }
                }
            }
            return result.ToArray();
        }
    }
}
