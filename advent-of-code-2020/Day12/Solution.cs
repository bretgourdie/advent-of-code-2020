using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day12
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            var ship = new Ship();

            foreach (var line in inputData)
            {
                var command = new Command(line);
                ship.PerformAction(command);
            }

            Console.WriteLine($"The mManhattan distance of the ship is {ship.ManhattanDistance()}");
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            throw new NotImplementedException();
        }
    }
}
