using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2020.Day23
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            Console.WriteLine(
                "The numbers after one are \""
                + new CupCircle(inputData.Single()).MakeMoves()
                + "\"");
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            throw new NotImplementedException();
        }
    }
}
