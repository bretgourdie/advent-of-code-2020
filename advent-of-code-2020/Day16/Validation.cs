using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day16
{
    class Validation
    {
        public string FieldName { get; private set; }
        private IEnumerable<Range> ranges;

        public Validation(string line)
        {
            var fieldAndRules = line.Split(':');

            FieldName = fieldAndRules[0];

            var ruleStrings = fieldAndRules[1]
                .TrimStart()
                .Split(
                    new string[] {"or"},
                    StringSplitOptions.RemoveEmptyEntries);

            ranges = ruleStrings.Select(x => new Range(x));
        }

        public bool InRange(int number)
        {
            return ranges.Any(range => range.Contains(number));
        }

        private class Range
        {
            private readonly int high;
            private readonly int low;

            public Range(string rangeString)
            {
                var split = rangeString.Split('-');
                low = int.Parse(split[0]);
                high = int.Parse(split[1]);
            }

            public bool Contains(int number)
            {
                return low <= number && number <= high;
            }
        }

        public override string ToString()
        {
            return FieldName;
        }
    }
}
