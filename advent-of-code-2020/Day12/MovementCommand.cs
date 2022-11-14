using System;

namespace advent_of_code_2020.Day12
{
    public class MovementCommand : ICommand
    {
        private readonly Transform movement;
        private readonly int times = 1;
        private bool movesWaypoint;

        public MovementCommand(
            int amount,
            Direction direction)
        {
            movement = convertDirectionToMovement(amount, direction);
            movesWaypoint = true;
        }

        public MovementCommand(
            Transform waypoint,
            int times)
        {
            movement = waypoint;
            this.times = times;
            movesWaypoint = false;
        }

        private Transform convertDirectionToMovement(
            int amount,
            Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return new Transform(0, amount);
                case Direction.South:
                    return new Transform(0, -1 * amount);
                case Direction.East:
                    return new Transform(amount, 0);
                case Direction.West:
                    return new Transform(-1 * amount, 0);
                default:
                    throw new NotImplementedException();
            }
        }

        public Transform Resolve(Transform t)
        {
            for (int ii = 0; ii < times; ii++)
            {
                t = new Transform(
                    t.X + movement.X,
                    t.Y + movement.Y,
                    t.Direction);
            }

            return t;
        }

        public bool MovesShip()
        {
            return !movesWaypoint;
        }
    }
}
