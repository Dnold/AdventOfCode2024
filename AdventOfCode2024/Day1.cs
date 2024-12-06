using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace AdventOfCode2024
{
   
    public struct Value
    {
        public int rightValue { get; set; }
        public int leftValue { get; set; }
    }
    public static class Day1
    {
        public static void Run()
        {
            string[] locationNumbers = ReadLinesOfDocument(@"D:\loc.txt");
            List<Value> values = GetValues(locationNumbers);
            int totalDistance = CalculateSimilarityScore(values);
            Console.WriteLine(totalDistance);
        }

        public static int CalculateTotalDistance(List<Value> values)
        {
            var leftValues = values.Select(v => v.leftValue).OrderBy(v => v).ToList();
            var rightValues = values.Select(v => v.rightValue).OrderBy(v => v).ToList();

            int totalDistance = 0;
            for (int i = 0; i < leftValues.Count; i++)
            {
                totalDistance += Math.Abs(leftValues[i] - rightValues[i]);
            }

            return totalDistance;
        }
        public static List<string[]> ConvertValuesToStringArray(List<Value> values)
        {
            List<string[]> vals = new List<string[]>();
            foreach (var v in values)
            {
                string[] array = new string[2];
                array[0] = string.Join("", v.leftValue);
                array[1] = string.Join("", v.rightValue);
                vals.Add(array);

            }
            return vals;
        }
        public static int CalculateSimilarityScore(List<Value> values)
        {
            int sum = 0;
            foreach (var value in values)
            {
                int left = value.leftValue;
                int rightCount = values.Count(v => v.rightValue == left);
                sum += left * rightCount;
            }
            return sum;
        }

        public static List<Value> GetValues(string[] locationNumbers)
        {
            List<Value> values = new List<Value>();
            foreach (string locationNumber in locationNumbers)
            {
                string[] locationLists = locationNumber.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                Value value = new Value
                {
                    leftValue = Convert.ToInt32(locationLists[0]),
                    rightValue = Convert.ToInt32(locationLists[1])
                };
                values.Add(value);
            }
            return values;
        }

        public static string[] ReadLinesOfDocument(string path)
        {
            return File.ReadAllLines(path);
        }
    }

}
