using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2020.Day20
{
    class Tile
    {
        private static Side[] ALL_SIDES = new[] {Side.North, Side.East, Side.South, Side.West};
        public readonly int Id;

        public readonly IDictionary<Side, string> Sides;

        public Tile(IList<string> tileChunk)
        {
            var header = tileChunk.First();
            var split = header.Split(' ');
            Id = int.Parse(split[1].Replace(":", ""));
            Sides = getSides(tileChunk.Skip(1).ToList());
        }

        private IDictionary<Side, string> getSides(IList<string> lines)
        {
            var sides = new Dictionary<Side, string>();

            var length = lines.Count;

            var xStarts = new[] {0, length - 1, 0, 0};
            var xEnds = new[] {length - 1, length - 1, length - 1, 0};
            var yStarts = new[] {0, 0, length - 1, 0};
            var yEnds = new[] {0, length - 1, length - 1, length - 1};
            var xAdds = new[] {1, 0, 1, 0};
            var yAdds = new[] {0, 1, 0, 1};

            for (int ii = 0; ii < ALL_SIDES.Length; ii++)
            {
                var xStart = xStarts[ii];
                var yStart = yStarts[ii];
                var xEnd = xEnds[ii];
                var yEnd = yEnds[ii];
                var xAdd = xAdds[ii];
                var yAdd = yAdds[ii];

                var side = new StringBuilder();

                for (int x = xStart, y = yStart;
                    Math.Min(xStart, xEnd) <= x && x <= Math.Max(xStart, xEnd)
                                                && Math.Min(yStart, yEnd) <= y && y <= Math.Max(yStart, yEnd);
                    x += xAdd, y += yAdd)
                {
                    side.Append(lines[y][x]);
                }

                sides[ALL_SIDES[ii]] = side.ToString();
            }

            return sides;
        }
    }
}
