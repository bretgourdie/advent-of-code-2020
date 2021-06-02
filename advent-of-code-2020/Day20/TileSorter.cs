using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day20
{
    class TileSorter
    {
        private readonly IList<Side> sidesClockwise;
        private readonly IList<Rotation> rotationsClockwise;
        private readonly IList<Reflection> reflections;

        private IDictionary<Point2D, OrientedTile> sortedTiles;

        public TileSorter()
        {
            sidesClockwise = new List<Side>() {Side.Left, Side.Up, Side.Right, Side.Down};
            rotationsClockwise = new List<Rotation>()
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
            sortedTiles = new Dictionary<Point2D, OrientedTile>();

            throw new NotImplementedException();
        }

        private bool tilesMatch(
            OrientedTile a,
            OrientedTile b)
        {
            throw new NotImplementedException();
        }

        private long getCornerMultiplications(IList<IList<OrientedTile>> sortedTiles)
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
