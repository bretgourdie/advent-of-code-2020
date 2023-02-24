using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day23
{
    interface ICupInstructions
    {
        LinkedList<int> makeCups(string cups);
        int numberOfMoves { get; }
        string getAnswer(
            LinkedListNode<int> one,
            Func<LinkedListNode<int>, LinkedListNode<int>> getNextCup);
    }
}
