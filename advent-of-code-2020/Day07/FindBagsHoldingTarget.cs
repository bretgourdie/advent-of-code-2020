namespace advent_of_code_2020.Day07
{
    class FindBagsHoldingTarget : ITraversal
    {
        public int Find(BagTree bagTree, string targetColor)
        {
            return countBagsHoldingTarget(bagTree, targetColor);
        }

        private int countBagsHoldingTarget(BagTree bagTree, string targetBag)
        {
            int bagsHoldingTarget = 0;

            foreach (var parentColor in bagTree.GetParents())
            {
                var children = bagTree.GetEdges(parentColor);

                foreach (var childEdge in children)
                {
                    var foundInColor = isTargetInBagOrChildren(bagTree, childEdge, targetBag);

                    if (foundInColor != null)
                    {
                        bagsHoldingTarget += 1;
                        break;
                    }
                }
            }

            return bagsHoldingTarget;
        }

        private string isTargetInBagOrChildren(
            BagTree bagTree,
            BagEdge currentEdge,
            string targetBag)
        {
            if (currentEdge.ToColor == targetBag) return currentEdge.FromColor;
            if (string.IsNullOrEmpty(currentEdge.ToColor)) return null;

            foreach (var childEdge in bagTree.GetEdges(currentEdge.ToColor))
            {
                var result = isTargetInBagOrChildren(bagTree, childEdge, targetBag);

                if (result != null) return result;
            }

            return null;
        }

        public string GetResultMessage(int result, string targetColor)
        {
            return $"Number of bags that contain {targetColor} are {result}";
        }
    }
}
