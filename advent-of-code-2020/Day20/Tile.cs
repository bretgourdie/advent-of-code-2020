using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day20
{
    class Tile
    {
        public readonly int Id;

        public readonly IList<string> Piece;

        private readonly int length;

        public Tile(IList<string> tileChunk)
        {
            var header = tileChunk.First();
            var split = header.Split(' ');
            Id = int.Parse(split[1].Replace(":", ""));

            Piece = new List<string>(tileChunk.Skip(1));
            length = Piece.Count;
        }

        public Edge GetEdge(Side side, Reflection reflection)
        {
            var contents = new List<char>();

            int xStart = 0, xEnd = 0, xDirection = 0;
            int yStart = 0, yEnd = 0, yDirection = 0;

            switch (side)
            {
                case Side.Up:
                    break;
                case Side.Down:
                    break;
                case Side.Left:
                    break;
                case Side.Right:
                    break;
            }

            return new Edge(contents, rotation, reflection);
        }
    }
}
