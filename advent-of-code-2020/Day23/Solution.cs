using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day23
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            Console.WriteLine(
                "The numbers after one are \""
                + performWork(inputData, new UntranslatedInstructions())
                + "\"");
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            Console.WriteLine(
                "The product of the next two cups are \""
                + performWork(inputData, new TranslatedInstructions())
                + "\"");
        }

        private string performWork(
            IList<string> inputData,
            ICupInstructions instructions)
        {
            return new CupCircle(
                    instructions,
                    inputData.Single())
                .MakeMoves();
        }
    }
}
