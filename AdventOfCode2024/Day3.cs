using System.Text.RegularExpressions;
namespace AdventOfCode2024
{
    public static class Day3
    {
        public static void Run()
        {
            string input = ReadDocument(@"D:\mult.txt");
            int result = SumOfMultiplications(input);
            Console.WriteLine(result);
        }

        public static int SumOfMultiplications(string input)
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
