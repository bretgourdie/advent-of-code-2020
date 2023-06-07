using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static advent_of_code_2020.Day20.MultiDimensionalArray;

namespace advent_of_code_2020.Day20
{
    class Tile
    {
        public readonly long Id;
        public readonly char[,] Image;

        public readonly Rotation Rotation;
        public readonly Reflection Reflection;

        public readonly string Top;
        public readonly string Bottom;
        public readonly string Left;
        public readonly string Right;

        private readonly IDictionary<Side, string> sides;

        public IEnumerable<string> Edges => sides.Values;

        private static readonly IDictionary<Side, Side> oppositeSide = new Dictionary<Side, Side>()
        {
            {Side.Left, Side.Right},
            {Side.Down, Side.Up},
            {Side.Right, Side.Left},
            {Side.Up, Side.Down}
        };

        public Tile(IList<string> contents) :
            this(
                parseIdLine(contents.First()),
                CreateGridFromLines(contents.Skip(1).ToList(), x => x),
                Rotation.None,
                Reflection.None
            ) {}

        public Tile(
            long id,
            char[,] image,
            Rotation rotation,
            Reflection reflection)
        {
            this.Id = id;
            this.Image = image;
            this.Rotation = rotation;
            this.Reflection = reflection;

            this.Top = top(image);
            this.Bottom = bottom(image);
            this.Left = left(image);
            this.Right = right(image);

            sides = new Dictionary<Side, string>()
            {
                { Side.Left, this.Left },
                { Side.Right, this.Right },
                { Side.Down, this.Bottom },
                { Side.Up, this.Top }
            };
        }

        public Tile FromPermutation(
            Rotation rotation,
            Reflection reflection,
            IList<Rotation> rotations)
        {
            var baseImage = CopyGrid(this.Image);

            var rotatedImage = RotateGridByDegrees(baseImage, rotation, rotations);

            var rotatedAndReflectedImage = ReflectGrid(rotatedImage, reflection);

            return new Tile(
                this.Id,
                rotatedAndReflectedImage,
                rotation,
                reflection);
        }

        public bool SideMatches(
            Tile other,
            Side otherSide)
        {
            return sides[oppositeSide[otherSide]] == other.sides[otherSide];
        }

        public bool AnySideMatches(
            Tile other)
        {
            foreach (var edge in Edges)
            {
                var reversedEdge = new string(edge.Reverse().ToArray());

                foreach (var otherEdge in other.Edges)
                {
                    if (edge == otherEdge || reversedEdge == otherEdge)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private string top(char[,] grid)
        {
            var sb = new StringBuilder();
            var max = grid.GetLength(0);
            for (int ii = 0; ii < max; ii++)
            {
                sb.Append(GetFromGrid(ii, 0, grid));
            }

            return sb.ToString();
        }

        private string bottom(char[,] grid)
        {
            var sb = new StringBuilder();
            var max = grid.GetLength(0);
            for (int ii = 0; ii < max; ii++)
            {
                sb.Append(GetFromGrid(ii, max - 1, grid));
            }

            return sb.ToString();
        }

        private string left(char[,] grid)
        {
            var sb = new StringBuilder();
            var max = grid.GetLength(1);
            for (int ii = 0; ii < max; ii++)
            {
                sb.Append(GetFromGrid(0, ii, grid));
            }

            return sb.ToString();
        }

        private string right(char[,] grid)
        {
            var sb = new StringBuilder();
            var max = grid.GetLength(1);
            for (int ii = 0; ii < max; ii++)
            {
                sb.Append(GetFromGrid(max - 1, ii, grid));
            }

            return sb.ToString();
        }

        private static long parseIdLine(string idLine)
        {
            return long.Parse(
                idLine.Replace("Tile ", String.Empty)
                    .Replace(":", String.Empty)
            );
        }

        public override string ToString()
        {
            return $"Tile {Id} Rotation.{Rotation} Reflection.{Reflection}";
        }
    }
}
