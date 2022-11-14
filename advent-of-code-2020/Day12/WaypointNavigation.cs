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
            if (command is MovementCommand || command is WaypointRotateCommand)
            {
                waypoint = command.Resolve(waypoint);
            }

            else if (command is ForwardMoveCommand)
            {
                ship = command.Resolve(ship);
            }
        }

        protected override ICommand parseRotation(int degrees, RotationDirection rotationDirection)
        {
            return new WaypointRotateCommand(degrees, rotationDirection);
        }

        protected override ForwardMoveCommand parseForwardMove(int times)
        {
            return new ForwardMoveCommand(waypoint, times);
        }

        public override int GetManhattanDistance()
        {
            return getManhattanDistance(ship);
        }
    }
}
