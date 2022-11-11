using System;

namespace advent_of_code_2020.Day12
{
    public class Ship
    {
        private readonly Waypoint waypoint;

        private ShipTransform shipTransform;

        public Ship(Waypoint waypoint)
        {
            shipTransform = new ShipTransform(0, 0, Direction.East);
            this.waypoint = waypoint;
        }

        public void PerformAction(ICommand command)
        {
            if (command is ShipCommand)
            {
                PerformAction((ShipCommand)command);
            }

            else if (command is WaypointCommand)
            {
                waypoint.PerformAction((WaypointCommand)command);
            }
        }

        public void PerformAction(ShipCommand command)
        {
            var newTransform = command.Resolve(shipTransform);

            shipTransform = newTransform as ShipTransform;
        }

        public int ManhattanDistance() => Math.Abs(shipTransform.X) + Math.Abs(shipTransform.Y);
    }
}
