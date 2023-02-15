using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace advent_of_code_2020.Day24
{
    class Lobby
    {
        private readonly IDictionary<string, Point2D> instructionToTransformation = new Dictionary<string, Point2D>()
        {
            { "ne", new Point2D(1, -1) },
            { "e", new Point2D(1, 0) },
            { "se", new Point2D(0, 1) },
            { "sw", new Point2D(-1, 1) },
            { "w", new Point2D(-1, 0) },
            { "nw", new Point2D(0, -1) }
        };

        public long FlipTiles(IList<string> instructions)
        {
            return performFlipInstructions(instructions).Count();
        }

        private ISet<Point2D> performFlipInstructions(
            IList<string> instructions)
        {
            ISet<Point2D> blackTiles = new HashSet<Point2D>();
            flipTiles(instructions, blackTiles);
            return blackTiles;
        }

        public long LivingFlipForDays(
            IList<string> instructions,
            int days)
        {
            var blackTiles = performFlipInstructions(instructions);

            for (int day = 1; day <= days; day++)
            {
                livingFlip(blackTiles);

                if (day <= 10 || day % 10 == 0)
                {
                    Console.WriteLine($"Day {day}: {blackTiles.Count()}");
                }
            }

            return blackTiles.Count();
        }

        private void livingFlip(ISet<Point2D> blackTiles)
        {
            var flipToBlack = new HashSet<Point2D>();
            var flipToWhite = new HashSet<Point2D>();

            foreach (var point in blackTiles)
            {
                var selfAndNeighbors = getSelfAndNeighbors(point);

                foreach (var checkPoint in selfAndNeighbors)
                {
                    checkFlip(
                        checkPoint,
                        blackTiles,
                        flipToBlack,
                        flipToWhite);
                }
            }

            foreach (var point in flipToBlack)
            {
                flipTileToBlack(point, blackTiles);
            }

            foreach (var point in flipToWhite)
            {
                flipTileToWhite(point, blackTiles);
            }
        }

        private IEnumerable<Point2D> getSelfAndNeighbors(Point2D point)
        {
            yield return point;

            foreach (var neighbor in getNeighbors(point))
            {
                yield return neighbor;
            }
        }

        private IEnumerable<Point2D> getNeighbors(Point2D point)
        {
            foreach (var instruction in instructionToTransformation.Keys)
            {
                var transform = instructionToTransformation[instruction];

                var neighborPoint = new Point2D(
                    point.Q + transform.Q,
                    point.R + transform.R);

                yield return neighborPoint;
            }
        }

        private void checkFlip(
            Point2D point,
            ISet<Point2D> blackTiles,
            ISet<Point2D> flipToBlack,
            ISet<Point2D> flipToWhite)
        {
            if (flipToBlack.Contains(point)
                || flipToWhite.Contains(point))
            {
                return;
            }

            var isBlackTile = blackTiles.Contains(point);

            var neighboringBlackTiles =
                countAdjacentBlackTiles(point, blackTiles);

            if (shouldFlipToBlack(isBlackTile, neighboringBlackTiles))
            {
                addIfNotExists(flipToBlack, point);
            }

            else if (shouldFlipToWhite(isBlackTile, neighboringBlackTiles))
            {
                addIfNotExists(flipToWhite, point);
            }
        }

        private void addIfNotExists(ISet<Point2D> list, Point2D point)
        {
            if (!list.Contains(point))
            {
                list.Add(point);
            }
        }

        private bool shouldFlipToWhite(
            bool isBlackTile,
            int neighboringBlackTiles)
        {
            var correctNumberOfBlackTiles = neighboringBlackTiles == 0 || neighboringBlackTiles > 2;

            return isBlackTile && correctNumberOfBlackTiles;
        }

        private bool shouldFlipToBlack(
            bool isBlackTile,
            int neighboringBlackTiles)
        {
            var correctNumberOfBlackTiles = neighboringBlackTiles == 2;

            return !isBlackTile && correctNumberOfBlackTiles;
        }

        private int countAdjacentBlackTiles(
            Point2D point,
            ISet<Point2D> blackTiles)
        {
            var blackTileCount = 0;

            var neighbors = getNeighbors(point).ToList();

            var neighborsThatAreBlackTiles =
                neighbors
                    .Where(neighbor => blackTiles.Contains(neighbor))
                    .ToList();

            return neighborsThatAreBlackTiles.Count();
        }

        private void flipTiles(
            IList<string> instructions,
            ISet<Point2D> blackTiles)
        {
            Point2D position;

            foreach (var line in instructions)
            {
                position = new Point2D(0, 0);

                for (int ii = 0; ii < line.Length; ii++)
                {
                    var substringLength = Math.Min(line.Length - ii, 2);
                    var rawInstruction = line.Substring(ii, substringLength);

                    var sanitizedInstruction = sanitizeInstruction(rawInstruction);

                    ii = adjustForTwoLetterInstruction(ii, sanitizedInstruction);

                    position = translateInstruction(position, sanitizedInstruction);
                }

                flip(position, blackTiles);
            }
        }

        private void flip(
            Point2D point,
            ISet<Point2D> blackTiles)
        {
            if (blackTiles.Contains(point))
            {
                flipTileToWhite(point, blackTiles);
            }

            else
            {
                flipTileToBlack(point, blackTiles);
            }
        }

        private int adjustForTwoLetterInstruction(
            int ii,
            string sanitizedInstruction)
        {
            return ii + sanitizedInstruction.Length - 1;
        }

        private void flipTileToBlack(
            Point2D point,
            ISet<Point2D> blackTiles)
        {
            blackTiles.Add(point);
        }

        private void flipTileToWhite(
            Point2D point,
            ISet<Point2D> blackTiles)
        {
            blackTiles.Remove(point);
        }

        private string sanitizeInstruction(string twoLetterInstruction)
        {
            if (twoLetterInstruction.StartsWith("s")
                || twoLetterInstruction.StartsWith("n"))
            {
                return twoLetterInstruction;
            }

            else
            {
                return twoLetterInstruction[0].ToString();
            }
        }

        private Point2D translateInstruction(
            Point2D position,
            string instruction)
        {
            var transformation = instructionToTransformation[instruction];

            return new Point2D(
                position.Q + transformation.Q,
                position.R + transformation.R
            );
        }
    }
}
