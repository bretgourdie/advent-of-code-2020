using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day20
{
    class TileSorter
    {
        private List<Rotation> rotations = new List<Rotation>()
        {
            Rotation.NoRotation,
            Rotation.Clockwise90Degrees,
            Rotation.Clockwise180Degrees,
            Rotation.Clockwise270Degrees
        };

        private List<Reflection> reflections = new List<Reflection>()
        {
            Reflection.None,
            Reflection.Horizontal
        };

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
            int dimension = (int)Math.Sqrt(tiles.Count);
            Tile[,] grid = new Tile[dimension, dimension];

            grid = sortTiles(new Queue<Tile>(tiles), grid);

            return grid;
        }

        private Tile[,] sortTiles(
            Queue<Tile> tiles,
            Tile[,] grid)
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
