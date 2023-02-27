using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day20
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            new TileSorter().GetCornerProduct(inputData);
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            throw new NotImplementedException();
        }
    }
}
