using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2020.Day12
{
    public class WaypointNavigation : NavigationStrategy
    {
        private Transform ship;
        private Transform waypoint;

        public WaypointNavigation()
        {
            ship = new Transform(0, 0, Direction.East);
            waypoint = new Transform(1, 10);
        }

        protected override void navigate(ICommand command)
        {
            if (command is AbsoluteDirectionCommand || command is WaypointRotateCommand)
            {
                waypoint = command.Resolve(waypoint);
            }

            else
            {
                ship = command.Resolve(ship);
            }
        }

        protected override ICommand parseRotation(int degrees, RotationDirection rotationDirection)
        {
            return new WaypointRotateCommand(degrees, rotationDirection);
        }

        protected override ICommand parseForwardMove(int amount)
        {
            return new WaypointForwardMove(ship, waypoint, amount);
        }

        public override int GetManhattanDistance()
        {
            return getManhattanDistance(ship);
        }
    }
}
