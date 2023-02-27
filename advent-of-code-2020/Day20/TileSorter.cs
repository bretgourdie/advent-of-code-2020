using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day20
{
    class TileSorter
    {
        public long GetCornerProduct(IList<string> input)
        {
            var tiles = parseTiles(input);

            var grid = sortTiles(tiles);

            return
                grid[0, 0].Id
                * grid[0, grid.GetLength(1)].Id
                * grid[grid.GetLength(1), 0].Id
                * grid[grid.GetLength(0), grid.GetLength(1)].Id;
        }

        private Tile[,] sortTiles(IList<Tile> tiles)
        {
            throw new NotImplementedException();
        }

        private IList<Tile> parseTiles(IList<string> input)
        {
            var tiles = new List<Tile>();

            var tileContent = new List<string>();

            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    tiles.Add(new Tile(tileContent));
                    tileContent = new List<string>();
                }

                else
                {
                    tileContent.Add(line);
                }
            }

            tiles.Add(new Tile(tileContent));

            return tiles;
        }
    }
}
