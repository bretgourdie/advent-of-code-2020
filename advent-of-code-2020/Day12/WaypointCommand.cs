using System;

namespace advent_of_code_2020.Day12
{
    public class WaypointCommand : ICommand
    {
        private readonly char commandLetter;
        private readonly int commandNumber;

        private const char North = 'N';
        private const char South = 'S';
        private const char East = 'E';
        private const char West = 'W';
        private const char Left = 'L';
        private const char Right = 'R';

        public WaypointCommand(string input)
        {
            commandLetter = input[0];
            commandNumber = int.Parse(input.Substring(1));
        }

        public Transform Resolve(Transform t)
        {
            switch (commandLetter)
            {
                case North:
                    return new Transform(t.X, t.Y + commandNumber);
                case South:
                    return new Transform(t.X, t.Y - commandNumber);
                case East:
                    return new Transform(t.X + commandNumber, t.Y);
                case West:
                    return new Transform(t.X - commandNumber, t.Y);
                case Left:
                    return resolveRotationCounterClockwise(t, commandNumber);
                case Right:
                    return resolveRotationClockwise(t, commandNumber);
                default:
                    throw new NotImplementedException();
            }
        }

        private Transform resolveRotationClockwise(
            Transform transform,
            int degrees)
        {
            int rotations = degrees % 90;

            int x = transform.X;
            int y = transform.Y;

            for (int ii = 0; ii <= rotations; ii++)
            {
                int temp = x;
                x = y;
                y = temp * -1;
            }

            return new Transform(x, y);
        }

        private Transform resolveRotationCounterClockwise(
            Transform transform,
            int degrees)
        {
            int rotations = degrees % 90;

            int x = transform.X;
            int y = transform.Y;

            for (int ii = 0; ii <= rotations; ii++)
            {
                int temp = y;
                y = x;
                x = temp * -1;
            }

            return new Transform(x, y);
        }
    }
}
