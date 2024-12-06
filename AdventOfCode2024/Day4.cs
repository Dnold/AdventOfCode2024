using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
namespace AdventOfCode2024
{
    public static class Day4
    {
        public static int[][] directions = new int[][] {
            new int []{ 0, 1  }, //Up
           new int[] { 1, 0  }, //Right
           new int[] { 1, 1  }, //Right UP
           new int[] { 1, -1 }, //Right Down
           new int[] { -1, 0 }, //Left
           new int[] { -1, 1 }, //Left Up
           new int[] { -1, -1}, //Left Down
           new int[] { 0, -1 }  //Down
        };

        public static void Run()
        {
            Console.WriteLine("######################################################");
            Console.WriteLine("Day 4");
            char?[,] mask =
            {
                {'M',null,'S' },
                {null,'A',null },
                {'M',null,'S' }
            };
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(basePath, "Input", "Day4.txt");
            char[,] input = GetInputFromDocument(relativePath);
            Console.WriteLine("-------------------------");
            Part1(input);
            Console.WriteLine("-------------------------");
            Part2(input, mask);
            Console.WriteLine("-------------------------");
            Console.WriteLine("######################################################");
        }
        public static void Part1(char[,] input)
        {
            Console.WriteLine("Part 1");
            int count = 0;
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i, j] == 'X')
                    {
                        foreach (int[] direction in directions)
                        {
                            if (CheckDirection(input, direction, j, i))
                            {
                                count++;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("The number of XMAS is: " + count);
        }
        public static void Part2(char[,] input, char?[,] mask)
        {
            Console.WriteLine("Part 2");
            int count = 0;
            for (int y = 0; y < input.GetLength(0); y++)
            {
                for (int x = 0; x < input.GetLength(1); x++)
                {
                    char?[,] curretLayerMask = mask;
                    if (input[y, x] == 'A')
                    {
                        for (int i = 0; i < 4; i++)
                        {

                            if (ApplyMask(input, curretLayerMask, x, y, 1, 1))
                            {
                                count++;
                                break;
                            }
                            else
                            {
                                curretLayerMask = RotateMask(curretLayerMask);
                            }
                        }

                    }

                }
            }
            Console.WriteLine("The number of X-MAS is: " + count);
        }
        static char?[,] RotateMask(char?[,] mask)
        {
            int n = mask.GetLength(0); // Assuming square matrix (n x n)
            char?[,] rotatedMask = new char?[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // Rotate clockwise: element at (i, j) moves to (j, n - 1 - i)
                    rotatedMask[j, n - 1 - i] = mask[i, j];
                }
            }

            return rotatedMask;
        }

        static bool ApplyMask(char[,] grid, char?[,] mask, int gridX, int gridY, int maskCenterX, int maskCenterY)
        {
            int maskHeight = mask.GetLength(0);
            int maskWidth = mask.GetLength(1);
            int gridHeight = grid.GetLength(0);
            int gridWidth = grid.GetLength(1);

            for (int maskY = 0; maskY < maskHeight; maskY++)
            {
                for (int maskX = 0; maskX < maskWidth; maskX++)
                {
                    char? maskValue = mask[maskY, maskX];
                    if (maskValue == null)
                        continue;

                    //Calculate Grid position corresponding to the mask Element
                    int offsetX = maskX - maskCenterX;
                    int offsetY = maskY - maskCenterY;
                    int checkX = gridX + offsetX;
                    int checkY = gridY + offsetY;

                    if (checkX < 0 || checkX >= gridWidth || checkY < 0 || checkY >= gridHeight)
                    {
                        return false;
                    }
                    if (grid[checkY, checkX] != maskValue)
                    {

                        return false;
                    }
                }
            }
            return true;

        }
        public static bool CheckDirection(char[,] input, int[] direction, int x, int y)
        {
            if (input[y, x] != 'X')
            {
                return false;
            }

            List<char> output = new List<char>();
            output.Add(input[y, x]);
            for (int order = 1; order <= 3; order++)
            {
                int yIndex = y + direction[0] * order;
                int xIndex = x + direction[1] * order;
                if (CheckBounds(input.GetLength(0) - 1, input.GetLength(1) - 1, yIndex, xIndex))
                {
                    output.Add(input[yIndex, xIndex]);
                }
            }
            if (new string(output.ToArray()) == "XMAS")
            {
                return true;
            }
            return false;
        }
        static bool CheckBounds(int sizeX, int sizeY, int x, int y)
        {
            if (x < 0 || x > sizeX)
            {
                return false;
            }
            if (y < 0 || y > sizeY)
            {
                return false;
            }
            return true;
        }
        public static char[,] GetInputFromDocument(string path)
        {
            List<string> input = new List<string>();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    input.Add(line);
                }
            }
            List<char[]> chars = new List<char[]>();
            foreach (string line in input)
            {
                char[] dataLine = line.ToCharArray();
                chars.Add(dataLine);
            }
            return ConvertToMultidimensional(chars.ToArray());
        }
        public static char[,] ConvertToMultidimensional(char[][] input)
        {
            int row = input.Length;
            int col = input[0].Length;

            char[,] resutl = new char[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    resutl[i, j] = input[i][j];
                }
            }
            return resutl;
        }
    }
}