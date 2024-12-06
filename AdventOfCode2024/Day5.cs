namespace AdventOfCode2024
{
    public static class Day5
    {
        public static void Run()
        {
            Console.WriteLine("######################################################");
            Console.WriteLine("Day 5");
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(basePath, "Input", "Day5.txt");
            var input = GetInput(relativePath);
            Console.WriteLine("-------------------------");
            Part1(input);
            Console.WriteLine("-------------------------");
            Part2(input);
            Console.WriteLine("-------------------------");
            Console.WriteLine("######################################################");
        }
        public static void Part1(Tuple<int[][], int[][]> input)
        {
            Console.WriteLine("Part 1");
            int count = 0;
            foreach (var item in input.Item2)
            {
                if (!IsUpdateValid(input.Item1, item))
                {
                    count += item[(item.Length - 1) / 2];
                }
            }
            Console.WriteLine("Sum of Middle Number of Sorted Updates:" + count);
        }
        public static void Part2(Tuple<int[][], int[][]> input)
        {
            Console.WriteLine("Part 2");
         
            int count = 0;
            foreach (var item in input.Item2)
            {
                if (!IsUpdateValid(input.Item1, item))
                {
                    int[] sorted = SortUpdate(item, input.Item1);
                  
                  
                    count += sorted[(item.Length - 1) / 2];
                }
                else
                {
                
                }
               
            }
            Console.WriteLine("Sum of Middle Number of non Valid now Sorted Updates: "+count);
        }

        public static int[] SortUpdate(int[] update, int[][] rules)
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            HashSet<int> updateSet = new HashSet<int>(update);

            foreach (int[] rule in rules)
            {
                if (updateSet.Contains(rule[0]) && updateSet.Contains(rule[1]))
                {
                    if (!graph.ContainsKey(rule[0]))
                    {
                        graph[rule[0]] = new List<int>();
                    }
                    graph[rule[0]].Add(rule[1]);
                }
            }

            HashSet<int> visited = new HashSet<int>();
            HashSet<int> currentPath = new HashSet<int>();
            List<int> sortedRules = new List<int>();

            void DFS(int node)
            {
                if (currentPath.Contains(node))
                {
                    throw new InvalidOperationException("Graph contains a cycle");
                }
                if (!visited.Contains(node))
                {
                    currentPath.Add(node);
                    if (graph.ContainsKey(node))
                    {
                        foreach (int neighbor in graph[node])
                        {
                            DFS(neighbor);
                        }
                    }
                    currentPath.Remove(node);
                    visited.Add(node);
                    sortedRules.Insert(0, node);
                }
            }

            HashSet<int> allNodes = new HashSet<int>(graph.Keys);
            foreach (var edges in graph.Values)
            {
                foreach (var node in edges)
                {
                    allNodes.Remove(node);
                }
            }
            foreach (int node in allNodes)
            {
                DFS(node);
            }

            foreach (var page in update)
            {
                if (!sortedRules.Contains(page))
                {
                    sortedRules.Add(page);
                }
            }

            return update.OrderBy(page => sortedRules.IndexOf(page)).ToArray();
        }

        public static bool IsUpdateValid(int[][] rules, int[] update)
        {
            foreach (var rule in rules)
            {
                if (update.Contains(rule[0]) && update.Contains(rule[1]))
                {
                    if (Array.IndexOf(update, rule[0]) > Array.IndexOf(update, rule[1]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static Tuple<int[][], int[][]> GetInput(string path)
        {
            List<int[]> rules = new List<int[]>();
            List<int[]> updates = new List<int[]>();
            bool readingRulesDone = false;
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "")
                    {
                        readingRulesDone = true;
                        continue;
                    }
                    if (!readingRulesDone)
                    {
                        string[] data = line.Split("|");
                        int[] rule = new int[] { Convert.ToInt32(data[0]), Convert.ToInt32(data[1]) };
                        rules.Add(rule);
                    }
                    else
                    {
                        string[] data = line.Split(",");
                        int[] update = new int[data.Length];
                        for (int i = 0; i < data.Length; i++)
                        {
                            update[i] = Convert.ToInt32(data[i]);
                        }
                        updates.Add(update);
                    }
                }
            }
            return new Tuple<int[][], int[][]>(rules.ToArray(), updates.ToArray());
        }
    }
}

