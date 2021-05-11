namespace advent_of_code_2020.Day18
{
    class Literal : TreeNode
    {
        private readonly long value;

        public Literal(long value)
        {
            this.value = value;
        }

        public override long Evaluate()
        {
            return value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
