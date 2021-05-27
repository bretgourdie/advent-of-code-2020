namespace advent_of_code_2020.Day20
{
    class Right : Side
    {
        public Right(int length) : base(
            new Dimension(length - 1, length - 1),
            new Dimension(0, length))
        { }
    }
}
