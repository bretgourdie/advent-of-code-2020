using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day17
{
    class Dimension<Point> where Point : struct
    {
        private readonly IEnumerable<Point> activeCubes;

        public Dimension(IEnumerable<Point> activeCubes)
        {
            this.activeCubes = activeCubes;
        }

        public int Min(
            Func<Point, int> property)
        {
            return activeCubes.Min(property);
        }

        public int MinBound(
            Func<Point, int> property)
        {
            return Min(property) - 1;
        }

        public int Max(
            Func<Point, int> property)
        {
            return activeCubes.Max(property);
        }

        public int MaxBound(
            Func<Point, int> property)
        {
            return Max(property) + 1;
        }
    }
}
