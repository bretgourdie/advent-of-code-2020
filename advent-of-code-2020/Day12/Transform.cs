namespace advent_of_code_2020.Day12
{
    public class Transform
    {
        public readonly Direction Direction;
        public readonly int X;
        public readonly int Y;

        public Transform(
            int x,
            int y) : this(x, y, Direction.North) { }

        public Transform(
            int x,
            int y,
            Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public override string ToString()
        {
            return $"{X},{Y}, {Direction}";
        }
    }
}
