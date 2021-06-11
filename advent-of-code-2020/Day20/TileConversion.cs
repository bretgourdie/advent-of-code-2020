using System.Collections.Generic;
using E = advent_of_code_2020.Day20.EnumHelper;

namespace advent_of_code_2020.Day20
{
    static class TileConversion
    {
        public static IList<OrientedTile> GetTilePermutations(Tile tile)
        {
            var permutations = new List<OrientedTile>();

            foreach (var rotation in E.RotationsClockwise)
            {
                foreach (var reflection in E.Reflections)
                {
                    permutations.Add(new OrientedTile(
                        tile,
                        rotation,
                        reflection)
                    );
                }
            }

            return permutations;
        }

    }
}
