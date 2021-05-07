using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day02
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            performWorkForProblems(
                new SledPolicy(),
                inputData);
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            performWorkForProblems(
                new TobogganPolicy(),
                inputData);
        }

        protected void performWorkForProblems(
            IPolicy policy,
            IList<string> inputData)
        {
            int validPasswords = 0;

            foreach (var line in inputData)
            {
                var ruleAndPassword = line.Split(new string[] {": "}, StringSplitOptions.RemoveEmptyEntries);
                var rule = ruleAndPassword[0];
                var password = ruleAndPassword[1];
                var numbersAndLetter = rule.Split(' ');
                var letter = numbersAndLetter[1].Single();
                var numbers = numbersAndLetter[0].Split('-');
                var firstNumber = int.Parse(numbers[0]);
                var secondNumber = int.Parse(numbers[1]);

                var passes = policy.Passes(
                    firstNumber,
                    secondNumber,
                    letter,
                    password);

                if (passes)
                {
                    validPasswords += 1;
                }
            }

            Console.WriteLine($"There are {validPasswords} valid passwords.");
        }
    }
}
