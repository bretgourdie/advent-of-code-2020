using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2020.Day03
{
    class Solution : AdventSolution<string>
    {
        private const char tree = '#';

        protected override void performWorkForProblem1(IList<string> inputData)
        {
            traverseWithAllToboggans(
                inputData,
                new Toboggan[]
                {
                    new Toboggan(3, 1)
                }
            );
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            traverseWithAllToboggans(
                inputData,
                new Toboggan[]
                {
                    new Toboggan(1, 1),
                    new Toboggan(3, 1),
                    new Toboggan(5, 1),
                    new Toboggan(7, 1),
                    new Toboggan(1, 2)
                }
            );
        }

        private void traverseWithAllToboggans(
            IList<string> inputData,
            IList<Toboggan> toboggans)
        {
            var trees = new List<int>();

            foreach (var toboggan in toboggans)
            {
                trees.Add(
                    traverseWithOneToboggan(
                        inputData,
                        toboggan
                    )
                );
            }

            long treesProduct = 1;

            foreach (var tree in trees)
            {
                treesProduct *= tree;
            }

            Console.WriteLine($"Encountered the product of {treesProduct} trees");
        }

        private int traverseWithOneToboggan(
            IList<string> inputData,
            Toboggan toboggan)
        {
            int numberOfTrees = 0;
            int xLocation = 0;
            int yLocation = 0;
            string row = inputData.First();

            while (yLocation + toboggan.Y < inputData.Count)
            {
                xLocation = (xLocation + toboggan.X) % row.Length;
                yLocation += toboggan.Y;

                row = inputData[yLocation];

                if (row[xLocation] == tree)
                {
                    numberOfTrees += 1;
                }
            }

            return numberOfTrees;
        }
    }
}
