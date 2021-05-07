namespace advent_of_code_2020.Day07
{
    class BagEdge
    {
        public int Weight { get; private set; }
        public string FromColor { get; private set; }
        public string ToColor { get; private set; }

        public BagEdge(
            int weight,
            string fromColor,
            string toColor)
        {
            Weight = weight;
            FromColor = fromColor;
            ToColor = toColor;
        }
    }
}
