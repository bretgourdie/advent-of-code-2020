using System;

namespace advent_of_code_2020.Day12
{
    public class WaypointForwardMove : ICommand
    {
        private readonly Transform ship;
        private readonly Transform waypoint;
        private readonly int times;

        public WaypointForwardMove(
            Transform ship,
            Transform waypoint,
            int times)
        {
            this.ship = ship;
            this.waypoint = waypoint;
            this.times = times;
        }

        public Transform Resolve(Transform transform)
        {
            throw new NotImplementedException();
        }
    }
}
