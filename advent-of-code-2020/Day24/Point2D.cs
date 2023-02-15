using System.Reflection.Emit;

namespace advent_of_code_2020.Day24
{
    struct Point2D
    {
        public readonly int Q;
        public readonly int R;

        public Point2D(int q, int r)
        {
            Q = q;
            R = r;
        }

        public override string ToString()
        {
            return $"[{Q},{R}]";
        }
    }
}
