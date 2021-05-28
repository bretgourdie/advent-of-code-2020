using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace advent_of_code_2020.Day20
{
    class Tile
    {
        public readonly int Id;

        public readonly IList<string> Piece;
        private readonly IDictionary<Side, Dimensions> dimensionsForSide;
        public readonly int Length;

        public Tile(IList<string> tileChunk)
        {
            var header = tileChunk.First();
            var split = header.Split(' ');
            Id = int.Parse(split[1].Replace(":", ""));

            Piece = new List<string>(tileChunk.Skip(1));
            Length = Piece.Count;
            dimensionsForSide = loadDimensionsForSide(Length);
        }

        public Edge GetEdge(Side side, Reflection reflection)
        {
            var contents = new List<char>();

            var dimensions = dimensionsForSide[side];

            var X = dimensions.X;
            var Y = dimensions.Y;

            var deltaX = getDelta(X);
            var deltaY = getDelta(Y);

            for (int x = X.Start, y = Y.Start; x <= X.End && y <= Y.End; x += deltaX, y += deltaY)
            {
                contents.Add(getPixel(x, y));
            }

            if (reflection == Reflection.Flip)
            {
                contents.Reverse();
            }

            return new Edge(contents, reflection);
        }

        private char getPixel(int x, int y)
        {
            // reverse x and y since Piece is by row, by column instead of by column, by row
            return Piece[y][x];
        }

        private int getDelta(Dimension dimension)
        {
            return Math.Min(dimension.End - dimension.Start, 1);
        }

        private IDictionary<Side, Dimensions> loadDimensionsForSide(int length)
        {
            var startUnchanging = new Dimension(0, 0);
            var startToEnd = new Dimension(0, length - 1);
            var endUnchanging = new Dimension(length - 1, length - 1);

            var dict = new Dictionary<Side, Dimensions>();

            dict.Add(Side.Up, new Dimensions(startToEnd, startUnchanging));
            dict.Add(Side.Down, new Dimensions(startToEnd, endUnchanging));
            dict.Add(Side.Left, new Dimensions(startUnchanging, startToEnd));
            dict.Add(Side.Right, new Dimensions(endUnchanging, startToEnd));

            return dict;
        }
    }
}
