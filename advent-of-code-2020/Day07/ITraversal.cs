namespace advent_of_code_2020.Day07
{
    interface ITraversal
    {
        int Find(BagTree bagTree, string targetColor);
        string GetResultMessage(int result, string targetColor);
    }
}
