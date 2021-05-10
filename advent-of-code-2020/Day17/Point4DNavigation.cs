using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day17
{
    class Point4DNavigation : INavigationStrategy<Point4D>
    {
        private Func<Point4D, int> X = cube => cube.X;
        private Func<Point4D, int> Y = cube => cube.Y;
        private Func<Point4D, int> Z = cube => cube.Z;
        private Func<Point4D, int> W = cube => cube.W;

        public Point4D LoadCube(int x, int y)
        {
            return new Point4D(x, y, 0, 0);
        }

        public IEnumerable<Point4D> GetPossibleCubes(
            IEnumerable<Point4D> activeCubes)
        {
            var d = new Dimension<Point4D>(activeCubes);

            for (int x = d.MinBound(X); x <= d.MaxBound(X); x++)
            {
                for (int y = d.MinBound(Y); y <= d.MaxBound(Y); y++)
                {
                    for (int z = d.MinBound(Z); z <= d.MaxBound(Z); z++)
                    {
                        for (int w = d.MinBound(W); w <= d.MaxBound(W); w++)
                        {
                            yield return new Point4D(x, y, z, w);
                        }
                    }
                }
            }
        }

        public IEnumerable<Point4D> GetNeighbors(
            Point4D cube)
        {
            for (int x = cube.X - 1; x <= cube.X + 1; x++)
            {
                for (int y = cube.Y - 1; y <= cube.Y + 1; y++)
                {
                    for (int z = cube.Z - 1; z <= cube.Z + 1; z++)
                    {
                        for (int w = cube.W - 1; w <= cube.W + 1; w++)
                        {
                            var testingCube = new Point4D(x, y, z, w);

                            if (!cube.Equals(testingCube))
                            {
                                yield return testingCube;
                            }
                        }
                    }
                }
            }
        }

        public string ListDimensions() => "x,y,z,w";
    }
}
