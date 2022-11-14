using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day12
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            performWork(inputData, new ShipNavigation());
        }

        private void performWork(
            IList<string> inputData,
            NavigationStrategy navigationStrategy)
        {
            foreach (var line in inputData)
            {
                navigationStrategy.Navigate(line);
            }

            Console.WriteLine(
                $"The Manhattan distance of the ship is {navigationStrategy.GetManhattanDistance()}");
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            performWork(inputData, new WaypointNavigation());
        }
    }
}
