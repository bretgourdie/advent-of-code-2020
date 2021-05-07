using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day01
{
    class Solution : AdventSolution
    {
        private const int target = 2020;

        protected override void performWorkForProblem1(
            IList<string> inputData)
        {
            var expenseEntries = convertInputToInts(inputData);

            for (int ii = 0; ii < expenseEntries.Count; ii++)
            {
                for (int jj = ii + 1; jj < expenseEntries.Count; jj++)
                {
                    int firstEntry = expenseEntries[ii];
                    int secondEntry = expenseEntries[jj];

                    if (firstEntry + secondEntry == target)
                    {
                        int product = firstEntry * secondEntry;
                        Console.WriteLine($"{firstEntry} + {secondEntry} == {target}; "
                        + $"{firstEntry} x {secondEntry} == {product}");
                    }
                }
            }
        }

        protected override void performWorkForProblem2(
            IList<string> inputData)
        {
            var expenseEntries = convertInputToInts(inputData);

            for (int ii = 0; ii < expenseEntries.Count; ii++)
            {
                for (int jj = ii + 1; jj < expenseEntries.Count; jj++)
                {
                    for (int kk = jj + 1; kk < expenseEntries.Count; kk++)
                    {
                        int firstEntry = expenseEntries[ii];
                        int secondEntry = expenseEntries[jj];
                        int thirdEntry = expenseEntries[kk];

                        if (firstEntry + secondEntry + thirdEntry == target)
                        {
                            int product = firstEntry * secondEntry * thirdEntry;

                            Console.WriteLine($"{firstEntry} + {secondEntry} + {thirdEntry} == {target}; "
                            + $"{firstEntry} * {secondEntry} * {thirdEntry} == {product}");
                        }
                    }
                }
            }
        }

        private IList<int> convertInputToInts(
            IEnumerable<string> inputData)
        {
            return inputData.Select(x => int.Parse(x)).ToList();
        }
    }
}
