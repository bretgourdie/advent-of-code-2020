using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day12
{
    public class Command
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

        private IList<Direction> clockwiseDirections = new List<Direction>()
        {
            Direction.North,
            Direction.East,
            Direction.South,
            Direction.West
        };

        public Command(string input)
        {
            commandLetter = input[0];
            commandNumber = Int32.Parse(input.Substring(1));
        }

        public Resolution Resolve(int x, int y, Direction direction)
        {
            switch (commandLetter)
            {
                case North:
                    y += commandNumber;
                    break;
                case South:
                    y -= commandNumber;
                    break;
                case East:
                    x += commandNumber;
                    break;
                case West:
                    x -= commandNumber;
                    break;
                case Left:
                    direction = resolveDirection(commandNumber, -1, direction);
                    break;
                case Right:
                    direction = resolveDirection(commandNumber, 1, direction);
                    break;
                case Forward:
                    x = resolveForwardX(x, commandNumber, direction);
                    y = resolveForwardY(y, commandNumber, direction);
                    break;
            }

            return new Resolution(x, y, direction);
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
