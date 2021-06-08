using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day20
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            var cornerProduct = new TileSorter().Sort(loadTiles(inputData));

            Console.WriteLine($"The product of the corner tiles is {cornerProduct}");
        }

        private Queue<Tile> loadTiles(IList<string> inputData)
        {
            var tiles = new Queue<Tile>();
            const int tileLength = 12;

            for (int ii = 0; ii < inputData.Count; ii += tileLength)
            {
                var tileChunk = inputData.Skip(ii).Take(tileLength - 1).ToList();
                tiles.Enqueue(new Tile(tileChunk));
            }

            return tiles;
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            throw new NotImplementedException();
        }
    }
}
