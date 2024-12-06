using System.Collections.Generic;
using System;
using System.IO;
namespace AdventOfCode2024
{
    public static class Day2
    {
        public static void Run()
        {
            Console.WriteLine("######################################################");
            Console.WriteLine("Day 2");
            Console.WriteLine("-------------------------");
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Input/Day2.txt");
            List<int[]> input = GetInput(path);
            Part1(input);
            Console.WriteLine("-------------------------");
            Part2(input);
            Console.WriteLine("-------------------------");
            Console.WriteLine("######################################################");
            
            

        }
        public static void Part1(List<int[]> input)
        {
            Console.WriteLine("Part 1");
            int count = 0;
            foreach (int[] report in input)
            {
                bool isValid = IsValid(report);
                if (isValid) { count++; }
            }

            Console.WriteLine("The number of valid reports is: " + count);
        }
        public static void Part2(List<int[]> input)
        {
            Console.WriteLine("Part 2");
            int count = 0;
            foreach (int[] report in input)
            {
                if (CheckIfHasValidState(report))
                {
                    count++;
                   
                }
            }
            Console.WriteLine("The number of reports with a almost valid state " + count);
        }
        public static bool CheckIfHasValidState(int[] nums)
        {
            bool hasValidState = false;
            for (int i = 0; i < nums.Length; i++)
            {
                List<int> numList = nums.ToList();
                numList.RemoveAt(i);

                if (IsValid(numList.ToArray()))
                {
                    hasValidState = true;
                }
            }
            return hasValidState;
        }
        public static bool IsValid(int[] nums)
        {

            if (nums.Length < 2) return false; // Edge case: less than 2 elements

            bool isIncreasing = nums[0] < nums[1];
            for (int i = 0; i < nums.Length - 1; i++)
            {
                int diff = Math.Abs(nums[i + 1] - nums[i]);
                if (diff < 1 || diff > 3)
                {
                    return false; // Difference is not between 1 and 3

                }

                if (isIncreasing && nums[i] >= nums[i + 1])
                {
                    return false; // Should be increasing but found a non-increasing pair
                }
                else if (!isIncreasing && nums[i] <= nums[i + 1])
                {
                    return false; // Should be decreasing but found a non-decreasing pair
                }

            }
            return true;
        }
        public static List<int[]> GetInput(string path)
        {

            return ReadDocument(path);


        }
        public static List<int[]> ReadDocument(string path)
        {
            List<int[]> outputList = new List<int[]>();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(" ");
                    List<int> foundNums = new List<int>();
                    foreach (string c in data)
                    {
                        foundNums.Add(Convert.ToInt32(c + ""));
                    }
                    outputList.Add(foundNums.ToArray());
                }
            }
            return outputList;
        }
    }
}
