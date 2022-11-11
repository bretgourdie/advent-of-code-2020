namespace advent_of_code_2020.Day12
{
    public class ShipTransform : Transform
    {
        public Direction Direction { get; private set; }

        public ShipTransform(int x, int y, Direction direction) : base(x, y)
        {
            Direction = direction;
        }
    }
}
