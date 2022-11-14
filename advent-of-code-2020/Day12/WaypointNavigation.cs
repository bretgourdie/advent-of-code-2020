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
            if (command.MovesShip())
            {
                ship = command.Resolve(ship);
            }

            else
            {
                waypoint = command.Resolve(waypoint);
            }
        }

        protected override ICommand parseRotation(int degrees, RotationDirection rotationDirection)
        {
            return new WaypointRotateCommand(degrees, rotationDirection);
        }

        protected override MovementCommand parseForwardMove(int times)
        {
            return new MovementCommand(waypoint, times);
        }

        public override int GetManhattanDistance()
        {
            return getManhattanDistance(ship);
        }
    }
}
