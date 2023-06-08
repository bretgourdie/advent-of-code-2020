using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day20
{
    static class MultiDimensionalArray
    {
        private static readonly int XDimension = 1;
        private static readonly int YDimension = 0;

        private static readonly IDictionary<Side, EdgeIteration> sideToEdgeIterations =
            new Dictionary<Side, EdgeIteration>()
            {
                { Side.Left, new EdgeIteration(YDimension, EdgeIteration.MinEdge, EdgeIteration.Iterate) },
                { Side.Right, new EdgeIteration(YDimension, EdgeIteration.MaxEdge, EdgeIteration.Iterate) },
                { Side.Up, new EdgeIteration(XDimension, EdgeIteration.Iterate, EdgeIteration.MinEdge) },
                { Side.Down, new EdgeIteration(XDimension, EdgeIteration.Iterate, EdgeIteration.MaxEdge) }
            };

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

        public static bool AnyAssigned<T>(T[,] grid)
        {
            for (int ii = 0; ii < grid.GetLength(0); ii++)
            {
                for (int jj = 0; jj < grid.GetLength(1); jj++)
                {
                    if (grid[ii, jj] != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static T GetFromGrid<T>(int x, int y, T[,] grid)
        {
            if (0 <= x && x < grid.GetLength(1)
                       && 0 <= y && y < grid.GetLength(0))
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
            var newGrid = new T[baseGrid.GetLength(YDimension), baseGrid.GetLength(XDimension)];
            for (int x = 0; x < baseGrid.GetLength(XDimension); x++)
            {
                for (int y = 0; y < baseGrid.GetLength(YDimension); y++)
                {
                    newGrid[y, baseGrid.GetLength(YDimension) - x - 1] = baseGrid[x, y];
                }
            }

            return newGrid;
        }

        public static long CountConditions<T>(T[,] grid, Func<T, bool> condition)
        {
            long instances = 0;

            for (int y = 0; y < grid.GetLength(YDimension); y++)
            {
                for (int x = 0; x < grid.GetLength(XDimension); x++)
                {
                    if (condition.Invoke(GetFromGrid(x, y, grid)))
                    {
                        instances += 1;
                    }
                }
            }

            return instances;
        }

        public static T[,] CopyGrid<T>(T[,] referenceImage)
        {
            return (T[,])referenceImage.Clone();
        }

        public static T[,] CreateGridFromLines<T>(IList<string> lines, Func<char, T> conversion)
        {
            var length = lines.First().Length;

            var width = lines.Count;

            var grid = new T[width, length];

            for (int ii = 0; ii < lines.Count; ii++)
            {
                for (int jj = 0; jj < lines[ii].Length; jj++)
                {
                    AssignToGrid<T>(jj, ii, conversion(lines[ii][jj]), grid);
                }
            }

            return grid;
        }

        public static IEnumerable<T> GetEdge<T>(T[,] grid, Side side)
        {
            var edgeIteration = sideToEdgeIterations[side];
            var iteratingMax = grid.GetLength(edgeIteration.IteratingDimension);

            for (int ii = 0; ii < iteratingMax; ii++)
            {
                yield return
                    GetFromGrid(
                        edgeIteration.XIterator(ii, iteratingMax),
                        edgeIteration.YIterator(ii, iteratingMax),
                        grid);
            }
        }

        private class EdgeIteration
        {
            public readonly int IteratingDimension;
            public readonly Func<int, int, int> XIterator;
            public readonly Func<int, int, int> YIterator;

            public EdgeIteration(
                int iteratingDimension,
                Func<int, int, int> xFunction,
                Func<int, int, int> yFunction)
            {
                this.IteratingDimension = iteratingDimension;
                this.XIterator = xFunction;
                this.YIterator = yFunction;
            }

            public static int Iterate(int ii, int max) => ii;

            public static int MinEdge(int ii, int max) => 0;

            public static int MaxEdge(int ii, int max) => max - 1;
        }
    }
}
