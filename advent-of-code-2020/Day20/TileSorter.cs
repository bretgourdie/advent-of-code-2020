using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static advent_of_code_2020.Day20.MultiDimensionalArray;

namespace advent_of_code_2020.Day20
{
    class TileSorter
    {
        private IDictionary<Side, Side> oppositeSide = new Dictionary<Side, Side>()
        {
            {Side.Left, Side.Right},
            {Side.Down, Side.Up},
            {Side.Right, Side.Left},
            {Side.Up, Side.Down}
        };

        private IList<Side> sides = new List<Side>()
        {
            Side.Up,
            Side.Left,
            Side.Down,
            Side.Right
        };

        private IList<Rotation> rotations = new List<Rotation>()
        {
            Rotation.None,
            Rotation.Clockwise90Degrees,
            Rotation.Clockwise180Degrees,
            Rotation.Clockwise270Degrees
        };

        private IList<Reflection> reflections = new List<Reflection>()
        {
            Reflection.None,
            Reflection.Horizontal
        };

        public long GetCornerProduct(IList<string> input)
        {
            var tiles = parseTiles(input);

            var cornerTiles = determineCornerTiles(tiles);

            return cornerTiles.Aggregate((long) 1, (number, tile) => number * tile.Id);
        }

        private IList<Tile> determineCornerTiles(IList<Tile> tiles)
        {
            var tilesByMatches = new Dictionary<Tile, int>();

            foreach (var tile in tiles)
            {
                if (!tilesByMatches.ContainsKey(tile))
                {
                    tilesByMatches[tile] = 0;
                }

                foreach (var other in tiles)
                {
                    if (tile.Id != other.Id)
                    {
                        if (oneEdgeMatches(tile, other))
                        {
                            tilesByMatches[tile] += 1;
                        }
                    }
                }
            }

            var cornerPieces = tilesByMatches
                .Where(tileAndMatch => tileAndMatch.Value == 2)
                .Select(tileAndMatch => tileAndMatch.Key)
                .ToList();

            return cornerPieces;
        }

        private bool oneEdgeMatches(Tile tile, Tile other)
        {
            int matches = 0;

            foreach (var edge in tile.Sides.Values)
            {
                var reversedEdge = new string(edge.Reverse().ToArray());

                foreach (var otherEdge in other.Sides.Values)
                {
                    if (edge == otherEdge || reversedEdge == otherEdge)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public long GetDragons(IList<string> input)
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

            var precomputedTiles = precomputeTiles(tiles);

            var precomputedTileEdgeHashes = precomputeTileEdgeHashes(precomputedTiles);

            grid = sortTiles(precomputeTiles(tiles), precomputedTileEdgeHashes, grid);

            return grid;
        }

        private IDictionary<int, IList<Tile>> precomputeTileEdgeHashes(IList<Tile> tilePermuations)
        {
            var dict = new Dictionary<int, IList<Tile>>();

            foreach (var tile in tilePermuations)
            {
                foreach (var sideAndEdge in tile.Sides)
                {
                    var hash = sideAndEdge.Value.GetHashCode();
                    if (!dict.ContainsKey(hash))
                    {
                        dict.Add(hash, new List<Tile>());
                    }

                    var tilesForHash = dict[hash];

                    tilesForHash.Add(tile);
                }
            }

            return dict;
        }

        private IList<Tile> precomputeTiles(IList<Tile> baseTiles)
        {
            var list = new List<Tile>();
            foreach (var baseTile in baseTiles)
            {
                foreach (var reflection in reflections)
                {
                    foreach (var rotation in rotations)
                    {
                        list.Add(baseTile.FromPermutation(rotation, reflection, rotations));
                    }
                }
            }

            return list;
        }

        private Tile[,] sortTiles(
            IList<Tile> tiles,
            IDictionary<int, IList<Tile>> hashToTiles,
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
                        IList<Tile> tilesToUse;

                        if (AnyAssigned(grid))
                        {
                            var neededSides = getNeededSides(x, y, grid);

                            var tileSet = new HashSet<Tile>();

                            foreach (var neededSide in neededSides)
                            {
                                if (hashToTiles.ContainsKey(neededSide.GetHashCode()))
                                {
                                    if (tileSet.Any())
                                    {
                                        tileSet.IntersectWith(hashToTiles[neededSide.GetHashCode()]);
                                    }

                                    else
                                    {
                                        tileSet.UnionWith(hashToTiles[neededSide.GetHashCode()]);
                                    }
                                }
                            }

                            tilesToUse = tileSet.ToList();
                        }

                        else
                        {
                            tilesToUse = tiles;
                        }

                        foreach (var tile in tilesToUse)
                        {
                            if (fits(tile, x, y, grid))
                            {
                                AssignToGrid(x, y, tile, grid);

                                var newGrid = sortTiles(
                                    tiles.Where(other => other.Id != tile.Id).ToList(),
                                    hashToTiles,
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

            return null;
        }

        private IList<string> getNeededSides(int x, int y, Tile[,] grid)
        {
            var sideList = new List<string>();

            var neighborSideAndTiles = getNeighborTiles(x, y, grid);

            foreach (var neighborSideAndTile in neighborSideAndTiles)
            {
                var neighborReferenceSide = this.oppositeSide[neighborSideAndTile.Key];
                sideList.Add(neighborSideAndTile.Value.Sides[neighborReferenceSide]);
            }

            return sideList;
        }

        private IList<KeyValuePair<Side, Tile>> getNeighborTiles(int x, int y, Tile[,] grid)
        {
            var tiles = new List<KeyValuePair<Side, Tile>>();

            foreach (var side in sides)
            {
                var neighborTile = getNeighborTile(x, y, grid, side);

                if (neighborTile != null)
                {
                    tiles.Add(new KeyValuePair<Side, Tile>(side, neighborTile));
                }
            }

            return tiles;
        }

        private Tile getNeighborTile(int x, int y, Tile[,] grid, Side side)
        {
            var newX = getXFromSide(x, side);
            var newY = getYFromSide(y, side);

            if (newX < 0 || newY < 0 || newX >= grid.GetLength(0) || newY >= grid.GetLength(1))
            {
                return null;
            }

            return GetFromGrid(newX, newY, grid);
        }

        private int getXFromSide(int xOrigin, Side side)
        {
            if (side == Side.Left)
            {
                return xOrigin - 1;
            }

            else if (side == Side.Right)
            {
                return xOrigin + 1;
            }

            return xOrigin;
        }

        private int getYFromSide(int yOrigin, Side side)
        {
            if (side == Side.Up)
            {
                return yOrigin - 1;
            }

            else if (side == Side.Down)
            {
                return yOrigin + 1;
            }

            return yOrigin;
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
            var neighbors = getNeighborTiles(x, y, grid);

            foreach (var neighbor in neighbors)
            {
                var neighboringSide = neighbor.Key;
                var neighboringTile = neighbor.Value;

                var neighborEdge = neighboringTile.Sides[oppositeSide[neighboringSide]];
                var thisEdge = tile.Sides[neighboringSide];

                if (neighborEdge != thisEdge)
                {
                    return false;
                }
            }

            if (!neighbors.Any()
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
