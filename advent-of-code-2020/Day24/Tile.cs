namespace advent_of_code_2020.Day24
{
    class Tile
    {
        public readonly Point2D Point2D;

        private bool wasFlipped;

        public Color Color
        {
            get => wasFlipped ? Color.Black : Color.White;
        }

        public Tile(Point2D point2d)
        {
            Point2D = point2d;
        }

        public void Flip() => wasFlipped = !wasFlipped;
    }
}
