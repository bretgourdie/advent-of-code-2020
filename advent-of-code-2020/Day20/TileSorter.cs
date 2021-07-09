using System;
using System.Collections.Generic;
using System.Linq;
using E = advent_of_code_2020.Day20.EnumHelper;

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
            return sort(
                new Queue<Tile>(tiles), 
                new Dictionary<Point2D, OrientedTile>(),
                (int)Math.Sqrt(tiles.Count()));
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

                foreach (var point in getAvailablePositions(board, dimension))
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

                tiles.Enqueue(tile);

            } while (tiles.Any() && tiles.Peek() != firstTile);

            return SORT_FAILURE;
        }

        private bool tileFits(
            IDictionary<Point2D, OrientedTile> board,
            Point2D testingPoint,
            OrientedTile testingTile)
        {
            foreach (var testingSide in E.SidesClockwise)
            {
                var checkingPoint = getPointTouchingSide(testingPoint, testingSide);

                if (!board.ContainsKey(checkingPoint)) continue;

                var checkingSide = SideConversion.GetOppositeSide(testingSide);
                var checkingTile = board[checkingPoint];

                if (!edgesMatch(
                    testingTile.GetEdge(testingSide),
                    checkingTile.GetEdge(checkingSide))
                    )
                {
                    return false;
                }
            }

            return true;
        }

        private IEnumerable<Point2D> getAvailablePositions(
            IDictionary<Point2D, OrientedTile> board,
            int dimension)
        {
            if (!board.Any())
            {
                return new[] {new Point2D(dimension / 2, dimension / 2)};
            }

            var positions = new List<Point2D>();

            foreach (var pointAndTile in board)
            {
                var point = pointAndTile.Key;
                foreach (var offset in sideOffset.Values)
                {
                    positions.Add(
                        new Point2D(
                            point.X + offset.X,
                            point.Y + offset.Y));
                }
            }

            positions.RemoveAll(point =>
                point.X < 0
                || point.Y < 0
                || point.X >= dimension
                || point.Y >= dimension);

            positions.RemoveAll(point =>
                board.Keys.Contains(point));

            return positions;
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
