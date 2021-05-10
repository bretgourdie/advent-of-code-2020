using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day17
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            cycleSixTimes(inputData, new Point3DNavigation());
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            cycleSixTimes(inputData, new Point4DNavigation());
        }

        private void cycleSixTimes<Point>(
            IList<string> inputData,
            INavigationStrategy<Point> navigationStrategy) where Point : struct
        {
            var plane = new Plane<Point>(
                inputData,
                navigationStrategy);

            for (int ii = 0; ii < 6; ii++)
            {
                plane.Cycle();
            }

            Console.WriteLine($"There are {plane.CountActive()} active cubes with dimensions {plane.GetDimensions()}");
        }
    }
}
