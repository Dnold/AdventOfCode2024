namespace AdventOfCode2024
{
    public static class Day6
    {
        static int[] guardPos = new int[2];
        static int[] currentDirection = new int[2] { 0, -1 };
        static List<int[]> visited = new List<int[]>();
        static int[] cachedGuardPos = new int[2];
        static char[,] input = new char[0, 0];

        public static void Run()
        {
            Console.WriteLine("Day 6");
            input = GetInput(@"D:\day6.txt");
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
            Part2();
        }

        public static void Part1()
        {
            int count = 0;
            while (Step())
            {
                count++;
            }
            Console.WriteLine("Anzahl der besuchten Felder: " + (visited.Count + 1));
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
            Console.WriteLine("Anzahl der 'X'-Felder: " + xCount);
        }

        public static void Part2()
        {
            int blockCount = 0;
            for (int y = 0; y < input.GetLength(0); y++)
            {
                for (int x = 0; x < input.GetLength(1); x++)
                {
                    input = GetInput(@"D:\day6.txt");
                    visited.Clear();
                    visitedStates.Clear();
                    currentDirection = new int[2] { 0, -1 };
                    guardPos = new int[] { cachedGuardPos[0], cachedGuardPos[1] };
                    if (input[y, x] != '#' && input[y, x] != '^')
                    {
                        input[y, x] = '#';
                        int maxCount = 6179;
                        int count = 0;
                        while (count <= maxCount)
                        {
                            if (!Step())
                            {
                                break; // Wächter kann sich nicht mehr bewegen
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
        }

        static HashSet<(int x, int y, int dx, int dy)> visitedStates = new HashSet<(int, int, int, int)>();

        public static bool DetectLooping()
        {
            // Erstelle den aktuellen Zustand (Position und Richtung)
            var currentState = (guardPos[0], guardPos[1], currentDirection[0], currentDirection[1]);

            // Prüfe, ob dieser Zustand bereits besucht wurde
            if (visitedStates.Contains(currentState))
            {
                return true; // Schleife erkannt
            }

            // Zustand hinzufügen, wenn er neu ist
            visitedStates.Add(currentState);
            return false; // Keine Schleife gefunden
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
            // Nächste Position berechnen
            int[] nextPos = new int[2] { guardPos[0] + currentDirection[0], guardPos[1] + currentDirection[1] };

            // Prüfe, ob die nächste Position innerhalb des Gitters liegt
            if (!IsInBounds(nextPos, input))
            {
                return false; // Wächter verlässt das Gitter
            }

            // Markiere die aktuelle Position als besucht, wenn noch nicht geschehen
            var currentState = (guardPos[0], guardPos[1], currentDirection[0], currentDirection[1]);
            if (!visitedStates.Contains(currentState))
            {
                visitedStates.Add(currentState);
                input[guardPos[1], guardPos[0]] = 'X'; // Markiere das Feld
            }

            // Prüfe, ob die nächste Position ein Hindernis ist
            if (input[nextPos[1], nextPos[0]] == '#')
            {
                // Drehe die Richtung nach rechts
                currentDirection = new int[2] { -currentDirection[1], currentDirection[0] };
            }
            else
            {
                // Bewege den Wächter auf die nächste Position
                guardPos = nextPos;
            }

            return true; // Wächter hat sich bewegt
        }


        public static bool IsInBounds(int[] pos, char[,] grid)
        {
            return pos[0] >= 0 && pos[0] < grid.GetLength(1) && pos[1] >= 0 && pos[1] < grid.GetLength(0);
        }
    }
}