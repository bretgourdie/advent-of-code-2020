namespace advent_of_code_2020.Day07
{
    class FindIndividualBagsInTarget : ITraversal
    {
        public int Find(BagTree bagTree, string targetColor)
        {
            return getBagCount(
                bagTree,
                targetColor)
                - 1; // ???
        }

        private int getBagCount(
            BagTree bagTree,
            string color)
        {
            if (color == null) return 1;

            int subBags = 1;

            var edges = bagTree.GetEdges(color);

            foreach (var edge in edges)
            {
                subBags += getBagCount(bagTree, edge.ToColor) * edge.Weight;
            }

            return subBags;
        }

        public string GetResultMessage(int result, string targetColor)
        {
            return $"Number of bags in {targetColor} is {result}";
        }
    }
}
