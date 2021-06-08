using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day20
{
    class SolvedTile
    {
        private IDictionary<Side, SolvedTile> matchedSides;
        private readonly OrientedTile orientedTile;

        public int Id => orientedTile.Id;

        public SolvedTile(OrientedTile orientedTile)
        {
            this.orientedTile = orientedTile;
            matchedSides = new Dictionary<Side, SolvedTile>();
        }

        public void SetMatch(SolvedTile solvedTile, Side side)
        {
            if (matchedSides.ContainsKey(side))
            {
                throw new ArgumentException($"Side {side} is already matched");
            }

            matchedSides[side] = solvedTile;
        }

        public IList<Side> GetUnmatchedSides(IList<Side> allSides)
        {
            return
                matchedSides.Keys
                    .Where(side => !allSides.Contains(side))
                    .ToList();
        }

        public string GetEdge(Side side)
        {
            return orientedTile.GetEdge(side);
        }
    }
}
