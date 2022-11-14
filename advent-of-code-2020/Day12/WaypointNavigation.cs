namespace advent_of_code_2020.Day12
{
    public class WaypointNavigation : NavigationStrategy
    {
        private Transform ship;
        private Transform waypoint;

        public WaypointNavigation(
            Transform ship,
            Transform waypoint)
        {
            this.ship = ship;
            this.waypoint = waypoint;
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

        protected override ICommand parseForwardMove(int times)
        {
            return new WaypointForwardMove(waypoint, times);
        }

        public override int GetManhattanDistance()
        {
            return getManhattanDistance(ship);
        }
    }
}
