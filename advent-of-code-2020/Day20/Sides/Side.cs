namespace advent_of_code_2020.Day20
{
    abstract class Side
    {
        public readonly Dimension X;
        public readonly Dimension Y;

        public Side(Dimension x, Dimension y)
        {
            X = x;
            Y = y;
        }
    }
}
