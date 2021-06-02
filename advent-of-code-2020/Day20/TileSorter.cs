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

        private IDictionary<Point2D, OrientedTile> sortedTiles;

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
        }

        public long Sort(Queue<Tile> tiles)
        {
            sortedTiles = new Dictionary<Point2D, OrientedTile>();

            var first = tiles.Dequeue();

            sortedTiles[new Point2D(0, 0)] = new OrientedTile(
                first,
                Rotation.NoRotation,
                Reflection.NoReflection,
                rotationsClockwise,
                sidesClockwise);

            while (tiles.Any())
            {
                var tile = tiles.Dequeue();

                foreach (var positionAndSortedTile in sortedTiles)
                {
                    var position = positionAndSortedTile.Key;
                    var sortedTile = positionAndSortedTile.Value;

                    for (int rotationIndex = 0; rotationIndex < rotationsClockwise.Count; rotationIndex++)
                    {
                        var rotation = rotationsClockwise[rotationIndex];

                        for (int reflectionIndex = 0; reflectionIndex < reflections.Count; reflectionIndex++)
                        {
                            var reflection = reflections[reflectionIndex];

                            for (int sideIndex = 0; sideIndex < sidesClockwise.Count; sideIndex++)
                            {
                                var side = sidesClockwise[sideIndex];
                            }
                        }
                    }
                }
            }
        }

        private bool edgesMatch(
            string edgeA,
            string edgeB)
        {
            return edgeA != null && edgeA.Equals(edgeB);
        }

        private long getCornerMultiplications(IList<IList<OrientedTile>> sortedTiles)
        {
            int N = sortedTiles.Count - 1;

            return
                sortedTiles[0][0].Id
                * sortedTiles[0][N].Id
                * sortedTiles[N][0].Id
                * sortedTiles[N][N].Id;
        }
    }
}
