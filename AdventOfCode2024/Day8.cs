using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public static class Day8
    {
        public static Dictionary<char, List<int[]>> antennas = new Dictionary<char, List<int[]>>();
        public static void Run()
        {
            Console.WriteLine("Day 8");
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(basePath, "Input", "Day8.txt");
            char[,] input = GetInput(relativePath);
            FillMap(input);
            Console.WriteLine("Antennas:");
            foreach (KeyValuePair<char, List<int[]>> antenna in antennas)
            {
                Console.WriteLine(antenna.Key + ": " + antenna.Value.Count);
            }
            HashSet<(int, int)> antinodes = new HashSet<(int, int)>();

            foreach (var array in antennas.Values)
            {
                for (int i = 0; i < array.Count; i++)
                {
                    for (int j = 0; j < array.Count; j++)
                    {
                        if( i == j)
                        {
                            continue;
                        }
                        int r1 = array[i][0];
                        int c1 = array[i][1];
                        int r2 = array[j][0];
                        int c2 = array[j][1];

                        int dr = r2 - r1;
                        int dc = c2 - c1;

                        int r = r1;
                        int c = c1;

                        while(0 <= r && r < input.GetLength(0) && 0 <= c && c < input.GetLength(1))
                        {
                            antinodes.Add((r, c));
                            r += dr;
                            c += dc;
                        }
                        //antinodes.Add((2 * r1 - r2, 2 * c1 - c2));
                        //antinodes.Add((2 * r2 - r1, 2 * c2 - c1));
                    }
                }
            }

            int count = 0;
            foreach (var antinode in antinodes.Where(e => 0 <= e.Item1 && e.Item1 < input.GetLength(0) && 0 <= e.Item2 && e.Item2 < input.GetLength(1)))
            {
                count++;
                Console.WriteLine($"Antinode: {antinode.Item1} {antinode.Item2}");
            }
            Console.WriteLine($"Number of Antinodes: {count}");



        }


        public static void Part1()
        {
            Console.WriteLine("Part 1");
        }
        public static void Part2()
        {
            Console.WriteLine("Part 2");
        }


        public static void FillMap(char[,] input)
        {
            for (int y = 0; y < input.GetLength(0); y++)
            {
                for (int x = 0; x < input.GetLength(1); x++)
                {
                    if (input[y, x] == '.')
                    {
                        continue;
                    }
                    if (!antennas.ContainsKey(input[y, x]))
                    {
                        antennas.Add(input[y, x], new List<int[]>());
                    }
                    antennas[input[y, x]].Add(new int[2] { y, x });
                }
            }
        }

        public static char[,] GetInput(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                List<string> lines = new List<string>();
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                char[,] input = new char[lines.Count, lines[0].Length];
                for (int i = 0; i < lines.Count; i++)
                {
                    for (int j = 0; j < lines[0].Length; j++)
                    {
                        input[i, j] = lines[i][j];
                    }
                }
                return input;
            }
        }
    }
}
