namespace AdventOfCode2024
{
    public static class Day6
    {
        static int[] guardPos = new int[2];
        static int[] currentDirection = new int[2] { 0, -1 };
        static List<int[]> visited = new List<int[]>();
        static int[] cachedGuardPos = new int[2];
        static char[,] input = new char[0, 0];
        static HashSet<(int x, int y, int dx, int dy)> visitedStates = new HashSet<(int, int, int, int)>();
        public static void Run()
        {
            Console.WriteLine("######################################################");
            Console.WriteLine("Day 6");
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(basePath, "Input", "Day6.txt");
            input = GetInput(relativePath);
            for (int y = 0; y < input.GetLength(0); y++)
            {
                for (int x = 0; x < input.GetLength(1); x++)
                {
                    if (input[y, x] == '^')
                    {
                        guardPos[0] = x;
                        guardPos[1] = y;
                    }
                }
            }
            cachedGuardPos = new int[2] { guardPos[0], guardPos[1] };
            Console.WriteLine("Guard Position: " + (guardPos[0] + 1) + " " + (guardPos[1] + 1));
            Console.WriteLine("-------------------------");
            Part1();
            guardPos = new int[2] { cachedGuardPos[0], cachedGuardPos[1] };
            visited.Clear();
            input = GetInput(relativePath);
            Console.WriteLine("-------------------------");
            Part2();
            Console.WriteLine("-------------------------");
            Console.WriteLine("######################################################");
        }

        public static void Part1()
        {
            Console.WriteLine("Part 1");
            int count = 0;
            while (Step())
            {
                count++;
            }
            Console.WriteLine("Number of Visited Cells: " + (visited.Count + 1));
            int xCount = 0;
            for (int y = 0; y < input.GetLength(0); y++)
            {
                for (int x = 0; x < input.GetLength(1); x++)
                {
                    if (input[y, x] == 'X')
                    {
                        xCount++;
                    }
                }
            }
            Console.WriteLine("Number of X-Cells after: " + xCount);
        }

        public static void Part2()
        {
            Console.WriteLine("Part 2");
            Console.WriteLine("Bruteforcing... This make take a while");
            int blockCount = 0;
            for (int y = 0; y < input.GetLength(0); y++)
            {
                for (int x = 0; x < input.GetLength(1); x++)
                {
                    string basePath = AppDomain.CurrentDomain.BaseDirectory;
                    string relativePath = Path.Combine(basePath, "Input", "Day6.txt");
                    input = GetInput(relativePath);
                    visited.Clear();
                    visitedStates.Clear();
                    currentDirection = new int[2] { 0, -1 };
                    guardPos = new int[] { cachedGuardPos[0], cachedGuardPos[1] };
                    if (input[y, x] != '#' && input[y, x] != '^')
                    {
                        input[y, x] = '#';
                        int maxCount = 6179; // I tried 6178 and it was not enough so I increased it by 1 and it worked
                        int count = 0;
                        while (count <= maxCount)
                        {
                            if (!Step())
                            {
                                break; // Guard cant move no more
                            }
                            count++;
                        }
                        if (DetectLooping())
                        {
                            blockCount++;
                        }

                    }
                }
            }
            Console.WriteLine("Number of possible Obstacles: " + blockCount);
        }



        public static bool DetectLooping()
        {
            // Create a tuple representing the current state
            var currentState = (guardPos[0], guardPos[1], currentDirection[0], currentDirection[1]);

            // Check if the current state has already been visited
            if (visitedStates.Contains(currentState))
            {
                return true; // Loop detected
            }

            // Add the current state to the visited states
            visitedStates.Add(currentState);
            return false; // No loop detected
        }

        public static char[,] GetInput(string path)
        {
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            char[,] grid = new char[lines.Count, lines[0].Length];
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    grid[i, j] = lines[i][j];
                }
            }
            return grid;
        }

        public static bool Step()
        {
            // Calculate next position
            int[] nextPos = new int[2] { guardPos[0] + currentDirection[0], guardPos[1] + currentDirection[1] };

            // Check if the next position is in bounds
            if (!IsInBounds(nextPos, input))
            {
                return false; // The guard left the grid
            }

            // Mark the current position as visited if it hasn't been visited before
            var currentState = (guardPos[0], guardPos[1], currentDirection[0], currentDirection[1]);
            if (!visitedStates.Contains(currentState))
            {
                visitedStates.Add(currentState);
                input[guardPos[1], guardPos[0]] = 'X'; // Mark the current position as visited
            }

            // Check if the next position is a wall
            if (input[nextPos[1], nextPos[0]] == '#')
            {
                // rotate the direction guard 90 degrees to the right
                currentDirection = new int[2] { -currentDirection[1], currentDirection[0] };
            }
            else
            {
                // Move the guard to the next position
                guardPos = nextPos;
            }

            return true; // The guard moved successfully
        }


        public static bool IsInBounds(int[] pos, char[,] grid)
        {
            return pos[0] >= 0 && pos[0] < grid.GetLength(1) && pos[1] >= 0 && pos[1] < grid.GetLength(0);
        }
    }
}