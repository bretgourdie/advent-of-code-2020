namespace advent_of_code_2020.Day18
{
    class Multiply : TreeNode
    {
        private readonly TreeNode a;
        private readonly TreeNode b;

        public Multiply(TreeNode a, TreeNode b)
        {
            this.a = a;
            this.b = b;
        }

        public override long Evaluate()
        {
            return a.Evaluate() * b.Evaluate();
        }

        public override string ToString()
        {
            return wrapInParens(a.ToString() + "*" + b.ToString());
        }
    }
}
