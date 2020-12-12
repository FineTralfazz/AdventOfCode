using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day10
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
            var last = 0;
            var differences = new List<int> { 0, 0, 1 };
            var adapters = File.ReadLines("../../../input.txt").Select(s => int.Parse(s)).ToList();
            adapters.Sort();
            foreach (var adapter in adapters)
            {
                differences[(adapter - last) - 1]++;
                last = adapter;
            }
            return differences[0] * differences[2];
        }

        static Int64 Solve2()
        {
            var adapters = File.ReadLines("../../../input.txt").Select(s => int.Parse(s)).ToList();
            adapters.Sort();
            adapters.Add(adapters.Last() + 3);
            var cache = new Dictionary<int, Int64>();
            return Recurse(adapters, cache, 0, 0);
        }

        static Int64 Recurse(List<int> adapters, Dictionary<int, Int64> cache, int offset, int startJolt)
        {
            if (cache.ContainsKey(offset))
            {
                return cache[offset];
            }
            if (offset + 1 == adapters.Count)
            {
                return 1;
            }
            Int64 result = 0;
            for (var i = offset; i < adapters.Count; i++)
            {
                var adapter = adapters[i];
                if (adapter - startJolt <= 3)
                {
                    var childResult = Recurse(adapters, cache, i + 1, adapter);
                    cache[i + 1] = childResult;
                    result += childResult;
                }
                else
                {
                    break;
                }
            }
            return result;
        }
    }
}