namespace advent_of_code_2020.Day20
{
    class Up : Side
    {
        public Up(int length) : base(
            new Dimension(0, length - 1),
            new Dimension(0, 0))
        { }
    }
}
