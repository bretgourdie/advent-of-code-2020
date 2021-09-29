using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2020.Day20
{
    class OrientedTile
    {
        private readonly Tile tile;
        private readonly Rotation rotation;
        private readonly Reflection reflection;

        public OrientedTile(
            Tile tile,
            Rotation rotation,
            Reflection reflection)
        {
            this.tile = tile;
            this.rotation = rotation;
            this.reflection = reflection;
        }


    }
}
