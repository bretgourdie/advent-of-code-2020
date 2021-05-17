using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day15
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            performMemoryGame(inputData, 2020);
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            performMemoryGame(inputData, 30000000);
        }

        private void performMemoryGame(IList<string> startingNumbers, int totalNumbersToSpeak)
        {
            var numberInMemory = new Dictionary<int, SpokenNumber>();
            int numberLastSpoken = 0;

            var splitInput = startingNumbers.Single().Split(',');

            int ii;
            for (ii = 1; ii <= splitInput.Length; ii++)
            {
                numberLastSpoken = speakStartingNumber(
                    numberInMemory,
                    int.Parse(splitInput[ii - 1]),
                    ii);
            }

            for (; ii <= totalNumbersToSpeak; ii++)
            {
                numberLastSpoken = speakNumber(
                    numberInMemory,
                    numberLastSpoken,
                    ii);
            }

            Console.WriteLine($"Number last spoken is {numberLastSpoken}");
        }

        private int speakStartingNumber(
            IDictionary<int, SpokenNumber> numberInMemory,
            int number,
            int round)
        {
            numberInMemory[number] = new SpokenNumber(number);
            numberInMemory[number].RecordSpoken(round);
            return number;
        }

        private int speakNumber(
            IDictionary<int, SpokenNumber> numberInMemory,
            int numberFromLastRound,
            int round)
        {
            var lastRoundSpokenNumber = numberInMemory[numberFromLastRound];

            var currentNumber = lastRoundSpokenNumber.GetNextSpokenNumber();

            if (!numberInMemory.ContainsKey(currentNumber))
            {
                numberInMemory[currentNumber] = new SpokenNumber(currentNumber);
            }

            numberInMemory[currentNumber].RecordSpoken(round);

            return currentNumber;
        }
    }
}
