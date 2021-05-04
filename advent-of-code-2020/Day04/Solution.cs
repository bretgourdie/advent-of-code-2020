using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2020.Day04
{
    class Solution : AdventSolution<string>
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            validatePassportData(
                inputData,
                Enumerable.Empty<Validation>()
            );
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            var validations = new Validation[]
            {
                new Validation("byr", x =>
                    inNumberRange(x, 1920, 2002)),
                new Validation("iyr", x =>
                    inNumberRange(x, 2010, 2020)),
                new Validation("eyr", x=>
                    inNumberRange(x, 2020, 2030)),
                new Validation("hgt", x => inMeasurementRange(x)),
                new Validation("hcl", x => isHex(x)),
                new Validation("ecl", x => isEyeColor(x)),
                new Validation("pid", x => isPassportId(x))
            };

            validatePassportData(
                inputData,
                validations);
        }

        private bool isPassportId(string x)
        {
            return
                long.TryParse(x, out long output)
                && x.Length == 9;
        }

        private bool isHex(string x)
        {
            if (x.First() != '#') return false;

            return long.TryParse(
                x.Substring(1),
                System.Globalization.NumberStyles.HexNumber,
                null,
                out long output);
        }

        private bool isEyeColor(string x)
        {
            string[] eyeColors = new[]
            {
                "amb",
                "blu",
                "brn",
                "gry",
                "grn",
                "hzl",
                "oth"
            };

            return eyeColors.Contains(x);
        }

        private bool inMeasurementRange(
            string numberWithMeasurement)
        {
            var suffix = numberWithMeasurement.Substring(
                numberWithMeasurement.Length - 2);

            var number = numberWithMeasurement.Substring(
                0,
                numberWithMeasurement.Length - 2);

            if (suffix == "cm")
            {
                return inNumberRange(number, 150, 193);
            }

            else if (suffix == "in")
            {
                return inNumberRange(number, 59, 76);
            }

            return false;
        }

        private bool inNumberRange(
            string number,
            int min,
            int max)
        {
            return int.TryParse(number, out int iNumber)
                   && min <= iNumber && iNumber <= max;
        }

        protected void validatePassportData(
            IList<string> inputData,
            IEnumerable<Validation> validations)
        {
            int totalValidPassports = 0;
            var currentPassport = new Passport();

            foreach (var line in inputData)
            {
                if (line == String.Empty)
                {
                    if (currentPassport.IsValid()
                    && passportPassesValidations(currentPassport, validations))
                    {
                        totalValidPassports += 1;
                    }

                    currentPassport = new Passport();
                }

                else
                {
                    currentPassport.AddAttributes(line);
                }
            }

            Console.WriteLine($"Found {totalValidPassports} valid passports.");
        }

        private bool passportPassesValidations(
            Passport passport,
            IEnumerable<Validation> validations)
        {
            foreach (var validation in validations)
            {
                if (passport.HasAttribute(validation.Attribute))
                {
                    var attributeValue = passport.GetAttribute(validation.Attribute);
                    if (!validation.IsValid(attributeValue))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
