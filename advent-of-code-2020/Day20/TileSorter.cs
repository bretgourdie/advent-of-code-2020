using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day20
{
    class TileSorter
    {
        public long Sort(IList<Tile> tiles)
        {
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
