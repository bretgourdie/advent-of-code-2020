using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day20
{
    class TileSorter
    {
        public long Sort(IList<Tile> tiles)
        {
            int length = tiles.First().Length;

            foreach (var tile in tiles)
            {
                var topSide = tile.GetEdge(Side.Up, Reflection.NoReflection);
                var leftSide = tile.GetEdge(Side.Left, Reflection.NoReflection);
                var rightSide = tile.GetEdge(Side.Right, Reflection.NoReflection);
                var bottomSide = tile.GetEdge(Side.Down, Reflection.NoReflection);

                var topReflected = tile.GetEdge(Side.Up, Reflection.Flip);
                var leftReflected = tile.GetEdge(Side.Left, Reflection.Flip);
                var rightReflected = tile.GetEdge(Side.Right, Reflection.Flip);
                var bottomReflected = tile.GetEdge(Side.Down, Reflection.Flip);
            }
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
