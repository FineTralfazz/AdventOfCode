using System;
using System.Collections.Generic;
using System.IO;

namespace day08
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
            var code = new List<Tuple<string, int>>();
            var visited = new HashSet<int>();
            foreach (var line in File.ReadLines("../../../input.txt"))
            {
                var halves = line.Split(' ');
                code.Add(new Tuple<string, int>(halves[0], int.Parse(halves[1])));
            }
            var instructionPointer = 0;
            var accumulator = 0;
            while (true)
            {
                if (visited.Contains(instructionPointer))
                {
                    return accumulator;
                }
                else
                {
                    visited.Add(instructionPointer);
                    var instruction = code[instructionPointer];
                    switch (instruction.Item1)
                    {
                        case "nop":
                            break;
                        case "acc":
                            accumulator += instruction.Item2;
                            break;
                        case "jmp":
                            instructionPointer += instruction.Item2 - 1;
                            break;
                    }
                    instructionPointer++;
                }
            }
        }

        static int Solve2()
        {
            var code = new List<Tuple<string, int>>();
            var visited = new HashSet<int>();
            foreach (var line in File.ReadLines("../../../input.txt"))
            {
                var halves = line.Split(' ');
                code.Add(new Tuple<string, int>(halves[0], int.Parse(halves[1])));
            }
            code.Add(new Tuple<string, int>("chris_ret", 0));
            // Patch corrupt instruction
            code[210] = new Tuple<string, int>("nop", 0);

            var instructionPointer = 0;
            var accumulator = 0;
            while (true)
            {
                if (visited.Contains(instructionPointer))
                {
                    Console.WriteLine(instructionPointer);
                    return accumulator;
                }
                else
                {
                    visited.Add(instructionPointer);
                    var instruction = code[instructionPointer];
                    Console.WriteLine($"{instructionPointer}: {instruction.Item1} {instruction.Item2}");
                    switch (instruction.Item1)
                    {
                        case "nop":
                            break;
                        case "acc":
                            accumulator += instruction.Item2;
                            break;
                        case "jmp":
                            instructionPointer += instruction.Item2 - 1;
                            break;
                        case "chris_ret": // custom instruction to mark the end of the code
                            return accumulator;
                    }
                    instructionPointer++;
                }
            }
        }
    }
}
