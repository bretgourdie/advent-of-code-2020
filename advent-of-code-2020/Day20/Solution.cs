using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day20
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            var cornerProduct = new TileSorter().GetCornerProduct(inputData);

            Console.WriteLine($"The corner product is {cornerProduct}");
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            var waterRoughness = new TileSorter().GetWaterRoughness(inputData);

            Console.WriteLine($"The water roughness is {waterRoughness}");
        }
    }
}
