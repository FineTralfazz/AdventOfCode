using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace day07
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine($"1: {Solve1()}");
            Console.WriteLine($"2: {Solve2()}");
            //Console.ReadLine();
        }

        static int Solve1()
        {
            var bagRules = new Dictionary<string, List<string>>(); // inner color, outer colors
            foreach (var line in File.ReadLines("../../../input.txt"))
            {
                var halves = line.Split(" contain ");
                var outerColor = ParseBag(halves[0]).Item2;
                
                foreach (var inner in halves[1].Split(','))
                {
                    var innerColor = ParseBag(inner).Item2;
                    if (!bagRules.ContainsKey(innerColor))
                    {
                        bagRules.Add(innerColor, new List<string>());
                    }
                    bagRules[innerColor].Add(outerColor);
                }
            }
            var visited = new HashSet<string>();
            Recurse1(bagRules, visited, "shiny gold");
            return visited.Count - 1;
        }

        static void Recurse1(Dictionary<string, List<string>> bagRules, HashSet<string> visited, string color)
        {
            visited.Add(color);
            if (bagRules.ContainsKey(color))
            {
                foreach (var outerColor in bagRules[color])
                {
                    if (!visited.Contains(outerColor))
                    {
                        Recurse1(bagRules, visited, outerColor);
                    }
                }
            }
        }

        static int Solve2()
        {
            var bagRules = new Dictionary<string, List<Tuple<int, string>>>(); // outer color, inner (count, color)s
            foreach (var line in File.ReadLines("../../../input.txt"))
            {
                var halves = line.Split(" contain ");
                var outerColor = ParseBag(halves[0]).Item2;
                if (!bagRules.ContainsKey(outerColor))
                {
                    bagRules.Add(outerColor, new List<Tuple<int, string>>());
                }

                foreach (var inner in halves[1].Split(','))
                {
                    bagRules[outerColor].Add(ParseBag(inner));
                }
            }
            return Recurse2(bagRules, "shiny gold") - 1;
        }

        static int Recurse2(Dictionary<string, List<Tuple<int, string>>> bagRules, string outerColor)
        {
            var result = 1;
            foreach (var innerBag in bagRules[outerColor])
            {
                if (innerBag.Item2 != "no other")
                {
                    result += innerBag.Item1 * Recurse2(bagRules, innerBag.Item2);
                }
            }
            return result;
        }

        static Tuple<int, string> ParseBag(string description)
        {
            description = description.TrimEnd('.').Trim();
            var count = 1;
            var groups = Regex.Match(description, "([0-9]*) ?(.+) bags?").Groups;
            if (groups[1].Value != "")
            {
                count = int.Parse(groups[1].Value);
            }
            return new Tuple<int, string>(count, groups[2].Value);
        }
    }
}
