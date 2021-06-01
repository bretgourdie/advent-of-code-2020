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
    }
}
