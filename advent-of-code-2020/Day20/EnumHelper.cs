using System.Collections.Generic;

namespace advent_of_code_2020.Day20
{
    static class EnumHelper
    {
        public static readonly IList<Rotation> RotationsClockwise = new List<Rotation>()
        {
            Rotation.NoRotation,
            Rotation.Clockwise90,
            Rotation.Clockwise180,
            Rotation.Clockwise270
        };
        public static readonly IList<Side> SidesClockwise = new List<Side>()
        {
            Side.Left,
            Side.Up,
            Side.Right,
            Side.Down
        };
        public static readonly IList<Reflection> Reflections = new List<Reflection>()
        {
            Reflection.NoReflection, Reflection.Flip
        };
    }
}
