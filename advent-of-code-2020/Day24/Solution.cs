using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day24
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            var flippedTiles = new Lobby().FlipTiles(inputData);
            Console.WriteLine(
                $"The number of tiles flipped to black are {flippedTiles}");
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            var lobby = new Lobby();
            lobby.FlipTiles(inputData);

            var flipFor100Days = lobby.LivingFlipForDays(100);

            Console.WriteLine(
                $"The number of tiles flipped to black after 100 days is {flipFor100Days}");
        }
    }
}
