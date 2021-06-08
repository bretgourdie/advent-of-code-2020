using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day20
{
    class Tile
    {
        public readonly int Id;
        public readonly int Length;
        private readonly char[,] piece;

        public Tile(IList<string> tileChunk)
        {
            var header = tileChunk.First();
            var split = header.Split(' ');
            Id = int.Parse(split[1].Replace(":", ""));

            piece = loadPiece(tileChunk.Skip(1).ToList());
            Length = piece.GetLength(0);
        }

        private char[,] loadPiece(IList<string> tileChunk)
        {
            int xLength = tileChunk.Count;
            int yLength = tileChunk.First().Length;

            var piece = new char[xLength, yLength];

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    piece[x, y] = tileChunk[x][y];
                }
            }

            return piece;
        }

        public string GetEdge(
            Side side)
        {
            var chars = new List<char>();

            var start = getStart(side);
            var vector = getVector(side);

            for (
                int x = start.X, y = start.Y;
                x < piece.GetLength(0) && y < piece.GetLength(1);
                x += vector.X, y += vector.Y)
            {
                chars.Add(piece[x, y]);
            }

            return String.Concat(chars);
        }

        private Point2D getStart(Side side)
        {
            switch (side)
            {
                case Side.Left: case Side.Up: return new Point2D(0, 0);
                case Side.Right: return new Point2D(0, Length - 1);
                case Side.Down: return new Point2D(Length - 1, 0);
            }

            throw new ArgumentException();
        }

        private Point2D getVector(Side side)
        {
            switch (side)
            {
                case Side.Up: case Side.Down: return new Point2D(0, 1);
                case Side.Left: case Side.Right: return new Point2D(1, 0);
            }

            throw new ArgumentException();
        }
    }
}
