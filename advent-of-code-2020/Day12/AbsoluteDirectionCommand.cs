using System;

namespace advent_of_code_2020.Day12
{
    class AbsoluteDirectionCommand : ICommand
    {
        private readonly Direction direction;
        private int amount;

        public AbsoluteDirectionCommand(
            int amount,
            Direction direction)
        {
            this.amount = amount;
            this.direction = direction;
        }

        public Transform Resolve(Transform t)
        {
            switch (direction)
            {
                case Direction.North:
                    return new Transform(t.X, t.Y + amount, t.Direction);
                case Direction.South:
                    return new Transform(t.X, t.Y - amount, t.Direction);
                case Direction.East:
                    return new Transform(t.X + amount, t.Y, t.Direction);
                case Direction.West:
                    return new Transform(t.X - amount, t.Y, t.Direction);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
