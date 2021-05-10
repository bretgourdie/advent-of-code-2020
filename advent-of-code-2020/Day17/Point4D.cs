namespace advent_of_code_2020.Day17
{
    struct Point4D
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int W { get; set; }

        public Point4D(int x, int y, int z, int w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Point4D)) return false;
            Point4D point = (Point4D) obj;

            return
                point.X == this.X
                && point.Y == this.Y
                && point.Z == this.Z
                && point.W == this.W;
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z},{W}";
        }
    }
}
