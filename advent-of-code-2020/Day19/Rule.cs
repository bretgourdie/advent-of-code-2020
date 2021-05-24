using System;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_2020.Day19
{
    class Rule
    {
        public readonly int Id;
        public readonly string RuleText;

        public Rule(string line)
        {
            var split = line.Split(':');
            Id = int.Parse(split[0]);
            RuleText = split[1]
                .TrimStart()
                .Replace("\"", String.Empty);
        }

        public string GetTest(IDictionary<int, Rule> rulesById)
        {
            if (isSingleLetter(RuleText))
            {
                return RuleText;
            }

            else
            {
                return expand(RuleText, rulesById);
            }
        }

        private string expand(
            string text,
            IDictionary<int, Rule> rulesById)
        {
            var sb = new StringBuilder();

            sb.Append("(");

            var splitText = text.Split(' ');

            foreach (var segment in splitText)
            {
                if (int.TryParse(segment, out int ruleId))
                {
                    sb.Append(rulesById[ruleId].GetTest(rulesById));
                }

                else
                {
                    sb.Append(segment);
                }
            }

            sb.Append(")");

            return sb.ToString();
        }

        private bool isSingleLetter(string ruleText)
        {
            return ruleText.Length == 1;
        }
    }
}
