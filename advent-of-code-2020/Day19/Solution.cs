using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace advent_of_code_2020.Day19
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            findMatchesForReceivedMessages(inputData, new string[] {});
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            var changes = new string[]
            {
                "8: ( 42 )+ ",
                "11: (?'Open' 42 )+ (?'Close-Open' 31 )+ (?(Open)(?!))"
            };

            findMatchesForReceivedMessages(
                inputData,
                changes);
        }

        private void findMatchesForReceivedMessages(
            IList<string> inputData,
            IList<string> changes)
        {
            var rule0 = new RuleSet(getRulesSection(inputData), changes).GetTest();

            var regex = new Regex("^" + rule0 + "$");

            int numberOfMatches = 0;

            foreach (var receivedMessage in getReceivedMessagesSection(inputData))
            {
                if (regex.IsMatch(receivedMessage))
                {
                    numberOfMatches += 1;
                }
            }

            Console.WriteLine($"The number of received messages that match are {numberOfMatches}");
        }

        private IList<string> getRulesSection(IList<string> inputData)
        {
            return inputData
                .Where(x => x.Contains(":"))
                .ToList();
        }

        private IList<string> getReceivedMessagesSection(IList<string> inputData)
        {
            return inputData
                .Where(x => !String.IsNullOrWhiteSpace(x) && !x.Contains(":"))
                .ToList();
        }

        protected override string getExample2DatasetFilename()
        {
            return "example2.txt";
        }
    }
}
