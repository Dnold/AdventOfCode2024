using System.Text.RegularExpressions;
namespace AdventOfCode2024
{
    public static class Day3
    {
        public static void Run()
        {
            Console.WriteLine("######################################################");
            Console.WriteLine("Day 3");
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(basePath, "Input", "Day3.txt");
            string input = ReadDocument(relativePath);
            Console.WriteLine("-------------------------");
            Part1(input);
            Console.WriteLine("-------------------------");
            Part2(input);
            Console.WriteLine("-------------------------");
            Console.WriteLine("######################################################");
           
        }
        public static void Part1(string input)
        {
            Console.WriteLine("Part 1");
            int result = SumOfMultiplications(input);
            Console.WriteLine("The Sum of the Multiplications is: "+ result);
        }
        public static void Part2(string input)
        {
            Console.WriteLine("Part 2");
            int result = SumOfMultiplicationsWithDo(input);
            Console.WriteLine("The Sum of the Multiplications is: " + result);
        }
        public static int SumOfMultiplications(string input)
        {
            int sum = 0;
            string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(input);

            foreach (Match match in matches)
            {
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);
                sum += x * y;
            }

            return sum;
        }
        public static int SumOfMultiplicationsWithDo(string input)
        {
            int sum = 0;
            bool isEnabled = true;
            string pattern = @"(mul\((\d{1,3}),(\d{1,3})\))|(do\(\))|(don't\(\))";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(input);

            foreach (Match match in matches)
            {
                if (match.Value.StartsWith("mul") && isEnabled)
                {
                    int x = int.Parse(match.Groups[2].Value);
                    int y = int.Parse(match.Groups[3].Value);
                    sum += x * y;
                }
                else if (match.Value == "do()")
                {
                    isEnabled = true;
                }
                else if (match.Value == "don't()")
                {
                    isEnabled = false;
                }
            }

            return sum;
        }

        public static string ReadDocument(string path)
        {
            string s = File.ReadAllText(path);
            return s;
        }
    }
}
