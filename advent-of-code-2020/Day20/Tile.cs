using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2020.Day20
{
    class Tile
    {
        public readonly int Id;
        public readonly IDictionary<Side, string> Sides;

        public readonly IDictionary<Side, int> Matches;
        public readonly IDictionary<Side, int> BackwardsMatches;

        private readonly char[,] image;

        public Tile(IList<string> contents)
        {
            Id = parseIdLine(contents.First());

            image = parsePieceLines(contents.Skip(1).ToList());

            Sides = getSides(image);

            Matches = initializeMatches();
            BackwardsMatches = initializeMatches();
        }

        private IDictionary<Side, int> initializeMatches()
        {
            return new Dictionary<Side, int>()
            {
                {Side.Down, 0},
                {Side.Left, 0},
                {Side.Right, 0},
                {Side.Up, 0}
            };
        }

        public void CheckMatches(Tile other)
        {
            foreach (var otherEdge in other.Sides.Values)
            {
                foreach (var sideAndContent in Sides)
                {
                    if (sideAndContent.Value == otherEdge)
                    {
                        Matches[sideAndContent.Key] += 1;
                    }

                    if (sideAndContent.Value.Reverse() == otherEdge)
                    {
                        BackwardsMatches[sideAndContent.Key] += 1;
                    }
                }
            }
        }

        private int parseIdLine(string idLine)
        {
            return int.Parse(
                idLine.Replace("Tile ", String.Empty)
                    .Replace(":", String.Empty)
            );
        }

        private char[,] parsePieceLines(IList<string> lines)
        {
            var length = lines.First().Length;

            var grid = new char[length, length];

            for (int ii = 0; ii < lines.Count; ii++)
            {
                for (int jj = 0; jj < lines[ii].Length; jj++)
                {
                    assignToGrid(ii, jj, lines[ii][jj], grid);
                }
            }

            return grid;
        }

        private IDictionary<Side, string> getSides(char[,] piece)
        {
            var sideDictionary = new Dictionary<Side, string>();

            var edge = new StringBuilder();
            var oppositeEdge = new StringBuilder();
            for (int ii = 0; ii < piece.GetLength(0); ii++)
            {
                edge.Append(getFromGrid(ii, 0, piece));
                oppositeEdge.Append(getFromGrid(ii, piece.GetLength(1) - 1, piece));
            }

            sideDictionary[Side.Left] = edge.ToString();
            sideDictionary[Side.Right] = oppositeEdge.ToString();

            edge = new StringBuilder();
            oppositeEdge = new StringBuilder();
            for (int jj = 0; jj < piece.GetLength(1); jj++)
            {
                edge.Append(getFromGrid(0, jj, piece));
                oppositeEdge.Append(getFromGrid(piece.GetLength(0) - 1, jj, piece));
            }

            sideDictionary[Side.Up] = edge.ToString();
            sideDictionary[Side.Down] = oppositeEdge.ToString();

            return sideDictionary;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int ii = 0; ii < image.GetLength(0); ii++)
            {
                for (int jj = 0; jj < image.GetLength(1); jj++)
                {
                    sb.Append(getFromGrid(ii, jj, image));
                }

                sb.AppendLine();
            }

            return
                $"Tile {Id}:"
                + Environment.NewLine
                + sb;
        }


        private void assignToGrid(int x, int y, char letter, char[,] grid)
        {
            grid[x, y] = letter;
        }

        private char getFromGrid(int x, int y, char[,] grid)
        {
            return grid[x, y];
        }
    }
}
