using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day09
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine($"1: {Solve1()}");
            Console.WriteLine($"2: {Solve2()}");
            Console.ReadLine();
        }

        static Int64 Solve1()
        {
            const int window = 25;
            var numbers = new List<Int64>();
            foreach (var line in File.ReadLines("../../../input.txt"))
            {
                var match = false;
                var thisNumber = Int64.Parse(line);
                numbers.Add(thisNumber);
                if (numbers.Count > window)
                {
                    var preamble = numbers.SkipLast(1).TakeLast(window).ToList();
                    for (var i = 0; i < window; i++)
                    {
                        for (var j = 0; j < window; j++)
                        {
                            if (i != j)
                            {
                                if (preamble[i] + preamble[j] == thisNumber)
                                {
                                    match = true;
                                }
                            }
                        }
                    }
                    if (!match)
                    {
                        return thisNumber;
                    }
                }
            }
            return -1;
        }

        static Int64 Solve2()
        {
            const Int64 target = 3199139634;
            var numbers = new List<Int64>();
            foreach (var line in File.ReadLines("../../../input.txt"))
            {
                var thisNumber = Int64.Parse(line);
                numbers.Add(thisNumber);
            }

            for (var first = 0; first < numbers.Count; first++){
                for (var window = 2; window < numbers.Count - first; window++)
                {
                    var run = numbers.Skip(first).Take(window).ToList();
                    var sum = run.Sum();
                    if (sum == target)
                    {
                        run.Sort();
                        return run.First() + run.Last();
                    } 
                    else if (sum > target)
                    {
                        break;
                    }
                }
            }
            return -1;
        }
    }
}
