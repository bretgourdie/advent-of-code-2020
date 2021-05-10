namespace advent_of_code_2020.Day17
{
    struct Point3D
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Point3D)) return false;
            Point3D point = (Point3D) obj;

            return
                point.X == this.X
                && point.Y == this.Y
                && point.Z == this.Z;
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z}";
        }
    }
}
