namespace advent_of_code_2020.Day20
{
    class Left : Side
    {
        public Left(int length) : base(
            new Dimension(0, 0),
            new Dimension(0, length - 1))
        { }
    }
}
