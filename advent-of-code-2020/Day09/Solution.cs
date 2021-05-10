using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day09
{
    class Solution : AdventSolution
    {
        private const int examplePreambleLength = 5;
        private const int problemPreambleLength = 25;

        protected override void performWorkForExample1(IList<string> inputData)
        {
            findFirstNumberThatDoesntSum(
                inputData,
                examplePreambleLength);
        }

        protected override void performWorkForProblem1(IList<string> inputData)
        {
            findFirstNumberThatDoesntSum(
                inputData,
                problemPreambleLength);
        }

        private void findFirstNumberThatDoesntSum(
            IList<string> inputData,
            int preambleLength)
        {
            long weaknessTarget = new Decrypter(inputData, preambleLength).FindNumberWithoutSum();
            Console.WriteLine($"The weakness target is {weaknessTarget}");
        }

        protected override void performWorkForExample2(IList<string> inputData)
        {
            findContiguousRangeForWeakness(
                inputData,
                examplePreambleLength);
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            findContiguousRangeForWeakness(
                inputData,
                problemPreambleLength);
        }

        private void findContiguousRangeForWeakness(
            IList<string> inputData,
            int preambleLength)
        {
            long weakness = new Decrypter(inputData, preambleLength).FindWeakness(inputData);
            Console.WriteLine($"The weakness is {weakness}");
        }
    }
}
