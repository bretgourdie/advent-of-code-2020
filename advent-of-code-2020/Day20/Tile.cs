using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2020.Day20
{
    class Tile
    {
        public readonly int Id;
        public readonly IDictionary<Side, int> SideHashes;
        public readonly IDictionary<Side, int> ReversedSideHashes;

        private readonly char[,] piece;

        public Tile(IList<string> contents)
        {
            Id = parseIdLine(contents.First());

            piece = parsePieceLines(contents.Skip(1).ToList());

            //SideHashes = getSideHashes(piece);

            //ReversedSideHashes = getReversedSideHashes(piece);

        }

        private int parseIdLine(string idLine)
        {
            return int.Parse(
                idLine.Replace("Tile ", String.Empty)
                    .Replace(":", String.Empty)
            );
        }

        private char[,] parsePieceLines(IList<string> lines)
        {
            var length = lines.First().Length;

            var grid = new char[length, length];

            for (int ii = 0; ii < lines.Count; ii++)
            {
                for (int jj = 0; jj < lines[ii].Length; jj++)
                {
                    assignToGrid(ii, jj, lines[ii][jj], grid);
                }
            }

            return grid;
        }

        private IDictionary<Side, int> getSideHashes(char[,] piece)
        {
            throw new NotImplementedException();
        }

        private IDictionary<Side, int> getReversedSideHashes(char[,] piece)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int ii = 0; ii < piece.GetLength(0); ii++)
            {
                for (int jj = 0; jj < piece.GetLength(1); jj++)
                {
                    sb.Append(getFromGrid(ii, jj, piece));
                }

                sb.AppendLine();
            }

            return
                $"Tile {Id}:"
                + Environment.NewLine
                + sb;
        }


        private void assignToGrid(int x, int y, char letter, char[,] grid)
        {
            grid[x, y] = letter;
        }

        private char getFromGrid(int x, int y, char[,] grid)
        {
            return grid[x, y];
        }
    }
}
