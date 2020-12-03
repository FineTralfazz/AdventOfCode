using System;
using System.Collections.Generic;
using System.IO;

namespace day03
{
    class Program
    {
        static void Main(string[] args)
        {
            var mountain = BuildMountain();
            Console.WriteLine($"1: {Sled(mountain, 3, 1)}");
            Console.WriteLine($"2: {Sled(mountain, 1, 1) * Sled(mountain, 3, 1) * Sled(mountain, 5, 1) * Sled(mountain, 7, 1) * Sled(mountain, 1, 2)}");
        }

        // It would be more efficient to generate the mountain on the fly, but this seemed easier at the time.
        static List<List<bool>> BuildMountain()
        {
            var mountain = new List<List<bool>>();
            foreach (var line in File.ReadLines("../../../input.txt"))
            {
                var row = new List<bool>();
                foreach (var c in line)
                {
                    if (c == '#')
                    {
                        row.Add(true);
                    }
                    else
                    {
                        row.Add(false);
                    }
                }
                mountain.Add(row);
            }
            return mountain;
        }

        static Int64 Sled(List<List<bool>> mountain, int right, int down)
        {
            var count = 0;
            var x = 0;
            for (var y = 0; y < mountain.Count; y += down) {
                if (mountain[y][x % (mountain[0].Count)])
                {
                    count++;
                }
                x += right;
            }
            return count;
        }
    }
}
