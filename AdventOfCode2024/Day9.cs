using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization.Metadata;

namespace AdventOfCode2024
{
    public static class Day9
    {
        public static int[] input = Array.Empty<int>();

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
            List<int> blocks = new List<int>();
            int id = 0;

            // Populate blocks and blanks
            for (int i = 0; i < input.Length; i++)
            {
                int count = input[i]; // Number of blocks or blanks

                if (i % 2 == 0) // Non-empty blocks
                {
                    for (int j = 0; j < count; j++)
                    {
                        blocks.Add(id); // Add block ID
                    }
                    id++;
                }
                else // Empty spaces
                {
                    for (int j = 0; j < count; j++)
                    {
                        blocks.Add(-1); // Add blank space as -1
                    }
                }
            }

            // Debug: Print initial blocks
            Console.WriteLine("Initial Blocks: " + string.Join(", ", blocks));

            // Move all non-empty blocks to the leftmost free space
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] == -1)
                {
                    while (blocks[^1] == -1) // Remove trailing blanks
                        blocks.RemoveAt(blocks.Count - 1);

                    if (blocks.Count <= i) break; // Stop if all blanks are handled

                    blocks[i] = blocks[^1]; // Fill blank with the last block
                    blocks.RemoveAt(blocks.Count - 1); // Remove used block
                }
            }

            // Debug: Print blocks after moving non-empty blocks
            Console.WriteLine("Blocks After Moving Non-Empty: " + string.Join(", ", blocks));

            // Calculate checksum
            long sum = 0;
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] == -1) continue; // Skip blanks
                sum += blocks[i] * i;
            }
            Console.WriteLine("Checksum: " + sum);
        }

        public static void Part2()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(basePath, "Input", "Day9.txt");
            input = GetInput(relativePath); // Refresh input

            Dictionary<int, (int, int)> files = new Dictionary<int, (int, int)>();
            List<(int, int)> blanks = new List<(int, int)>();

            int fid = 0;
            int pos = 0;

            for (int i = 0; i < input.Length; i++)
            {
                int x = input[i];

                if (i % 2 == 0)
                {
                    if (x == 0)
                    {
                        throw new Exception("File size cannot be 0");
                    }
                    files[fid] = (pos, x);
                    fid++;
                }
                else
                {
                    if (x != 0)
                    {
                        blanks.Add((pos, x));
                    }
                }

                pos += x;
                // Debug: Print initial blocks
            }
            while (fid > 0)
            {
                fid -= 1;
                (int pos, int size) file = files[fid];
                for (int i = 0; i < blanks.Count; i++)
                {
                    (int bpos, int bsize) = blanks[i];
                    if (bpos >= file.pos)
                    {
                        blanks = blanks.GetRange(0, i);
                        break;
                    }
                    if (file.size <= bsize)
                    {
                        files[fid] = (bpos, file.size);

                        if (file.size == bsize)
                        {
                            blanks.RemoveAt(i);

                        }
                        else
                        {
                            blanks[i] = (bpos + file.size, bsize - file.size);
                        }
                        break;
                    }

                }

            }
            long total = 0;
            foreach (var kvp in files)
            {
                int mid = kvp.Key;
                var (rpos, rsize) = kvp.Value; // Dekonstruktion des Tupels

                for (int x = rpos; x < rpos + rsize; x++)
                {
                    total += mid * x;
                }
            }
            Console.WriteLine("Checksum: " + total);
        }

        public static int[] GetInput(string path)
        {
            List<int> result = new List<int>();

            using (StreamReader sr = new StreamReader(path))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (char c in line)
                    {
                        if (char.IsDigit(c))
                        {
                            result.Add(c - '0'); // Convert char to int
                        }
                    }
                }
            }
            return result.ToArray();
        }
    }
}
