using System;

namespace advent_of_code_2020.Day12
{
    public class ShipForwardMove : ICommand
    {
        private readonly int amount;

        public ShipForwardMove(int amount)
        {
            this.amount = amount;
        }

        public Transform Resolve(Transform t)
        {
            switch (t.Direction)
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
