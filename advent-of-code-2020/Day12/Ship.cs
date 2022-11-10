using System;

namespace advent_of_code_2020.Day12
{
    public class Ship
    {
        private int x;
        private int y;
        private Direction direction;

        public Ship()
        {
            direction = Direction.East;
        }

        public void PerformAction(Command command)
        {
            var resolution = command.Resolve(x, y, direction);

            x = resolution.X;
            y = resolution.Y;
            direction = resolution.Direction;
        }

        public int ManhattanDistance() => Math.Abs(x) + Math.Abs(y);
    }
}
