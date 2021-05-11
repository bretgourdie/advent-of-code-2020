using System;
using System.Linq;

namespace advent_of_code_2020.Day18
{
    class BasicMathEvaluator : IEvaluator
    {
        private string expression;
        private char nextToken;

        public long Evaluate(string expression)
        {
            this.expression = expression.Replace(" ", "");

            scanToken();
            var resultTree = parseE();

            if (isEnd())
            {
                throw new Exception();
            }

            return resultTree.Evaluate();
        }

        private bool isEnd()
        {
            return nextToken == default(char);
        }

        private char scanToken()
        {
            if (expression.Any())
            {
                nextToken = expression[0];
                expression = expression.Substring(1);
            }

            return default(char);
        }

        private TreeNode parseE()
        {
            var a = parseF();

            while (true)
            {
                if (nextToken == '+')
                {
                    scanToken();
                    var b = parseF();

                    a = new Add(a, b);
                }

                else if (nextToken == '*')
                {
                    scanToken();
                    var b = parseF();

                    a = new Multiply(a, b);
                }

                else
                {
                    scanToken();
                    return a;
                }
            }
        }

        private TreeNode parseF()
        {
            if (long.TryParse(nextToken.ToString(), out long value))
            {
                scanToken();
                return new Literal(value);
            }

            else if (nextToken == '(')
            {
                scanToken();
                return parseE();
            }

            return null;
        }
    }
}
