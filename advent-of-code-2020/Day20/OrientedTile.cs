namespace advent_of_code_2020.Day20
{
    class OrientedTile
    {
        public int Id => tile.Id;

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

        public string GetEdge(Side side)
        {
            return SideConversion.GetOrientedSide(tile, side, rotation, reflection);
        }
    }
}
