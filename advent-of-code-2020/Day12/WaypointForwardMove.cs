namespace advent_of_code_2020.Day12
{
    public class WaypointForwardMove : ICommand
    {
        private readonly Transform waypoint;
        private readonly int times;

        public WaypointForwardMove(
            Transform waypoint,
            int times)
        {
            this.waypoint = waypoint;
            this.times = times;
        }

        public Transform Resolve(Transform ship)
        {
            for (int ii = 0; ii < times; ii++)
            {
                ship = new Transform(
                    ship.X + waypoint.X,
                    ship.Y + waypoint.Y,
                    ship.Direction);
            }

            return ship;
        }
    }
}
