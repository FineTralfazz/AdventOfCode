using System;
using System.IO;
using System.Text.RegularExpressions;

namespace day02
{
    class Program
    {
        static void Main(string[] args)
        {
            var count1 = 0;
            var count2 = 0;
            foreach (var line in File.ReadLines("../../../input.txt"))
            {
                if (Valid1(line))
                {
                    count1++;
                }

                if (Valid2(line))
                {
                    count2++;
                }
            }
            Console.WriteLine($"Part 1: {count1}");
            Console.WriteLine($"Part 2: {count2}");
        }

        static bool Valid1(string input)
        {
            var groups = Regex.Match(input, "([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)").Groups;
            var lowerBound = Int32.Parse(groups[1].Value);
            var upperBound = Int32.Parse(groups[2].Value);
            var character = groups[3].Value[0];
            var password = groups[4].Value;
            var count = 0;
            foreach (char c in password)
            {
                if (c == character)
                {
                    count++;
                }
            }
            return lowerBound <= count && count <= upperBound;
        }

        static bool Valid2(string input)
        {
            var groups = Regex.Match(input, "([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)").Groups;
            var position1 = Int32.Parse(groups[1].Value) - 1;
            var position2 = Int32.Parse(groups[2].Value) - 1;
            var character = groups[3].Value[0];
            var password = groups[4].Value;
            var match1 = password[position1] == character;
            var match2 = password[position2] == character;
            return match1 ^ match2;
        }
    }
}
