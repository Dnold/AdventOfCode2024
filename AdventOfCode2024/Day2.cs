using System.Collections.Generic;
using System;
using System.IO;
namespace AdventOfCode2024
{
public static class Day2
{
    public static void Run()
    {
        List<int[]> input = GetInput(@"D:\nums.txt.txt");
        int count = 0;
        foreach (int[] inputs in input)
        {
            bool isValid = CheckIfHasValidState(inputs);
            if (isValid) { count++; }
        }

        Console.WriteLine(count);
        Console.WriteLine(IsValid(new int[] { 7, 6, 4, 2, 1 }));


    }
    public static bool CheckIfHasValidState(int[] nums)
    {
        bool hasValidState = false;
        for(int i = 0; i < nums.Length; i++)
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
