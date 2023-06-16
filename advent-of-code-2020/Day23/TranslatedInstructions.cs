using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day23
{
    class TranslatedInstructions : ICupInstructions
    {
        public LinkedList<int> makeCups(string cups)
        {
            var circle = new LinkedList<int>();

            foreach (var number in cups)
            {
                circle.AddLast(int.Parse(number.ToString()));
            }

            int numberOfCups = cups.Length;
            int cupToAdd = circle.Max() + 1;

            while (numberOfCups < 1_000_000)
            {
                circle.AddLast(cupToAdd);

                numberOfCups += 1;
                cupToAdd += 1;
            }

            return circle;
        }

        public int numberOfMoves => 10_000_000;

        public string getAnswer(
            LinkedListNode<int> one,
            Func<LinkedListNode<int>, LinkedListNode<int>> getNextCup) =>
            multiplyNextTwoCups(one, getNextCup);


        private string multiplyNextTwoCups(
            LinkedListNode<int> one,
            Func<LinkedListNode<int>, LinkedListNode<int>> getNextCup)
        {
            var cup1 = getNextCup(one);
            var cup2 = getNextCup(cup1);

            return ((long)cup1.Value * (long)cup2.Value).ToString();
        }
    }
}
