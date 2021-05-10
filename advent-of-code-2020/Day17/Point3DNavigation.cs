using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day17
{
    class Point3DNavigation : INavigationStrategy<Point3D>
    {
        private Func<Point3D, int> X = cube => cube.X;
        private Func<Point3D, int> Y = cube => cube.Y;
        private Func<Point3D, int> Z = cube => cube.Z;

        public Point3D LoadCube(int x, int y)
        {
            return new Point3D(x, y, 0);
        }

        public IEnumerable<Point3D> GetPossibleCubes(
            IEnumerable<Point3D> activeCubes)
        {
            var d = new Dimension<Point3D>(activeCubes);

            for (int x = d.MinBound(X); x <= d.MaxBound(X); x++)
            {
                for (int y = d.MinBound(Y); y <= d.MaxBound(Y); y++)
                {
                    for (int z = d.MinBound(Z); z <= d.MaxBound(Z); z++)
                    {
                        yield return new Point3D(x, y, z);
                    }
                }
            }
        }

        public IEnumerable<Point3D> GetNeighbors(
            Point3D cube)
        {
            for (int x = cube.X - 1; x <= cube.X + 1; x++)
            {
                for (int y = cube.Y - 1; y <= cube.Y + 1; y++)
                {
                    for (int z = cube.Z - 1; z <= cube.Z + 1; z++)
                    {
                        var testingCube = new Point3D(x, y, z);

                        if (!cube.Equals(testingCube))
                        {
                            yield return testingCube;
                        }
                    }
                }
            }
        }

        public string ListDimensions() => "x,y,z";
    }
}
