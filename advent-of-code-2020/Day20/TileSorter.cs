using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day20
{
    class TileSorter
    {
        private readonly IDictionary<Side, Point2D> sideOffset;
        private const long SORT_FAILURE = 0;

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

        public long Sort(IEnumerable<Tile> tiles)
        {
            throw new NotImplementedException();
        }

        private long sort(
            Queue<Tile> tiles,
            IDictionary<Point2D, OrientedTile> board,
            int dimension)
        {
            if (!tiles.Any()) return getCornerMultiplications(board, dimension);

            var firstTile = tiles.Peek();

            do
            {
                var tile = tiles.Dequeue();

                foreach (var point in getAvailablePositions(board))
                {
                    foreach (var orientedTile in TileConversion.GetTilePermutations(tile))
                    {
                        if (tileFits(board, point, orientedTile))
                        {
                            board.Add(new KeyValuePair<Point2D, OrientedTile>(point, orientedTile));
                            var sortResult = sort(tiles, board, dimension);

                            if (sortResult == SORT_FAILURE)
                            {
                                board.Remove(point);
                            }

                            else
                            {
                                return sortResult;
                            }
                        }
                    }
                }

            } while (tiles.Any() && tiles.Peek() != firstTile);

            return SORT_FAILURE;
        }

        private bool tileFits(
            IDictionary<Point2D, OrientedTile> board,
            Point2D testingPoint,
            OrientedTile testingTile)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Point2D> getAvailablePositions(
            IDictionary<Point2D, OrientedTile> board)
        {
            throw new NotImplementedException();
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

        private long getCornerMultiplications(
            IDictionary<Point2D, OrientedTile> solvedTiles,
            int dimension)
        {
            return
                solvedTiles[new Point2D(0, 0)].Id
                * solvedTiles[new Point2D(0, dimension)].Id
                * solvedTiles[new Point2D(dimension, 0)].Id
                * solvedTiles[new Point2D(dimension, dimension)].Id;
        }
    }
}
