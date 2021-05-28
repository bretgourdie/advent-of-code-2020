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
                var topSide = tile.GetEdge(new Up(length), Reflection.NoReflection);
                var leftSide = tile.GetEdge(new Left(length), Reflection.NoReflection);
                var rightSide = tile.GetEdge(new Right(length), Reflection.NoReflection);
                var bottomSide = tile.GetEdge(new Down(length), Reflection.NoReflection);

                var topReflected = tile.GetEdge(new Up(length), Reflection.Flip);
                var leftReflected = tile.GetEdge(new Left(length), Reflection.Flip);
                var rightReflected = tile.GetEdge(new Right(length), Reflection.Flip);
                var bottomReflected = tile.GetEdge(new Down(length), Reflection.Flip);
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
