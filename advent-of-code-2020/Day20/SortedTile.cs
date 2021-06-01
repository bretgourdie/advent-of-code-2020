namespace advent_of_code_2020.Day20
{
    class SortedTile
    {
        private readonly Tile tile;
        private readonly Rotation rotation;
        private readonly Reflection reflection;

        public SortedTile(
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
