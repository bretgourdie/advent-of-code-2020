using System;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_2020.Day23
{
    class UntranslatedInstructions : ICupInstructions
    {
        public LinkedList<int> makeCups(string cups)
        {
            var circle = new LinkedList<int>();

            foreach (var number in cups)
            {
                circle.AddLast(int.Parse(number.ToString()));
            }

            return circle;
        }

        public int numberOfMoves => 100;

        public string getAnswer(
            LinkedListNode<int> one,
            Func<LinkedListNode<int>, LinkedListNode<int>> getNextCup) =>
                numbersAfterOne(one, getNextCup);

        private string numbersAfterOne(
            LinkedListNode<int> one,
            Func<LinkedListNode<int>, LinkedListNode<int>> getNextCup)
        {
            var node = getNextCup(one);
            var numbersAfterOne = new StringBuilder();

            while (node.Value != 1)
            {
                numbersAfterOne.Append(node.Value.ToString());
                node = getNextCup(node);
            }

            return numbersAfterOne.ToString();
        }
    }
}
