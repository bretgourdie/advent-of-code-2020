using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day20
{
    class TileSorter
    {
        private readonly IList<Side> sidesClockwise;
        private readonly IList<Rotation> rotationsClockwise;
        private readonly IList<Reflection> reflections;
        private readonly IDictionary<Side, Point2D> sideOffset;

        public TileSorter()
        {
            sidesClockwise = new List<Side>() {Side.Left, Side.Up, Side.Right, Side.Down};
            rotationsClockwise = new List<Rotation>()
            {
                Rotation.NoRotation,
                Rotation.Clockwise90,
                Rotation.Clockwise180,
                Rotation.Clockwise270
            };
            reflections = new List<Reflection>() {Reflection.NoReflection, Reflection.Flip};
            sideOffset = loadSideOffsets();
        }

        private IDictionary<Tile, IList<OrientedTile>> loadTilesToPermutationDictionary(
            IEnumerable<Tile> tiles)
        {
            var tileDict = new Dictionary<Tile, IList<OrientedTile>>();

            foreach (var tile in tiles)
            {
                tileDict[tile] = getTilePermutations(tile);
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
        }

        private Side getOppositeSide(Side side)
        {
            return sidesClockwise[(sidesClockwise.IndexOf(side) + 2) % sidesClockwise.Count];
        }

        private IDictionary<Side, Point2D> loadSideOffsets()
        {
            var sidesWithOffsets = new Dictionary<Side, Point2D>()
            {
                { Side.Left, new Point2D(-1, 0) },
                { Side.Up, new Point2D(0, -1) },
                { Side.Right, new Point2D(1, 0) },
                { Side.Down, new Point2D(0, 1) }
            };

            return sidesWithOffsets;
        }

        private IList<OrientedTile> getTilePermutations(Tile tile)
        {
            var permutations = new List<OrientedTile>();

            foreach (var rotation in rotationsClockwise)
            {
                foreach (var reflection in reflections)
                {
                    permutations.Add(new OrientedTile(
                        tile,
                        rotation,
                        reflection,
                        rotationsClockwise,
                        sidesClockwise));
                }
            }

            return permutations;
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
            var min = solvedTiles.Keys.Min(point => point.X);
            var max = solvedTiles.Keys.Max(point => point.Y);

            return
                solvedTiles[new Point2D(min, min)].Id
                * solvedTiles[new Point2D(min, max)].Id
                * solvedTiles[new Point2D(max, min)].Id
                * solvedTiles[new Point2D(max, max)].Id;
        }
    }
}
