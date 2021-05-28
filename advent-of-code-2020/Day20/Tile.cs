using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day20
{
    class Tile
    {
        public readonly int Id;

        public readonly IList<string> Piece;
        public readonly int Length;

        public Tile(IList<string> tileChunk)
        {
            var header = tileChunk.First();
            var split = header.Split(' ');
            Id = int.Parse(split[1].Replace(":", ""));

            Piece = new List<string>(tileChunk.Skip(1));
            Length = Piece.Count;
        }

        public Edge GetEdge(Side side, Reflection reflection)
        {
            var contents = new List<char>();

            var X = side.X;
            var Y = side.Y;

            var deltaX = getDelta(X);
            var deltaY = getDelta(Y);

            for (int x = X.Start, y = Y.Start; x <= X.End && y <= Y.End; x += deltaX, y += deltaY)
            {
                contents.Add(Piece[y][x]);
            }

            if (reflection == Reflection.Flip)
            {
                contents.Reverse();
            }

            return new Edge(contents, reflection);
        }

        private int getDelta(Dimension dimension)
        {
            return Math.Min(dimension.End - dimension.Start, 1);
        }
    }
}
