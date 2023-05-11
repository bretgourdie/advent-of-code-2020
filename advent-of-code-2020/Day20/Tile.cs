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
        private readonly char[,] image;

        public Tile(IList<string> contents)
        {
            Id = parseIdLine(contents.First());

            image = CreateGridFromLines(contents.Skip(1).ToList(), x => x);
        }

        public Tile(
            long id,
            char[,] image)
        {
            this.Id = id;
            this.image = image;
        }

        public static Tile FromPermutation(
            Tile tile,
            Rotation rotation,
            Reflection reflection,
            IList<Rotation> rotations)
        {
            var baseImage = tile.image;

            var rotatedImage = RotateGridByDegrees(baseImage, rotation, rotations);

            var rotatedAndReflectedImage = ReflectGrid(rotatedImage, reflection);

            return new Tile(tile.Id, rotatedAndReflectedImage);
        }

        public string Top()
        {
            var sb = new StringBuilder();
            var max = image.GetLength(0);
            for (int ii = 0; ii < max; ii++)
            {
                sb.Append(GetFromGrid(ii, 0, image));
            }

            return sb.ToString();
        }

        public string Bottom()
        {
            var sb = new StringBuilder();
            var max = image.GetLength(0);
            for (int ii = 0; ii < max; ii++)
            {
                sb.Append(GetFromGrid(ii, max - 1, image));
            }

            return sb.ToString();
        }

        public string Left()
        {
            var sb = new StringBuilder();
            var max = image.GetLength(1);
            for (int ii = 0; ii < max; ii++)
            {
                sb.Append(GetFromGrid(0, ii, image));
            }

            return sb.ToString();
        }

        public string Right()
        {
            var sb = new StringBuilder();
            var max = image.GetLength(1);
            for (int ii = 0; ii < max; ii++)
            {
                sb.Append(GetFromGrid(max - 1, ii, image));
            }

            return sb.ToString();
        }



        private long parseIdLine(string idLine)
        {
            return long.Parse(
                idLine.Replace("Tile ", String.Empty)
                    .Replace(":", String.Empty)
            );
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int ii = 0; ii < image.GetLength(0); ii++)
            {
                for (int jj = 0; jj < image.GetLength(1); jj++)
                {
                    sb.Append(GetFromGrid(jj, ii, image));
                }

                sb.AppendLine();
            }

            return
                $"Tile {Id}:"
                + Environment.NewLine
                + sb;
        }
    }
}
