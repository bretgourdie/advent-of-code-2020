namespace advent_of_code_2020.Day20
{
    class Down : Side
    {
        public Down(int length) : base(
            new Dimension(0, length - 1),
            new Dimension(length - 1, length - 1))
        { }
    }
}
