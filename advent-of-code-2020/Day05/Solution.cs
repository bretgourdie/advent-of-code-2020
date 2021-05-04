using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2020.Day05
{
    class Solution : AdventSolution<string>
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            int greatestSeatId = 0;

            foreach (var line in inputData)
            {
                var seatId = getSeatId(line);

                greatestSeatId = Math.Max(greatestSeatId, seatId);
            }

            Console.WriteLine($"The largest seat id is {greatestSeatId}");
        }

        private int getSeatId(string line)
        {
            int row = determineRow(line.Substring(0, 7));
            int column = determineColumn(line.Substring(7));

            int seatId = row * 8 + column;

            return seatId;
        }

        private int determineRow(string first7)
        {
            return determineNumber(
                first7,
                'B',
                'F',
                127,
                0);
        }

        private int determineColumn(string last3)
        {
            return determineNumber(
                last3,
                'R',
                'L',
                7,
                0);
        }

        private int determineNumber(
            string instructionLine,
            char takeUpper,
            char takeLower,
            int upperBound,
            int lowerBound)
        {
            char currentInstruction = instructionLine[0];

            if (instructionLine.Length == 1)
            {
                if (currentInstruction == takeUpper)
                {
                    return upperBound;
                }

                if (currentInstruction == takeLower)
                {
                    return lowerBound;
                }
            }

            int range = upperBound - lowerBound;
            float delta = range / 2f;

            if (currentInstruction == takeUpper)
            {
                lowerBound = (int) Math.Ceiling(lowerBound + delta);
            }

            if (currentInstruction == takeLower)
            {
                upperBound = (int) Math.Floor(upperBound - delta);
            }

            return
                determineNumber(
                    instructionLine.Substring(1),
                    takeUpper,
                    takeLower,
                    upperBound,
                    lowerBound);
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            var seats = new List<int>();

            foreach (var line in inputData)
            {
                seats.Add(getSeatId(line));
            }

            seats.Sort();

            int prevSeat = seats[0];

            for (int ii = 1; ii < seats.Count; ii++, prevSeat = seats[ii - 1])
            {

                if (prevSeat != seats[ii] - 1)
                {
                    Console.WriteLine($"Missing seat {seats[ii] - 1}");
                }
            }
        }
    }
}
