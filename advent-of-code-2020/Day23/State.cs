using System.Collections.Generic;

namespace advent_of_code_2020.Day23
{
    class State
    {
        //public readonly LinkedList<int> Circle;
        public readonly IList<LinkedListNode<int>> PickedUpCups;
        public readonly int Destination;
        public readonly int CurrentCup;

        public State(
            LinkedList<int> circle,
            IList<LinkedListNode<int>> pickedUpCups,
            LinkedListNode<int> destination,
            LinkedListNode<int> currentCup)
        {
            //Circle = new LinkedList<int>(circle);
            PickedUpCups = new List<LinkedListNode<int>>(pickedUpCups);
            Destination = destination.Value;
            CurrentCup = currentCup.Value;
        }

        public bool Matches(State other)
        {
            if (Destination != other.Destination)
                return false;

            if (CurrentCup != other.CurrentCup)
                return false;

            if (!listsMatch(PickedUpCups, other.PickedUpCups))
                return false;

            //return circlesMatch(Circle, other.Circle);
            return true;
        }

        private bool listsMatch(
            IList<LinkedListNode<int>> a,
            IList<LinkedListNode<int>> b)
        {
            for (int ii = 0; ii < a.Count; ii++)
            {
                if (a[ii].Value != b[ii].Value)
                    return false;
            }

            return true;
        }

        private bool circlesMatch(
            LinkedList<int> a,
            LinkedList<int> b)
        {
            var count = a.Count;
            var aNode = a.First;
            var bNode = b.Find(aNode.Value);

            for (int ii = 0; ii < count; ii++)
            {
                if (aNode.Value != bNode.Value)
                    return false;

                aNode = getNextCup(a, aNode);
                bNode = getNextCup(b, bNode);
            }

            return true;
        }

        private LinkedListNode<int> getNextCup(
            LinkedList<int> circle,
            LinkedListNode<int> cup)
        {
            return cup.Next ?? circle.First;
        }
    }
}
