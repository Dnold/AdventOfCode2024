using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public static class Day10
    {
        public static int[,] input;
        public static void Run()
        {
            Console.WriteLine("#########################");
            Console.WriteLine("Day 10");

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(basePath, "Input", "Day10.txt");

            input = GetInput(relativePath);

            Part1();

            Console.WriteLine("--------------------");

            Part2();

            Console.WriteLine("---------------------");
            Console.WriteLine("##########################");
        }
        public static void Part1()
        {
            int count = 0;
            Console.WriteLine("Part 1");
            for (int y = 0; y < input.GetLength(0); y++)
            {
                for (int x = 0; x < input.GetLength(1); x++)
                {
                    if (input[y, x] == 0)
                    {
                        Queue<(int, int)> toVisit = new Queue<(int, int)>();
                        HashSet<(int, int)> visited = new HashSet<(int, int)>();
                        toVisit.Enqueue((x, y));
                        visited.Add((x, y)); // Fügen Sie den Startknoten zu visited hinzu

                        while (toVisit.Count > 0)
                        {
                            (int, int) current = toVisit.Dequeue();

                            // Überprüfen, ob wir das Ziel erreicht haben
                            if (input[current.Item2, current.Item1] == 9)
                            {
                                count++;
                                
                            }

                            // Fügen Sie alle verbundenen Nachbarn hinzu, keine Diagonalen
                            for (int i = -1; i <= 1; i++)
                            {
                                for (int j = -1; j <= 1; j++)
                                {
                                    if (i == 0 && j == 0)
                                    {
                                        continue;
                                    }
                                    // Überprüfen, ob es diagonal ist
                                    if (i != 0 && j != 0)
                                    {
                                        continue;
                                    }
                                    int newX = current.Item1 + i;
                                    int newY = current.Item2 + j;
                                    // Überprüfen, ob es innerhalb der Grenzen liegt und nicht besucht wurde
                                    if (newX >= 0 && newX < input.GetLength(1) && newY >= 0 && newY < input.GetLength(0) && !visited.Contains((newX, newY)))
                                    {
                                        // Überprüfen, ob der neue Knoten genau 1 höher ist als der aktuelle Knoten
                                        if (input[newY, newX] == input[current.Item2, current.Item1] + 1)
                                        {
                                            toVisit.Enqueue((newX, newY));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine(count);
        }

        public static void Part2()
        {
            Console.WriteLine("Part 2");
        }
        public static int[,] GetInput(string path)
        {
            int[,] input;
            using (StreamReader sr = new StreamReader(path))
            {
                List<string> lines = new List<string>();

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                input = new int[lines.Count, lines[0].Length];
                for (int i = 0; i < lines.Count; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        input[i, j] = Convert.ToInt32(lines[i][j] + "");
                    }
                }
            }
            return input;
        }
    }
}
