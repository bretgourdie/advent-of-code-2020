using System.Collections.Generic;

namespace advent_of_code_2020.Day15
{
    class SpokenNumber
    {
        private readonly Stack<int> spokenOn;
        public readonly int Number;

        public SpokenNumber(
            int number)
        {
            spokenOn = new Stack<int>();
            Number = number;
        }

        public void RecordSpoken(int round)
        {
            spokenOn.Push(round);
        }

        public int GetNextSpokenNumber()
        {
            if (spokenOn.Count <= 1)
            {
                return 0;
            }

            else
            {
                var mostRecentRoundSpoken = spokenOn.Pop();
                var secondMostRecentRoundSpoken = spokenOn.Pop();

                spokenOn.Push(secondMostRecentRoundSpoken);
                spokenOn.Push(mostRecentRoundSpoken);

                return mostRecentRoundSpoken - secondMostRecentRoundSpoken;
            }
        }

        public override string ToString()
        {
            return $"{Number}: Spoken {spokenOn.Peek()}";
        }
    }
}
