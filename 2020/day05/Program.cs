using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day05
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer1 = 0;
            var answer2 = 0;
            var seatIds = new List<int>();
            foreach (var line in File.ReadLines("../../../input.txt"))
            {
                var seatId = SeatId(line);
                answer1 = Math.Max(answer1, seatId);
                seatIds.Add(seatId);
            }
            answer2 = Solve2(seatIds);
            Console.WriteLine($"1: {answer1}");
            Console.WriteLine($"2: {answer2}");
            Console.ReadLine();
        }

        static int SeatId(string pass)
        {
            var row = 0;
            foreach (var c in pass.Take(7))
            {
                row = row << 1;
                if (c == 'B')
                {
                    row++;
                }
            }

            var column = 0;
            foreach (var c in pass.Skip(7).Take(3))
            {
                column = column << 1;
                if (c == 'R')
                {
                    column++;
                }
            }

            return row * 8 + column;
        }

        static int Solve2(List<int> seatIds)
        {
            var expected = 75;
            seatIds.Sort();
            foreach (var id in seatIds)
            {
                if (id != expected)
                {
                    return expected;
                }
                expected++;
            }
            throw new Exception("No answer!");
        }
    }
}
