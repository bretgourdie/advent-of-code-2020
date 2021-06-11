using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace advent_of_code_2020.Day20
{
    class TileSorter
    {
        private readonly IDictionary<Side, Point2D> sideOffset;

        private IDictionary<Tile, IList<OrientedTile>> tilePermutationsByTile;

        public TileSorter()
        {
            sideOffset = loadSideOffsets();
        }

        private IDictionary<Tile, IList<OrientedTile>> loadTilesToPermutationDictionary(
            IEnumerable<Tile> tiles)
        {
            var tileDict = new Dictionary<Tile, IList<OrientedTile>>();

            foreach (var tile in tiles)
            {
                tileDict[tile] = TileConversion.GetTilePermutations(tile);
            }

            return tileDict;
        }

        public long Sort(IList<Tile> tiles)
        {
            return getCornerMultiplications(sort(tiles));
        }

        private OrientedTile[,] sort(IList<Tile> tiles)
        {
            var grid = getEmptyTileGrid(tiles);

            var tileCombinations = getTileCombinations(tiles);

            foreach (var tileCombination in tileCombinations)
            {
                populateGrid(grid, tileCombination);
            }
        }

        private void populateGrid(
            OrientedTile[,] grid,
            IList<Tile> tiles)
        {
            var xLength = grid.GetLength(0);
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < grid.GetLength(0); y++)
                {
                    var tile = tiles[x * xLength + y];
                    grid[x, y] = new OrientedTile(tile, Rotation.NoRotation, Reflection.NoReflection);
                }
            }
        }

        private IList<IList<Tile>> getTileCombinations(IList<Tile> tiles)
        {
            throw new NotImplementedException();
        }

        private OrientedTile[,] getEmptyTileGrid(IEnumerable<Tile> tiles)
        {
            var square = (int) Math.Sqrt(tiles.Count());

            return new OrientedTile[square, square];
        }

        private OrientedTile getTile(OrientedTile[,] solvedTiles, Point2D solvedPosition)
        {
            if (0 <= solvedPosition.X && solvedPosition.X < solvedTiles.GetLength(0)
              && 0 <= solvedPosition.Y && solvedPosition.Y < solvedTiles.GetLength(1))
            {
                return solvedTiles[solvedPosition.X, solvedPosition.Y];
            }

            return null;
        }

        private IDictionary<Side, Point2D> loadSideOffsets()
        {
            var sidesWithOffsets = new Dictionary<Side, Point2D>()
            {
                { Side.Left, new Point2D(0, -1) },
                { Side.Up, new Point2D(-1, 0) },
                { Side.Right, new Point2D(0, 1) },
                { Side.Down, new Point2D(1, 0) }
            };

            return sidesWithOffsets;
        }

        private Point2D getPointTouchingSide(
            Point2D origin,
            Side side)
        {
            var offset = sideOffset[side];

            return new Point2D(
                origin.X + offset.X,
                origin.Y + offset.Y);
        }

        private bool edgesMatch(
            string edgeA,
            string edgeB)
        {
            return edgeA != null && edgeA.Equals(edgeB);
        }

        private long getCornerMultiplications(OrientedTile[,] solvedTiles)
        {
            var N = solvedTiles.GetLength(0);

            return
                solvedTiles[0, 0].Id
                * solvedTiles[0, N].Id
                * solvedTiles[N, 0].Id
                * solvedTiles[N, N].Id;
        }
    }
}
