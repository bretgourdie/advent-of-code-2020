namespace advent_of_code_2020.Day20
{
    class SortedTile
    {
        public readonly Tile Tile;
        public readonly Rotation Rotation;
        public readonly Reflection Reflection;
        public readonly Point2D Position;

        public SortedTile(
            Tile tile,
            Rotation rotation,
            Reflection reflection,
            Point2D position)
        {
            Tile = tile;
            Rotation = rotation;
            Reflection = reflection;
            Position = position;
        }
    }
}
