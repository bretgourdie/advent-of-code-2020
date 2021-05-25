using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day10
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            var adapters = getSortedListOfAdapters(inputData);

            int jolt1Differences = 0, jolt3Differences = 0;

            for (int ii = 1; ii < adapters.Count; ii++)
            {
                int previous = adapters[ii - 1];
                int current = adapters[ii];

                int difference = current - previous;

                if (difference == 1)
                {
                    jolt1Differences += 1;
                }

                else if (difference == 3)
                {
                    jolt3Differences += 1;
                }
            }

            Console.WriteLine($"1-jolt diffs x 3-jolt diffs = {jolt1Differences * jolt3Differences}");
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            var adapters = getSortedListOfAdapters(inputData);

            long numberOfDistinctAdapters = getDistinctAdapters(adapters, 0, new Dictionary<int, long>());

            Console.WriteLine($"The number of distinct adapter configurations is {numberOfDistinctAdapters}");
        }

        private long getDistinctAdapters(
            IList<int> adapters,
            int index,
            IDictionary<int, long> memoizedConfigurations)
        {
            if (memoizedConfigurations.ContainsKey(index)) return memoizedConfigurations[index];

            long numberOfDistinctConfigurations = 0;

            for (int ii = index + 1; ii < adapters.Count && ii < index + 4; ii++)
            {
                if (canConnect(adapters, index, ii))
                {
                    if (isLastNode(adapters, ii))
                    {
                        numberOfDistinctConfigurations += 1;
                    }

                    else
                    {
                        numberOfDistinctConfigurations += getDistinctAdapters(adapters, ii, memoizedConfigurations);
                    }
                }
            }

            memoizedConfigurations[index] = numberOfDistinctConfigurations;

            return numberOfDistinctConfigurations;
        }

        private bool canConnect(IList<int> adapters, int startNode, int endNode)
        {
            return adapters[endNode] - adapters[startNode] <= 3;
        }

        private bool isLastNode(IList<int> adapters, int index)
        {
            return index + 1 == adapters.Count;
        }

        private IList<int> getSortedListOfAdapters(IList<string> inputData)
        {
            var adapters = new List<int>(inputData.Select(x => int.Parse(x)));
            adapters.Add(0);
            adapters.Add(adapters.Max() + 3);
            adapters.Sort();

            return adapters;
        }
    }
}
