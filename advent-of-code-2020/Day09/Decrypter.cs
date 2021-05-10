using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day09
{
    class Decrypter
    {
        private int preambleLength;
        private IList<long> numberStream;

        public Decrypter(
            IList<string> inputData,
            int preambleLength)
        {
            this.preambleLength = preambleLength;
            this.numberStream = convertToNumberList(inputData);
        }

        private IList<long> convertToNumberList(
            IList<string> inputData)
        {
            return inputData.Select(x => long.Parse(x)).ToList();
        }

        public long FindNumberWithoutSum()
        {
            int bufferStart = 0;

            for (int ii = preambleLength; ii < numberStream.Count; ii++)
            {
                long targetNumber = numberStream[ii];

                if (canBeSummedByTwoNumbers(targetNumber, bufferStart))
                {
                    bufferStart += 1;
                }

                else
                {
                    return targetNumber;
                }
            }

            throw new InvalidOperationException("Could not find a weakness for the input.");
        }

        private bool canBeSummedByTwoNumbers(
            long numberToCheck,
            int bufferStart)
        {
            for (int ii = bufferStart; ii < bufferStart + preambleLength; ii++)
            {
                long firstNumber = numberStream[ii];

                if (firstNumber >= numberToCheck)
                {
                    continue;
                }

                for (int jj = ii + 1; jj < bufferStart + preambleLength; jj++)
                {
                    long secondNumber = numberStream[jj];
                    if (firstNumber + secondNumber == numberToCheck)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public long FindWeakness()
        {
            var contiguousNumbers = findContiguousNumbersThatSumToTarget(
                FindNumberWithoutSum()
            );

            return sumOfMinAndMax(contiguousNumbers);
        }

        private IEnumerable<long> findContiguousNumbersThatSumToTarget(
            long weaknessTarget)
        {
            for (int ii = 0; ii < numberStream.Count; ii++)
            {
                bool smallerThanWeaknessTarget = true;
                for (int jj = ii + 1; jj < numberStream.Count && smallerThanWeaknessTarget; jj++)
                {
                    var slice = numberStream.Skip(ii).Take(jj - ii);
                    long sliceSum = slice.Sum();

                    smallerThanWeaknessTarget = sliceSum < weaknessTarget;

                    if (sliceSum == weaknessTarget)
                    {
                        return slice;
                    }
                }
            }

            return Enumerable.Empty<long>();
        }

        private long sumOfMinAndMax(
            IEnumerable<long> contiguousNumbers)
        {
            return contiguousNumbers.Min() + contiguousNumbers.Max();
        }
    }
}
