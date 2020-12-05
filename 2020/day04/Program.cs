using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace day04
{
    class Program
    {
        static void Main(string[] args)
        {
            var count1 = 0;
            var count2 = 0;
            var currentPassport = new Passport();
            foreach (var line in File.ReadLines("../../../input.txt"))
            {
                if (line == "\n" || line == "")
                {
                    if (currentPassport.Valid1())
                    {
                        count1++;
                    }
                    if (currentPassport.Valid2())
                    {
                        count2++;
                    }
                    currentPassport = new Passport();
                    continue;
                }
                var fields = line.Split(' ');
                foreach (var field in fields)
                {
                    var parts = field.Split(':');
                    currentPassport.Set(parts[0], parts[1]);
                }
            }
            Console.WriteLine($"1: {count1}");
            Console.WriteLine($"2: {count2}");
            Console.ReadLine();
        }
    }

    class Passport
    {
        public string BirthYear;
        public string IssueYear;
        public string ExpirationYear;
        public string Height;
        public string HairColor;
        public string EyeColor;
        public string PassportId;
        public string CountryId;
        
        public void Set(string key, string value)
        {
            switch (key) {
                case "byr":
                    BirthYear = value;
                    break;
                case "iyr":
                    IssueYear = value;
                    break;
                case "eyr":
                    ExpirationYear = value;
                    break;
                case "hgt":
                    Height = value;
                    break;
                case "hcl":
                    HairColor = value;
                    break;
                case "ecl":
                     EyeColor = value;
                    break;
                case "pid":
                    PassportId = value;
                    break;
                case "cid":
                    CountryId = value;
                    break;
                default:
                    throw new Exception("Unrecognized key");
            }
        }

        public bool Valid1()
        {
            return BirthYear != null && IssueYear != null && ExpirationYear != null && Height != null && HairColor != null && EyeColor != null && PassportId != null;
        }

        public bool Valid2()
        {
            if (BirthYear == null || IssueYear == null || ExpirationYear == null || Height == null || HairColor == null || EyeColor == null || PassportId == null)
            {
                return false;
            }

            var birthYear = int.Parse(BirthYear);
            if (!(birthYear >= 1920 && birthYear <= 2002))
            {
                Console.WriteLine($"failed birth year {birthYear}");
                return false;
            }

            var issueYear = int.Parse(IssueYear);
            if (!(issueYear >= 2010 && issueYear <= 2020))
            {
                Console.WriteLine($"failed issue year {issueYear}");
                return false;
            }

            var expirationYear = int.Parse(ExpirationYear);
            if (!(expirationYear >= 2020 && expirationYear <= 2030))
            {
                Console.WriteLine($"failed exp year {expirationYear}");
                return false;
            }

            var heightGroups = Regex.Match(Height, "^([0-9]+)(in|cm)$").Groups;
            if (heightGroups.Count != 3)
            {
                Console.WriteLine($"failed height {Height}");
                return false;
            }
            var heightNumber = int.Parse(heightGroups[1].Value);
            var heightUnit = heightGroups[2].Value;
            if (heightUnit == "cm" && !(heightNumber >= 150 && heightNumber <= 193))
            {
                Console.WriteLine($"failed height {Height} {heightNumber} {heightUnit}");
                return false;
            }
            if (heightUnit == "in" && !(heightNumber >= 59 && heightNumber <= 76))
            {
                Console.WriteLine($"failed height {Height}");
                return false;
            }

            if (!Regex.IsMatch(HairColor, @"^\#[0-9a-f]{6}$"))
            {
                Console.WriteLine($"failed hair {HairColor}");
                return false;
            }

            if (!(new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(EyeColor)))
            {
                Console.WriteLine($"failed eye {EyeColor}");
                return false;
            }

            if (!Regex.IsMatch(PassportId, "^[0-9]{9}$"))
            {
                Console.WriteLine($"failed id {PassportId}");
                return false;
            }

            return true;
        }
    }
}
