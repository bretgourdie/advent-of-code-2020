using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day20
{
    class TileSorter
    {
        public long GetCornerProduct(IList<Tile> tiles)
        {
            return cornerProduct(
                getCorners(tiles));
        }

        private IEnumerable<Tile> getCorners(IList<Tile> tiles)
        {
            var matchesByTile = determineMatchesByTile(tiles);

            var cornerPieces = determineCornerPieces(matchesByTile);

            return cornerPieces;
        }

        private IDictionary<Tile, int> determineMatchesByTile(
                IList<Tile> tiles)
        {
            var matchesByTile = new Dictionary<Tile, int>();

            for (int checkingTileIndex = 0; checkingTileIndex < tiles.Count; checkingTileIndex++)
            {
                var checkingTile = tiles[checkingTileIndex];

                matchesByTile[checkingTile] = 0;

                for (int testingTileIndex = 0; testingTileIndex < tiles.Count; testingTileIndex++)
                {
                    if (checkingTileIndex == testingTileIndex)
                    {
                        continue;
                    }

                    var testingTile = tiles[testingTileIndex];

                    if (oneSideMatches(checkingTile, testingTile, out bool tileAFlipped))
                    {
                        matchesByTile[checkingTile] += 1;
                    }
                }
            }

            return matchesByTile;
        }

        private bool oneSideMatches(
            Tile a,
            Tile b,
            out bool tileAFlipped)
        {
            foreach (var aSide in a.Sides)
            {
                foreach (var bSide in b.Sides)
                {
                    if (aSide.Equals(bSide))
                    {
                        tileAFlipped = false;
                        return true;
                    }

                    string reversedASide = new string(aSide.Reverse().ToArray());

                    if (reversedASide.Equals(bSide))
                    {
                        tileAFlipped = true;
                        return true;
                    }
                }
            }

            tileAFlipped = false;
            return false;
        }

        private IEnumerable<Tile> determineCornerPieces(
            IDictionary<Tile, int> matchesByTile)
        {
            return matchesByTile
                .Where(x => x.Value == 2)
                .Select(x => x.Key);
        }

        private long cornerProduct(IEnumerable<Tile> tiles)
        {
            long product = 1;

            foreach (var tile in tiles)
            {
                product *= tile.Id;
            }

            return product;
        }

    }
}
