using System;
using System.Collections.Generic;
using System.Linq;
using static advent_of_code_2020.Day20.MultiDimensionalArray;

namespace advent_of_code_2020.Day20
{
    class TileSorter
    {
        private readonly IList<string> seaMonsterTemplate = new List<string>()
        {
            {"                  # "},
            {"#    ##    ##    ###"},
            {" #  #  #  #  #  #   "}
        };

        private readonly IDictionary<Side, Side> oppositeSide = new Dictionary<Side, Side>()
        {
            {Side.Left, Side.Right},
            {Side.Down, Side.Up},
            {Side.Right, Side.Left},
            {Side.Up, Side.Down}
        };

        private readonly IList<Side> sides = new List<Side>()
        {
            Side.Up,
            Side.Left,
            Side.Down,
            Side.Right
        };

        private readonly IList<Rotation> rotations = new List<Rotation>()
        {
            Rotation.None,
            Rotation.Clockwise90Degrees,
            Rotation.Clockwise180Degrees,
            Rotation.Clockwise270Degrees
        };

        private readonly IList<Reflection> reflections = new List<Reflection>()
        {
            Reflection.None,
            Reflection.Horizontal
        };

        public long GetCornerProduct(IList<string> input)
        {
            var tiles = parseTiles(input);

            var cornerTiles = getCornerTiles(tiles);

            return cornerTiles.Aggregate((long) 1, (number, tile) => number * tile.Id);
        }

        private IList<Tile> getCornerTiles(IList<Tile> tiles)
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

        public long GetWaterRoughness(IList<string> input)
        {
            var tiles = parseTiles(input);

            var cornerTiles = getCornerTiles(tiles);

            var grid = sortTiles(tiles, cornerTiles);

            var gaplessImage = removeGaps(grid);

            var highlightedSeamonsterGrid = spotSeaMonsters(gaplessImage);

            var roughness = determineRoughness(highlightedSeamonsterGrid);

            return roughness;
        }

        private long determineRoughness(char[,] spottedGrid)
        {
            int roughness = 0;

            for (int y = 0; y < spottedGrid.GetLength(0); y++)
            {
                for (int x = 0; x < spottedGrid.GetLength(1); x++)
                {
                    if (GetFromGrid(x, y, spottedGrid) == '#')
                    {
                        roughness += 1;
                    }
                }
            }

            return roughness;
        }

        private char[,] spotSeaMonsters(char[,] gaplessImage)
        {
            var seamonsterGrid = CreateGridFromLines(seaMonsterTemplate, x => x);

            var seamonsters = new List<Point2D>();

            char[,] spottedGrid = null;

            var daGrid = new Tile(-1, gaplessImage, Rotation.None, Reflection.None);

            for (int possibleReflection = 0; possibleReflection < reflections.Count && seamonsters.Count == 0; possibleReflection++)
            {
                for (int possibleRotations = 0; possibleRotations < 4 && seamonsters.Count == 0; possibleRotations++)
                {
                    var permute = daGrid.FromPermutation(
                        rotations[possibleRotations],
                        reflections[possibleReflection],
                        rotations);

                    var currentGaplessImage = permute.Image;

                    for (int y = 0; y < currentGaplessImage.GetLength(0) - seamonsterGrid.GetLength(0) - 1; y++)
                    {
                        for (int x = 0; x < currentGaplessImage.GetLength(1) - seamonsterGrid.GetLength(1) - 1; x++)
                        {
                            if (isSeamonster(x, y, currentGaplessImage, seamonsterGrid))
                            {
                                seamonsters.Add(new Point2D(x, y));
                            }
                        }
                    }

                    if (seamonsters.Count > 0)
                    {
                        spottedGrid = CopyGrid(currentGaplessImage);
                    }
                }
            }

            if (seamonsters.Count <= 0)
            {
                throw new ApplicationException("Couldn't find any sea monsters");
            }

            foreach (var seamonsterCoordinate in seamonsters)
            {
                markGridWithSpotted(
                    seamonsterCoordinate.X,
                    seamonsterCoordinate.Y,
                    spottedGrid,
                    seamonsterGrid);
            }

            return spottedGrid;
        }

        private void markGridWithSpotted(int x, int y, char[,] grid, char[,] seamonsterGrid)
        {
            for (int ySeamonsterInPictureCheck = y; ySeamonsterInPictureCheck < seamonsterGrid.GetLength(0) + y; ySeamonsterInPictureCheck++)
            {
                for (int xSeamonsterInPictureCheck = x; xSeamonsterInPictureCheck < seamonsterGrid.GetLength(1) + x; xSeamonsterInPictureCheck++)
                {
                    int ySeamonsterReference = ySeamonsterInPictureCheck - y;
                    int xSeamonsterReference = xSeamonsterInPictureCheck - x;
                    var reference = GetFromGrid(xSeamonsterReference, ySeamonsterReference, seamonsterGrid);

                    if (reference == '#')
                    {
                        AssignToGrid(xSeamonsterInPictureCheck, ySeamonsterInPictureCheck, 'O', grid);
                    }
                }
            }
        }

        private bool isSeamonster(int x, int y, char[,] gaplessImage, char[,] seamonsterGrid)
        {
            for (int ySeamonsterInPictureCheck = y; ySeamonsterInPictureCheck < seamonsterGrid.GetLength(0) + y; ySeamonsterInPictureCheck++)
            {
                for (int xSeamonsterInPictureCheck = x; xSeamonsterInPictureCheck < seamonsterGrid.GetLength(1) + x; xSeamonsterInPictureCheck++)
                {
                    int ySeamonsterReference = ySeamonsterInPictureCheck - y;
                    int xSeamonsterReference = xSeamonsterInPictureCheck - x;
                    var reference = GetFromGrid(xSeamonsterReference, ySeamonsterReference, seamonsterGrid);

                    if (reference == '#')
                    {
                        var content = GetFromGrid(xSeamonsterInPictureCheck, ySeamonsterInPictureCheck, gaplessImage);

                        if (content != '#')
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private char[,] removeGaps(Tile[,] sortedImage)
        {
            var dimension = sortedImage.GetLength(0) * (sortedImage[0,0].Image.GetLength(0) - 2);
            char[,] gaplessImage = new char[dimension, dimension];

            for (int x = 0; x < sortedImage.GetLength(0); x++)
            {
                for (int y = 0; y < sortedImage.GetLength(1); y++)
                {
                    writeBorderless(x, y, GetFromGrid(x, y, sortedImage), gaplessImage);
                }
            }

            return gaplessImage;
        }

        private void writeBorderless(
            int x,
            int y,
            Tile tile,
            char[,] gaplessImage)
        {
            var xGaplessReference = x * (tile.Image.GetLength(0) - 2);
            var yGaplessReference = y * (tile.Image.GetLength(1) - 2);

            for (int xTileReference = 1; xTileReference < tile.Image.GetLength(0) - 1; xTileReference++)
            {
                for (int yTileReference = 1; yTileReference < tile.Image.GetLength(1) - 1; yTileReference++)
                {
                    var content = GetFromGrid(xTileReference, yTileReference, tile.Image);

                    var xGaplessCoordinate = xGaplessReference + xTileReference - 1;
                    var yGaplessCoordinate = yGaplessReference + yTileReference - 1;

                    AssignToGrid(xGaplessCoordinate, yGaplessCoordinate, content, gaplessImage);
                }
            }
        }

        private Tile[,] sortTiles(IList<Tile> allTiles, IList<Tile> cornerPieces)
        {
            int dimension = (int)Math.Sqrt(allTiles.Count);
            Tile[,] grid = new Tile[dimension, dimension];

            var nonCornerPieces = separateCornerPieces(allTiles, cornerPieces);

            grid = sortTiles(
                precomputeTiles(nonCornerPieces),
                precomputeTiles(cornerPieces),
                grid);

            return grid;
        }

        private IList<Tile> separateCornerPieces(IList<Tile> allTiles, IList<Tile> cornerPieces)
        {
            return allTiles
                .Where(tile => !cornerPieces.Contains(tile))
                .ToList();
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
            IList<Tile> nonCornerPieces,
            IList<Tile> cornerPieces,
            Tile[,] grid)
        {
            if (!nonCornerPieces.Any() && !cornerPieces.Any())
            {
                return grid;
            }

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (GetFromGrid(x, y, grid) == null && isAppropriatePlacement(x, y, grid))
                    {
                        IList<Tile> tilesToUse = getTilesToUse(nonCornerPieces, cornerPieces, x, y, grid.GetLength(0));

                        foreach (var tile in tilesToUse)
                        {
                            if (fits(tile, x, y, grid))
                            {
                                AssignToGrid(x, y, tile, grid);

                                var newGrid = sortTiles(
                                    isAppraisingCorner(x, y, grid.GetLength(0))
                                        ? nonCornerPieces
                                        : nonCornerPieces.Where(other => other.Id != tile.Id).ToList(),
                                    isAppraisingCorner(x, y, grid.GetLength(0))
                                        ? cornerPieces.Where(other => other.Id != tile.Id).ToList()
                                        : cornerPieces,
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

        private bool isAppraisingCorner(int x, int y, int gridLength)
        {
            return
                (x == 0 || x == gridLength - 1) && (y == 0 || y == gridLength - 1);
        }

        private IList<Tile> getTilesToUse(IList<Tile> nonCornerPieces, IList<Tile> cornerPieces, int x, int y, int gridLength)
        {
            if (isAppraisingCorner(x, y, gridLength))
            {
                return cornerPieces;
            }

            else
            {
                return nonCornerPieces;
            }
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
