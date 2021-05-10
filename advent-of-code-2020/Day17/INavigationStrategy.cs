using System.Collections.Generic;

namespace advent_of_code_2020.Day17
{
    interface INavigationStrategy<Point> where Point : struct
    {
        Point LoadCube(int x, int y);
        IEnumerable<Point> GetPossibleCubes(IEnumerable<Point> activeCubes);
        IEnumerable<Point> GetNeighbors(Point cube);
        string ListDimensions();
    }
}
