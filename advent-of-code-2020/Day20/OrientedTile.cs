using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day20
{
    class OrientedTile
    {
        public int Id => tile.Id;

        private readonly Tile tile;
        private readonly Rotation rotation;
        private readonly Reflection reflection;

        private readonly IList<Rotation> rotationsClockwise;
        private readonly IList<Side> sidesClockwise;

        public OrientedTile(
            Tile tile,
            Rotation rotation,
            Reflection reflection,
            IList<Rotation> rotationsClockwise,
            IList<Side> sidesClockwise)
        {
            this.tile = tile;
            this.rotation = rotation;
            this.reflection = reflection;

            this.rotationsClockwise = rotationsClockwise;
            this.sidesClockwise = sidesClockwise;
        }

        public string GetEdge(Side side)
        {
            var rotationOffset = rotationsClockwise.IndexOf(rotation);

            var sideIndex = sidesClockwise.IndexOf(side);

            sideIndex = (sideIndex + rotationOffset) % sidesClockwise.Count;

            var translatedSide = sidesClockwise[sideIndex];

            var edge = tile.GetEdge(translatedSide);

            if (reflection == Reflection.Flip)
            {
                return String.Concat(edge.Reverse());
            }

            return edge;
        }
    }
}
