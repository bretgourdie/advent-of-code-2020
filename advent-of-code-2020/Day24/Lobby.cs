using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day24
{
    class Lobby
    {
        private IDictionary<Point2D, Tile> floorGrid = new Dictionary<Point2D, Tile>();

        public long FlipTiles(IList<string> instructions)
        {
            flipTiles(instructions);
            return countBlackTiles();
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
            int q = position.Q;
            int r = position.R;

            switch (instruction)
            {
                case "e":
                    q += 1;
                    break;
                case "se":
                    r += 1;
                    break;
                case "sw":
                    q -= 1;
                    r += 1;
                    break;
                case "w":
                    q -= 1;
                    break;
                case "nw":
                    r -= 1;
                    break;
                case "ne":
                    q += 1;
                    r -= 1;
                    break;
            }

            return new Point2D(q, r);
        }

        private long countBlackTiles() =>
            floorGrid
                .Count(pair => pair.Value.Color == Color.Black);
    }
}
