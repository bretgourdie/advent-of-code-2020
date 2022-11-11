using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day12
{
    public class ShipCommand : ICommand
    {
        private readonly char commandLetter;
        private readonly int commandNumber;

        private const char North = 'N';
        private const char South = 'S';
        private const char East = 'E';
        private const char West = 'W';
        private const char Left = 'L';
        private const char Right = 'R';
        private const char Forward = 'F';

        private readonly IList<Direction> clockwiseDirections = new List<Direction>()
        {
            Direction.North,
            Direction.East,
            Direction.South,
            Direction.West
        };

        public ShipCommand(string input)
        {
            commandLetter = input[0];
            commandNumber = int.Parse(input.Substring(1));
        }

        public Transform Resolve(Transform transform)
        {
            var t = transform as ShipTransform;
            switch (commandLetter)
            {
                case North:
                    return new ShipTransform(t.X, t.Y + commandNumber, t.Direction);
                case South:
                    return new ShipTransform(t.X, t.Y - commandNumber, t.Direction);
                case East:
                    return new ShipTransform(t.X + commandNumber, t.Y, t.Direction);
                case West:
                    return new ShipTransform(t.X - commandNumber, t.Y, t.Direction);
                case Left:
                    return new ShipTransform(t.X, t.Y, resolveDirection(commandNumber, -1, t.Direction));
                case Right:
                    return new ShipTransform(t.X, t.Y, resolveDirection(commandNumber, 1, t.Direction));
                case Forward:
                    return new ShipTransform(
                        resolveForwardX(t.X, commandNumber, t.Direction),
                        resolveForwardY(t.Y, commandNumber, t.Direction),
                        t.Direction);
                default:
                    throw new NotImplementedException();
            }
        }

        private int resolveForwardX(int x, int forwardSpaces, Direction direction)
        {
            if (direction == Direction.West)
            {
                x -= forwardSpaces;
            }

            else if (direction == Direction.East)
            {
                x += forwardSpaces;
            }

            return x;
        }

        private int resolveForwardY(int y, int forwardSpaces, Direction direction)
        {
            if (direction == Direction.South)
            {
                y -= forwardSpaces;
            }

            else if (direction == Direction.North)
            {
                y += forwardSpaces;
            }

            return y;
        }

        private Direction resolveDirection(int degrees, int rotatingDirection, Direction currentDirection)
        {
            var directionIndex = clockwiseDirections.IndexOf(currentDirection);

            var indexSpots = degrees / 90;

            var spotsToRotate = indexSpots * rotatingDirection;
            var newUnboundIndex = directionIndex + spotsToRotate;
            var newBoundIndex = mod(newUnboundIndex, clockwiseDirections.Count);

            return clockwiseDirections[newBoundIndex];
        }

        private int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}
