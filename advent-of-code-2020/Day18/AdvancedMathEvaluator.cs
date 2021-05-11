using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day18
{
    class AdvancedMathEvaluator : IEvaluator
    {
        private string expression;

        private int curTokenIndex;

        private Queue<char> output;
        private Stack<char> operators;

        public long Evaluate(string expression)
        {
            this.expression = expression.Replace(" ", "");
            curTokenIndex = -1;
            output = new Queue<char>();
            operators = new Stack<char>();

            var rpnQueue = shunt();
            var result = evaluateShunt(rpnQueue);

            return result;
        }

        private Queue<char> shunt()
        {
            while (canScanToken())
            {
                scanToken();

                char token = expression[curTokenIndex];

                if (isNumber(token, out long disregard))
                {
                    output.Enqueue(token);
                }

                else if (isLeftParens(token))
                {
                    operators.Push(token);
                }

                else if (isBinaryOperator(token))
                {
                    while
                    (
                        operators.Any()
                        && operatorOnStackHasEqualOrGreaterPrecedence(token)
                        && !isLeftParens(operators.Peek())
                    )
                    {
                        output.Enqueue(operators.Pop());
                    }
                    operators.Push(token);
                }

                else if (isLeftParens(token))
                {
                    operators.Push(token);
                }

                else if (isRightParens(token))
                {
                    while (!isLeftParens(operators.Peek()))
                    {
                        output.Enqueue(operators.Pop());
                    }

                    if (isLeftParens(operators.Peek()))
                    {
                        operators.Pop(); // discard
                    }
                }
            }

            while (operators.Any())
            {
                output.Enqueue(operators.Pop());
            }

            return output;
        }

        private long evaluateShunt(Queue<char> rpnQueue)
        {
            var stack = new Stack<TreeNode>();

            while (rpnQueue.Any())
            {
                var curElement = rpnQueue.Dequeue();

                if (isNumber(curElement, out long number))
                {
                    stack.Push(new Literal(number));
                }

                else if (isBinaryOperator(curElement))
                {
                    handleBinaryOperator(curElement, stack);
                }
            }

            var root = stack.Pop();
            return root.Evaluate();
        }

        private void handleBinaryOperator(
            char theOperator,
            Stack<TreeNode> resultStack)
        {
            var a = resultStack.Pop();
            var b = resultStack.Pop();

            if (theOperator == '+')
            {
                resultStack.Push(new Add(a, b));
            }

            else if (theOperator == '*')
            {
                resultStack.Push(new Multiply(a, b));
            }

            else
            {
                throw new ArgumentException("Not add or multiply", nameof(theOperator));
            }
        }

        private bool operatorOnStackHasEqualOrGreaterPrecedence(char other)
        {
            return getPrecedence(operators.Peek()) >= getPrecedence(other);
        }

        private bool canScanToken()
        {
            return curTokenIndex + 1 < expression.Length;
        }

        private void scanToken()
        {
            curTokenIndex += 1;
        }

        private int getPrecedence(char letter)
        {
            if (letter == '*') return 1;
            if (letter == '+') return 2;
            return -1;
        }

        private bool isLeftParens(char letter)
        {
            return letter == '(';
        }

        private bool isRightParens(char letter)
        {
            return letter == ')';
        }

        private bool isBinaryOperator(char letter)
        {
            return letter == '+' || letter =='*';
        }

        private bool isNumber(char letter, out long result)
        {
            return long.TryParse(letter.ToString(), out result);
        }
    }
}
