using System;
using System.Linq;
using E = advent_of_code_2020.Day20.EnumHelper;

namespace advent_of_code_2020.Day20
{
    static class SideConversion
    {
        public static Side GetOppositeSide(Side side)
        {
            return E.SidesClockwise[(E.SidesClockwise.IndexOf(side) + 2) % E.SidesClockwise.Count];
        }

        public static string GetOrientedSide(
            Tile tile,
            Side side,
            Rotation rotation,
            Reflection reflection)
        {
            var rotationOffset = E.RotationsClockwise.IndexOf(rotation);

            var sideIndex = E.SidesClockwise.IndexOf(side);

            sideIndex = (sideIndex + rotationOffset) % E.SidesClockwise.Count;

            var translatedSide = E.SidesClockwise[sideIndex];

            var edge = tile.GetEdge(translatedSide);

            if (reflection == Reflection.Flip)
            {
                return String.Concat(edge.Reverse());
            }

            return edge;
        }
    }
}
