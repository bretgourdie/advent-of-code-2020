using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day20
{
    class TileSorter
    {
        private readonly IList<Side> sidesClockwise;
        private readonly IList<Rotation> rotationIndex;
        private readonly IList<Reflection> reflections;

        private IDictionary<Point2D, SortedTile> sortedTiles;

        public TileSorter()
        {
            sidesClockwise = new List<Side>() {Side.Left, Side.Up, Side.Right, Side.Down};
            rotationIndex = new List<Rotation>()
            {
                Rotation.NoRotation,
                Rotation.Clockwise90,
                Rotation.Clockwise180,
                Rotation.Clockwise270
            };
            reflections = new List<Reflection>() {Reflection.NoReflection, Reflection.Flip};
        }

        public long Sort(IList<Tile> tiles)
        {
            sortedTiles = new Dictionary<Point2D, SortedTile>();

            throw new NotImplementedException();
        }

        private long getCornerMultiplications(IList<IList<Tile>> sortedTiles)
        {
            int N = sortedTiles.Count - 1;

            return
                sortedTiles[0][0].Id
                * sortedTiles[0][N].Id
                * sortedTiles[N][0].Id
                * sortedTiles[N][N].Id;
        }
    }
}
