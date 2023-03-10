using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2020.Day20
{
    class Tile
    {
        public readonly long Id;
        public readonly IDictionary<Side, string> Sides;
        public readonly IDictionary<Side, string> BackwardsSides;

        private readonly char[,] image;

        public Tile(IList<string> contents)
        {
            Id = parseIdLine(contents.First());

            image = parsePieceLines(contents.Skip(1).ToList());

            Sides = getSides(image);
            BackwardsSides = getBackwardsSides(image);
        }

        public Tile(
            long id,
            char[,] image)
        {
            this.Id = id;
            this.image = image;
        }

        private IDictionary<Side, string> getSides(char[,] image)
        {
            return new Dictionary<Side, string>()
            {
                { Side.Up, top(image) },
                { Side.Down, bottom(image) },
                { Side.Left, left(image) },
                { Side.Right, right(image) }
            };
        }

        private IDictionary<Side, string> getBackwardsSides(char[,] image)
        {
            var flipped = flipHorizontal(image);

            return new Dictionary<Side, string>()
            {
                { Side.Up, top(flipped) },
                { Side.Down, bottom(flipped) },
                { Side.Left, left(flipped) },
                { Side.Right, right(flipped) }
            };
        }

        private string top(char[,] image)
        {
            var sb = new StringBuilder();
            var max = image.GetLength(0);
            for (int ii = 0; ii < max; ii++)
            {
                sb.Append(getFromGrid(ii, 0, image));
            }

            return sb.ToString();
        }

        private string bottom(char[,] image)
        {
            var sb = new StringBuilder();
            var max = image.GetLength(0);
            for (int ii = 0; ii < max; ii++)
            {
                sb.Append(getFromGrid(ii, max - 1, image));
            }

            return sb.ToString();
        }

        private string left(char[,] image)
        {
            var sb = new StringBuilder();
            var max = image.GetLength(1);
            for (int ii = 0; ii < max; ii++)
            {
                sb.Append(getFromGrid(0, ii, image));
            }

            return sb.ToString();
        }

        private string right(char[,] image)
        {
            var sb = new StringBuilder();
            var max = image.GetLength(1);
            for (int ii = 0; ii < max; ii++)
            {
                sb.Append(getFromGrid(max - 1, ii, image));
            }

            return sb.ToString();
        }

        private char[,] flipHorizontal(char[,] image)
        {
            var xMax = image.GetLength(0);
            var yMax = image.GetLength(1);

            var flip = new char[xMax, yMax];

            for (int x = 0; x < xMax; x++)
            {
                for (int y = 0; y < yMax; y++)
                {
                    assignToGrid(xMax - 1 - x, y, getFromGrid(x, y, image), flip);
                }
            }

            return flip;
        }

        private long parseIdLine(string idLine)
        {
            return long.Parse(
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
                    assignToGrid(jj, ii, lines[ii][jj], grid);
                }
            }

            return grid;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int ii = 0; ii < image.GetLength(0); ii++)
            {
                for (int jj = 0; jj < image.GetLength(1); jj++)
                {
                    sb.Append(getFromGrid(jj, ii, image));
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
            grid[y, x] = letter;
        }

        private char getFromGrid(int x, int y, char[,] grid)
        {
            return grid[y, x];
        }
    }
}
