using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day12
{
    class Solution : AdventSolution
    {
        private readonly IList<char> waypointCommands = new List<char>()
        {
            'N',
            'S',
            'E',
            'W',
            'L',
            'R'
        };

        protected override void performWorkForProblem1(IList<string> inputData)
        {
            var ship = new Ship(waypoint: null);

            foreach (var line in inputData)
            {
                var command = new ShipCommand(line);
                ship.PerformAction(command);
            }

            Console.WriteLine($"The Manhattan distance of the ship is {ship.ManhattanDistance()}");
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            var ship = new Ship(new Waypoint());

            foreach (var line in inputData)
            {
                ICommand command = parseCommandForPart2(line);
                ship.PerformAction(command);
            }
        }

        private ICommand parseCommandForPart2(string line)
        {
            if (waypointCommands.Contains(line[0]))
            {
                return new WaypointCommand(line);
            }

            else
            {
                return new ShipCommand(line);
            }
        }
    }
}
