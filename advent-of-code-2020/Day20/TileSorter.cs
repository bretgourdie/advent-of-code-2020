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

        public long Sort(
            Queue<Tile> tiles,
            IDictionary<Point2D, OrientedTile> board)
        {
            if (!tiles.Any()) return getCornerMultiplications(board);

            while (tiles.Any())
            {
                var tile = tiles.Dequeue();
            }

            throw new NotImplementedException();
        }

        private IList<IList<Tile>> getTileCombinations(IList<Tile> tiles)
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

        private long getCornerMultiplications(IDictionary<Point2D, OrientedTile> solvedTiles)
        {
            throw new NotImplementedException();
        }
    }
}
