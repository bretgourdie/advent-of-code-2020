using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2020.Day20
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            var tileContent = new List<string>();

            foreach (var line in inputData)
            {
                if (string.IsNullOrEmpty(line))
                {
                    var tile = new Tile(tileContent);
                    Console.WriteLine(tile);
                    tileContent = new List<string>();
                }

                else
                {
                    tileContent.Add(line);
                }
            }
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            throw new NotImplementedException();
        }
    }
}
