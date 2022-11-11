namespace advent_of_code_2020.Day12
{
    public class Waypoint
    {
        private Transform transform;

        public Waypoint()
        {
            transform = new Transform(10, 1);
        }

        public void PerformAction(WaypointCommand command)
        {
            var newTransform = command.Resolve(this.transform);

            transform = newTransform;
        }
    }
}
