﻿using System;
using System.Collections.Generic;
using System.Linq;
using static advent_of_code_2020.Day20.MultiDimensionalArray;

namespace advent_of_code_2020.Day20
{
    class TileSorter
    {
        private List<Rotation> rotations = new List<Rotation>()
        {
            Rotation.None,
            Rotation.Clockwise90Degrees,
            Rotation.Clockwise180Degrees,
            Rotation.Clockwise270Degrees
        };

        private List<Reflection> reflections = new List<Reflection>()
        {
            Reflection.None,
            Reflection.Horizontal
        };

        private IDictionary<long, IList<Tile>> precomputedPermutationsByTile;

        public long GetCornerProduct(IList<string> input)
        {
            var tiles = parseTiles(input);

            var grid = sortTiles(tiles);

            return
                grid[0, 0].Id
                * grid[0, grid.GetLength(1) - 1].Id
                * grid[grid.GetLength(1) - 1, 0].Id
                * grid[grid.GetLength(0) - 1, grid.GetLength(1) - 1].Id;
        }

        private Tile[,] sortTiles(IList<Tile> tiles)
        {
            int dimension = (int)Math.Sqrt(tiles.Count);
            Tile[,] grid = new Tile[dimension, dimension];

            precomputedPermutationsByTile = precomputeTiles(tiles);

            grid = sortTiles(new List<Tile>(tiles), grid);

            return grid;
        }

        private IDictionary<long, IList<Tile>> precomputeTiles(IList<Tile> baseTiles)
        {
            var dict = new Dictionary<long, IList<Tile>>();
            foreach (var baseTile in baseTiles)
            {
                var list = new List<Tile>();

                foreach (var reflection in reflections)
                {
                    foreach (var rotation in rotations)
                    {
                        list.Add(baseTile.FromPermutation(rotation, reflection, rotations));
                    }
                }

                dict[baseTile.Id] = list;
            }

            return dict;
        }

        private Tile[,] sortTiles(
            IList<Tile> tiles,
            Tile[,] grid)
        {
            if (!tiles.Any())
            {
                return grid;
            }

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (GetFromGrid(x, y, grid) == null && isAppropriatePlacement(x, y, grid))
                    {
                        foreach (var tile in tiles)
                        {
                            foreach (var permutation in precomputedPermutationsByTile[tile.Id])
                            {
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

            return null;
        }
        private bool isAppropriatePlacement(int x, int y, Tile[,] grid)
        {
            var tiles = from Tile tile in grid
                where tile != null
                select tile;

            var tileIsAlreadyThere = GetFromGrid(x, y, grid) != null;

            var atLeastOneTile = tiles.Any();

            var coordinatesAtStart = x == 0 && y == 0;

            return !tileIsAlreadyThere && (atLeastOneTile || coordinatesAtStart);
        }

        private bool fits(Tile tile, int x, int y, Tile[,] grid)
        {
            var leftTile = GetFromGrid(x - 1, y, grid);
            if (leftTile != null)
            {
                if (tile.Left != leftTile.Right)
                {
                    return false;
                }
            }

            var rightTile = GetFromGrid(x + 1, y, grid);
            if (rightTile != null)
            {
                if (tile.Right != rightTile.Left)
                {
                    return false;
                }
            }

            var downTile = GetFromGrid(x, y + 1, grid);
            if (downTile != null)
            {
                if (tile.Bottom != downTile.Top)
                {
                    return false;
                }
            }

            var upTile = GetFromGrid(x, y - 1, grid);
            if (upTile != null)
            {
                if (tile.Top != upTile.Bottom)
                {
                    return false;
                }
            }

            if (leftTile == null
                && rightTile == null
                && upTile == null
                && downTile == null
                && AnyAssigned(grid))
            {
                return false;
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

            return tiles;
        }
    }
}
