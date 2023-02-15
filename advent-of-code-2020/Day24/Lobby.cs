using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day24
{
    class Lobby
    {
        private IDictionary<Point2D, Tile> floorGrid = new Dictionary<Point2D, Tile>();

        private readonly IDictionary<string, Point2D> instructionToTransformation = new Dictionary<string, Point2D>()
        {
            { "e", new Point2D(1, 0) },
            { "se", new Point2D(0, 1) },
            { "sw", new Point2D(-1, 1) },
            { "w", new Point2D(-1, 0) },
            { "nw", new Point2D(0, -1) },
            { "ne", new Point2D(1, -1) }
        };

        public long FlipTiles(IList<string> instructions)
        {
            flipTiles(instructions);
            return countBlackTiles();
        }

        public long LivingFlipForDays(int days)
        {
            for (int day = 1; day <= days; day++)
            {
                livingFlip();

                if (day <= 10 || day % 10 == 0)
                {
                    Console.WriteLine($"Day {day}: {countBlackTiles()}");
                }
            }

            return countBlackTiles();
        }

        private void livingFlip()
        {
            var flipToBlack = new HashSet<Point2D>();
            var flipToWhite = new HashSet<Point2D>();

            foreach (var pointAndTile in floorGrid)
            {
                var point = pointAndTile.Key;
                foreach (var neighbor in neighbors(point))
                {
                    checkFlip(flipToBlack, flipToWhite, neighbor);
                }

                checkFlip(flipToBlack, flipToWhite, point);
            }

            foreach (var point in flipToBlack)
            {
                addAndFlip(point);
            }

            foreach (var point in flipToWhite)
            {
                addAndFlip(point);
            }
        }

        private IEnumerable<Point2D> neighbors(Point2D point)
        {
            foreach (var instruction in instructionToTransformation.Keys)
            {
                var transform = instructionToTransformation[instruction];
                yield return new Point2D(
                    point.Q + transform.Q,
                    point.R + transform.R
                );
            }
        }

        private void checkFlip(
            HashSet<Point2D> flipToBlack,
            HashSet<Point2D> flipToWhite,
            Point2D point)
        {
            if (pointWillBeFlipped(flipToBlack, point)
                || pointWillBeFlipped(flipToWhite, point))
            {
                return;
            }

            var tile = getTile(point);

            if (shouldFlipToBlack(point, tile))
            {
                addIfNotExists(flipToBlack, point);
            }

            else if (shouldFlipToWhite(point, tile))
            {
                addIfNotExists(flipToBlack, point);
            }
        }

        private Tile getTile(Point2D point)
        {
            if (floorGrid.ContainsKey(point))
            {
                return floorGrid[point];
            }

            return new Tile(point);
        }

        private bool pointWillBeFlipped(
            HashSet<Point2D> flipSet,
            Point2D point) =>
            flipSet.Contains(point);

        private void addIfNotExists(HashSet<Point2D> list, Point2D point)
        {
            if (!list.Contains(point))
            {
                list.Add(point);
            }
        }

        private bool shouldFlipToWhite(Point2D point, Tile tile)
        {
            if (tile.Color == Color.White)
            {
                return false;
            }

            var adjacentBlackTiles = countAdjacentBlackTiles(point);

            return adjacentBlackTiles == 0 || adjacentBlackTiles > 2;
        }

        private bool shouldFlipToBlack(Point2D point, Tile tile) =>
            tile.Color != Color.Black && countAdjacentBlackTiles(point) == 2;

        private int countAdjacentBlackTiles(Point2D point)
        {
            var blackTiles = 0;

            foreach (var instruction in instructionToTransformation.Keys)
            {
                var transformation = instructionToTransformation[instruction];

                var checkingPoint = new Point2D(point.Q + transformation.Q, point.R + transformation.R);

                if (floorGrid.ContainsKey(checkingPoint))
                {
                    if (floorGrid[checkingPoint].Color == Color.Black)
                    {
                        blackTiles += 1;
                    }
                }
            }

            return blackTiles;
        }

        private void flipTiles(IList<string> instructions)
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

                addAndFlip(position);
            }
        }

        private int adjustForTwoLetterInstruction(
            int ii,
            string sanitizedInstruction)
        {
            return ii + sanitizedInstruction.Length - 1;
        }

        private void addAndFlip(Point2D position)
        {
            if (!floorGrid.ContainsKey(position))
            {
                floorGrid.Add(position, new Tile(position));
            }

            floorGrid[position].Flip();
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

        private long countBlackTiles() =>
            floorGrid
                .Count(pair => pair.Value.Color == Color.Black);
    }
}
