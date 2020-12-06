using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day06
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine($"1: {Solve1()}");
            Console.WriteLine($"2: {Solve2()}");
            Console.ReadLine();
        }

        static int Solve1()
        {
            var result = 0;
            var groupAnswers = new HashSet<char>();
            foreach (var line in File.ReadLines("../../../input.txt"))
            {
                if (line == "")
                {
                    result += groupAnswers.Count;
                    groupAnswers.Clear();
                }
                foreach (var c in line)
                {
                    groupAnswers.Add(c);
                }
            }
            return result;
        }

        static int Solve2()
        {
            var result = 0;
            var groupAnswers = new List<List<char>>();
            foreach (var line in File.ReadLines("../../../input.txt"))
            {
                if (line == "")
                {
                    var intersection = new List<char>(groupAnswers.First());
                    foreach (var list in groupAnswers)
                    {
                        intersection = intersection.Intersect(list).ToList();
                    }
                    result += intersection.Count;
                    groupAnswers.Clear();
                }
                else
                {
                    var myAnswers = new List<char>();
                    foreach (var c in line)
                    {
                        myAnswers.Add(c);
                    }
                    groupAnswers.Add(myAnswers);
                }
            }
            return result;
        }
    }
}
