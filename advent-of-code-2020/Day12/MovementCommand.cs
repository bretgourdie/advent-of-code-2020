using System;

namespace advent_of_code_2020.Day12
{
    public class MovementCommand : ICommand
    {
        private readonly Transform movement;

        public MovementCommand(
            int amount,
            Direction direction)
        {
            movement = convertDirectionToMovement(amount, direction);
        }

        public MovementCommand(
            Transform waypoint)
        {
            movement = waypoint;
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
            return new Transform(
                t.X + movement.X,
                t.Y + movement.Y,
                t.Direction);
        }
    }
}
