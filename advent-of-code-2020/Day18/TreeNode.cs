namespace advent_of_code_2020.Day18
{
    abstract class TreeNode
    {
        public abstract long Evaluate();
        public abstract override string ToString();

        protected string wrapInParens(string expression)
        {
            return "(" + expression + ")";
        }
    }
}
