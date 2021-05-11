using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day18
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            evaluateMath(inputData, new BasicMathEvaluator());
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            evaluateMath(inputData, new AdvancedMathEvaluator());
        }

        private void evaluateMath(
            IList<string> inputData,
            IEvaluator evaluator)
        {
            long sum = 0;
            foreach (var line in inputData)
            {
                sum += evaluator.Evaluate(line);
            }

            Console.WriteLine($"The sum of all expressions is {sum}");
        }
    }
}
