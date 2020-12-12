using System;
using System.IO;
using System.Text.RegularExpressions;

namespace day12
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
            var ship = new Ship1();
            foreach (var line in File.ReadLines("../../../input.txt"))
            {
                var matches = Regex.Match(line, "([A-Z])([0-9]+)").Groups;
                var command = matches[1].Value[0];
                var number = int.Parse(matches[2].Value);
                ship.RunCommand(command, number);
            }
            return ship.ManhattanDistance();
        }

        static Int64 Solve2()
        {
            var ship = new Ship2();
            foreach (var line in File.ReadLines("../../../input.txt"))
            {
                var matches = Regex.Match(line, "([A-Z])([0-9]+)").Groups;
                var command = matches[1].Value[0];
                var number = int.Parse(matches[2].Value);
                ship.RunCommand(command, number);
            }
            return ship.ManhattanDistance();
        }
    }

    class Ship1
    {
        int _degrees = 90;
        int _x = 0;
        int _y = 0;

        public void RunCommand(char command, int number)
        {
            switch (command)
            {
                case ('N'):
                    _y += number;
                    break;
                case ('S'):
                    _y -= number;
                    break;
                case ('E'):
                    _x += number;
                    break;
                case ('W'):
                    _x -= number;
                    break;
                case ('L'):
                    _degrees -= number;
                    break;
                case ('R'):
                    _degrees += number;
                    break;
                case ('F'):
                    switch (GetDegrees())
                    {
                        case (0):
                            _y += number;
                            break;
                        case (90):
                            _x += number;
                            break;
                        case (180):
                            _y -= number;
                            break;
                        case (270):
                            _x -= number;
                            break;
                        default:
                            throw new Exception();
                    }
                    break;
                default:
                    throw new Exception();
            }
            //Console.WriteLine($"{GetDegrees()} {_x} {_y}");
        }

        public int GetDegrees()
        {
            if (_degrees >= 0)
            {
                return _degrees % 360;
            }
            else
            {
                return 360 - (Math.Abs(_degrees) % 360);
            }
        }

        public int ManhattanDistance()
        {
            return Math.Abs(_x) + Math.Abs(_y);
        }
    }

    class Ship2
    {
        int _x = 0;
        int _y = 0;
        int _waypointX = 10;
        int _waypointY = 1;

        public void RunCommand(char command, int number)
        {
            switch (command)
            {
                case ('N'):
                    _waypointY += number;
                    break;
                case ('S'):
                    _waypointY -= number;
                    break;
                case ('E'):
                    _waypointX += number;
                    break;
                case ('W'):
                    _waypointX -= number;
                    break;
                case ('L'):
                    Rotate(number * -1);
                    break;
                case ('R'):
                    Rotate(number);
                    break;
                case ('F'):
                    _x += _waypointX * number;
                    _y += _waypointY * number;
                    break;
                default:
                    throw new Exception();
            }
            //Console.WriteLine($"{_waypointX} {_waypointY} {_x} {_y}");
        }

        public int GetDegrees(int degrees)
        {
            if (degrees >= 0)
            {
                return degrees % 360;
            }
            else
            {
                return 360 - (Math.Abs(degrees) % 360);
            }
        }

        public int ManhattanDistance()
        {
            return Math.Abs(_x) + Math.Abs(_y);
        }

        void Rotate(int degrees)
        {
            switch (GetDegrees(degrees))
            {
                case (0):
                    break;
                case (90):
                    {
                        var newX = _waypointY;
                        var newY = _waypointX * -1;
                        _waypointX = newX;
                        _waypointY = newY;
                    }
                    break;
                case (180):
                    _waypointX *= -1;
                    _waypointY *= -1;
                    break;
                case (270):
                    {
                        var newX = _waypointY * -1;
                        var newY = _waypointX;
                        _waypointX = newX;
                        _waypointY = newY;
                    }
                    break;
                default:
                    throw new Exception();
            }
        }
    }
}
