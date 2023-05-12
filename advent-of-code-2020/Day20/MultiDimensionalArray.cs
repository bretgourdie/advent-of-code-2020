using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day20
{
    static class MultiDimensionalArray
    {
        public static T[,] FlipHorizontal<T>(T[,] image)
        {
            var xMax = image.GetLength(0);
            var yMax = image.GetLength(1);

            var flip = new T[xMax, yMax];

            for (int x = 0; x < xMax; x++)
            {
                for (int y = 0; y < yMax; y++)
                {
                    AssignToGrid(xMax - 1 - x, y, GetFromGrid(x, y, image), flip);
                }
            }

            return flip;
        }

        public static T GetFromGrid<T>(int x, int y, T[,] grid)
        {
            if (0 <= x && x < grid.GetLength(0)
                       && 0 <= y && y < grid.GetLength(1))
            {
                return (T)grid[y, x];
            }

            else
            {
                return default(T);
            }
        }

        public static void AssignToGrid<T>(int x, int y, T element, T[,] grid)
        {
            grid[y, x] = element;
        }

        public static T[,] ReflectGrid<T>(T[,] baseGrid, Reflection reflection)
        {
            if (reflection == Reflection.Horizontal)
            {
                return FlipHorizontal(baseGrid);
            }

            else if (reflection == Reflection.None)
            {
                return baseGrid;
            }

            else
            {
                throw new NotImplementedException(nameof(reflection));
            }
        }

        public static T[,] RotateGridByDegrees<T>(T[,] baseGrid, Rotation rotation, IList<Rotation> rotations)
        {
            var numberOfRotations = rotations.IndexOf(rotation);
            T[,] newGrid = CopyGrid(baseGrid);

            for (int ii = 0; ii < numberOfRotations; ii++)
            {
                newGrid = rotateGrid90DegreesClockwise(CopyGrid(newGrid));
            }

            return newGrid;
        }

        private static T[,] rotateGrid90DegreesClockwise<T>(T[,] baseGrid)
        {
            var newGrid = new T[baseGrid.GetLength(0), baseGrid.GetLength(1)];
            for (int ii = 0; ii < baseGrid.GetLength(0); ii++)
            {
                for (int jj = 0; jj < baseGrid.GetLength(1); jj++)
                {
                    newGrid[jj, baseGrid.GetLength(0) - ii - 1] = baseGrid[ii, jj];
                }
            }

            return newGrid;
        }

        public static T[,] CopyGrid<T>(T[,] referenceImage)
        {
            var newGrid = new T[referenceImage.GetLength(0), referenceImage.GetLength(1)];
            for (int ii = 0; ii < referenceImage.GetLength(0); ii++)
            {
                for (int jj = 0; jj < referenceImage.GetLength(1); jj++)
                {
                    newGrid[ii, jj] = referenceImage[ii, jj];
                }
            }

            return newGrid;
        }

        public static T[,] CreateGridFromLines<T>(IList<string> lines, Func<char, T> conversion)
        {
            var length = lines.First().Length;

            var grid = new T[length, length];

            for (int ii = 0; ii < lines.Count; ii++)
            {
                for (int jj = 0; jj < lines[ii].Length; jj++)
                {
                    AssignToGrid<T>(jj, ii, conversion(lines[ii][jj]), grid);
                }
            }

            return grid;
        }
    }
}
