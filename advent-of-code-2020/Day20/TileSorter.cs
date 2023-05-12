using System;
using System.Collections.Generic;
using System.Linq;
using static advent_of_code_2020.Day20.MultiDimensionalArray;

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

            grid = sortTiles(new List<Tile>(tiles), grid);

            return grid;
        }

        private Tile[,] sortTiles(
            IList<Tile> tiles,
            Tile[,] grid)
        {
            foreach (var tile in tiles)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    for (int y = 0; y < grid.GetLength(1); y++)
                    {
                        if (GetFromGrid(x, y, grid) == null)
                        {
                            foreach (var rotation in rotations)
                            {
                                foreach (var reflection in reflections)
                                {
                                    var permutation = tile.FromPermutation(rotation, reflection, rotations);

                                    if (fits(permutation, x, y, grid))
                                    {
                                        AssignToGrid(x, y, permutation, grid);

                                        var newGrid = sortTiles(
                                            tiles.Where(other => other.Id != permutation.Id).ToList(),
                                            grid);

                                        if (newGrid != null)
                                        {
                                            return grid;
                                        }

                                        else
                                        {
                                            AssignToGrid(x, y, null, grid);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        private bool fits(Tile tile, int x, int y, Tile[,] grid)
        {
            var leftTile = GetFromGrid(x - 1, y, grid);
            if (leftTile != null)
            {
                if (tile.Left() != leftTile.Right())
                {
                    return false;
                }
            }

            var rightTile = GetFromGrid(x + 1, y, grid);
            if (rightTile != null)
            {
                if (tile.Right() != rightTile.Left())
                {
                    return false;
                }
            }

            var downTile = GetFromGrid(x, y + 1, grid);
            if (downTile != null)
            {
                if (tile.Bottom() != downTile.Top())
                {
                    return false;
                }
            }

            var upTile = GetFromGrid(x, y - 1, grid);
            if (upTile != null)
            {
                if (tile.Top() != upTile.Bottom())
                {
                    return false;
                }
            }

            return true;
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
