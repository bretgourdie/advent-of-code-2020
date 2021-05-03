using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day01
{
    class Solution : AdventSolution<int>
    {
        private const int target = 2020;

        protected override void performWorkForProblem1(
            IList<int> expenseEntries)
        {
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
            IList<int> expenseEntries)
        {
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
    }
}
