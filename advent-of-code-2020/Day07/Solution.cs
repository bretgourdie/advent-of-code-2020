using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day07
{
    class Solution : AdventSolution<string>
    {
        private const string targetBag = "shiny gold";

        protected override void performWorkForProblem1(IList<string> inputData)
        {
            loadAndTraverseTree(
                inputData,
                new FindBagsHoldingTarget());
        }

        private void loadAndTraverseTree(
            IList<string> inputData,
            ITraversal traversalStrategy)
        {
            var bagTree = new BagTree(inputData);

            const string targetBag = "shiny gold";

            int result = traversalStrategy.Find(bagTree, targetBag);

            Console.WriteLine(traversalStrategy.GetResultMessage(result, targetBag));
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            loadAndTraverseTree(
                inputData,
                new FindIndividualBagsInTarget());
        }
    }
}
