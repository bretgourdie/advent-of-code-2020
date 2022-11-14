using System;

namespace advent_of_code_2020.Day12
{
    public abstract class NavigationStrategy
    {

        private const char North = 'N';
        private const char South = 'S';
        private const char East = 'E';
        private const char West = 'W';

        private const char Left = 'L';
        private const char Right = 'R';

        private const char Forward = 'F';

        public void Navigate(string line)
        {
            navigate(parseCommand(line));
        }

        protected abstract void navigate(ICommand command);

        public abstract int GetManhattanDistance();

        protected int getManhattanDistance(Transform transform) =>
            Math.Abs(transform.X) + Math.Abs(transform.Y);

        private ICommand parseCommand(string line)
        {
            char letter = line[0];
            int number = int.Parse(line.Substring(1));

            switch (letter)
            {
                case North:
                    return new MovementCommand(number, Direction.North);
                case South:
                    return new MovementCommand(number, Direction.South);
                case East:
                    return new MovementCommand(number, Direction.East);
                case West:
                    return new MovementCommand(number, Direction.West);

                case Left:
                    return parseRotation(number, RotationDirection.CounterClockwise);
                case Right:
                    return parseRotation(number, RotationDirection.Clockwise);

                case Forward:
                    return parseForwardMove(number);

                default:
                    throw new NotImplementedException();
            }
        }

        protected abstract ICommand parseRotation(int degrees, RotationDirection rotationDirection);
        protected abstract MovementCommand parseForwardMove(int amount);
    }
}
