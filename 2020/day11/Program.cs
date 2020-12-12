using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day11
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
            var oldSeats = File.ReadLines("../../../input.txt").ToList();
            while (true)
            {
                var newSeats = new List<string>(oldSeats);
                for (var y = 0; y < oldSeats.Count(); y++)
                {
                    for (var x = 0; x < oldSeats.First().Count(); x++)
                    {
                        if (oldSeats[y][x] == 'L' && AdjacentsOccupied1(oldSeats, x, y) == 0)
                        {
                            var newRow = newSeats[y].ToCharArray();
                            newRow[x] = '#';
                            newSeats[y] = new string(newRow);
                        }
                        else if (oldSeats[y][x] == '#' && AdjacentsOccupied1(oldSeats, x, y) >= 4)
                        {
                            var newRow = newSeats[y].ToCharArray();
                            newRow[x] = 'L';
                            newSeats[y] = new string(newRow);
                        }
                    }
                }
                if (Enumerable.SequenceEqual(oldSeats, newSeats))
                {
                    var result = 0;
                    foreach (var row in newSeats)
                    {
                        foreach (var seat in row)
                        {
                            if (seat == '#')
                            {
                                result++;
                            }
                        }
                    }
                    return result;
                }
                //foreach (var row in newSeats)
                //{
                //    Console.WriteLine(row);
                //}
                //Console.WriteLine("");
                oldSeats = newSeats;
            }
        }

        static Int64 Solve2()
        {
            var oldSeats = File.ReadLines("../../../input.txt").ToList();
            while (true)
            {
                var newSeats = new List<string>(oldSeats);
                for (var y = 0; y < oldSeats.Count(); y++)
                {
                    for (var x = 0; x < oldSeats.First().Count(); x++)
                    {
                        if (oldSeats[y][x] == 'L' && AdjacentsOccupied2(oldSeats, x, y) == 0)
                        {
                            var newRow = newSeats[y].ToCharArray();
                            newRow[x] = '#';
                            newSeats[y] = new string(newRow);
                        }
                        else if (oldSeats[y][x] == '#' && AdjacentsOccupied2(oldSeats, x, y) >= 5)
                        {
                            var newRow = newSeats[y].ToCharArray();
                            newRow[x] = 'L';
                            newSeats[y] = new string(newRow);
                        }
                    }
                }
                if (Enumerable.SequenceEqual(oldSeats, newSeats))
                {
                    var result = 0;
                    foreach (var row in newSeats)
                    {
                        foreach (var seat in row)
                        {
                            if (seat == '#')
                            {
                                result++;
                            }
                        }
                    }
                    return result;
                }
                //foreach (var row in newSeats)
                //{
                //    Console.WriteLine(row);
                //}
                //Console.WriteLine("");
                oldSeats = newSeats;
            }
        }

        static int AdjacentsOccupied1(List<string> seats, int x, int y)
        {
            var result = 0;
            for (var adjX = -1; adjX < 2; adjX++)
            {
                for (var adjY = -1; adjY < 2; adjY++)
                {
                    if (!(adjX == 0 && adjY == 0) && Occupied(seats, x + adjX, y + adjY))
                    {
                        result++;
                    }
                }
            }
            //Console.WriteLine($"{x} {y} {result}");
            return result;
        }

        static int AdjacentsOccupied2(List<string> seats, int x, int y)
        {
            var result = 0;

            // W
            if (RayTrace(seats, x, y, -1, 0))
            {
                result++;
            }

            // E
            if (RayTrace(seats, x, y, 1, 0))
            {
                result++;
            }

            // N
            if (RayTrace(seats, x, y, 0, -1))
            {
                result++;
            }

            // S
            if (RayTrace(seats, x, y, 0, 1))
            {
                result++;
            }

            // NW
            if (RayTrace(seats, x, y, -1, -1))
            {
                result++;
            }

            // SW
            if (RayTrace(seats, x, y, -1, 1))
            {
                result++;
            }

            // NE
            if (RayTrace(seats, x, y, 1, -1))
            {
                result++;
            }

            // SE
            if (RayTrace(seats, x, y, 1, 1))
            {
                result++;
            }

            //Console.WriteLine($"{x} {y} {result}");
            return result;
        }

        static bool RayTrace(List<string> seats, int startX, int startY, int dirX, int dirY)
        {
            var x = startX;
            var y = startY;
            while (true)
            {
                x += dirX;
                y += dirY;
                if (x < 0 || y < 0 || x >= seats[0].Length || y >= seats.Count)
                {
                    return false;
                }
                switch (seats[y][x])
                {
                    case '.':
                        continue;
                    case 'L':
                        return false;
                    case '#':
                        return true;
                }
            }
        }

        static bool Occupied(List<string> seats, int x, int y)
        {
            if (x >= 0 && x < seats[0].Count() && y >= 0 && y < seats.Count())
            {
                //Console.WriteLine($"{x} {y} occupied");
                return seats[y][x] == '#';
            }
            else
            {
                //Console.WriteLine($"{x} {y} unoccupied");
                return false;
            }
        }
    }
}
