using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2020.Day11
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            settleSeats(inputData, new ImmediatelyAdjacent());
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            settleSeats(inputData, new FirstFromCenter());
        }

        private void settleSeats(IList<string> inputData, IOccupiedSeatCounter occupiedSeatCounter)
        {
            var seats = loadSeatArray(inputData);
            IList<Point2D> toOccupy;
            IList<Point2D> toEmpty;

            do
            {
                var state = getState(seats);

                toOccupy = new List<Point2D>();
                toEmpty = new List<Point2D>();

                for (int x = 0; x < seats.GetLength(0); x++)
                {
                    for (int y = 0; y < seats.GetLength(1); y++)
                    {
                        if (shouldOccupy(x, y, seats, occupiedSeatCounter))
                        {
                            toOccupy.Add(new Point2D(x, y));
                        }

                        else if (shouldEmpty(x, y, seats, occupiedSeatCounter))
                        {
                            toEmpty.Add(new Point2D(x, y));
                        }
                    }
                }

                foreach (var point in toOccupy)
                {
                    seats[point.X, point.Y] = SeatStatus.Occupied;
                }

                foreach (var point in toEmpty)
                {
                    seats[point.X, point.Y] = SeatStatus.Empty;
                }

            } while (!isStablized(toOccupy, toEmpty));

            int occupiedSeats = getTotalOccupiedSeats(seats);
            Console.WriteLine($"The number of occupied seats is {occupiedSeats}");
        }

        private string getState(SeatStatus[,] seats)
        {
            StringBuilder sb = new StringBuilder();

            for (int x = 0; x < seats.GetLength(0); x++)
            {
                for (int y = 0; y < seats.GetLength(1); y++)
                {
                    sb.Append((char)seats[x, y]);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        private bool isStablized(
            IEnumerable<Point2D> toOccupy,
            IEnumerable<Point2D> toEmpty)
        {
            return !toOccupy.Any() && !toEmpty.Any();
        }

        private bool shouldOccupy(
            int x,
            int y,
            SeatStatus[,] seats,
            IOccupiedSeatCounter occupiedSeatCounter)
        {
            if (seats[x, y] != SeatStatus.Empty) return false;

            return occupiedSeatCounter.GetAdjacentOccupiedSeats(
                new Point2D(x, y),
                seats) == 0;
        }

        private bool shouldEmpty(
            int x,
            int y,
            SeatStatus[,] seats,
            IOccupiedSeatCounter occupiedSeatCounter)
        {
            if (seats[x, y] != SeatStatus.Occupied) return false;

            return occupiedSeatCounter.GetAdjacentOccupiedSeats(
                new Point2D(x, y),
                seats) >= occupiedSeatCounter.EmptyThreshold;
        }

        private int getTotalOccupiedSeats(SeatStatus[,] seats)
        {
            int occupiedSeats = 0;

            for (int x = 0; x < seats.GetLength(0); x++)
            {
                for (int y = 0; y < seats.GetLength(1); y++)
                {
                    if (seats[x, y] == SeatStatus.Occupied)
                    {
                        occupiedSeats += 1;
                    }
                }
            }

            return occupiedSeats;
        }

        private SeatStatus[,] loadSeatArray(IList<string> inputData)
        {
            var length = inputData.Count;
            var width = inputData.First().Length;
            var seats = new SeatStatus[length, width];

            for (int x = 0; x < inputData.Count; x++)
            {
                for (int y = 0; y < inputData[x].Length; y++)
                {
                    seats[x, y] = (SeatStatus) inputData[x][y];
                }
            }

            return seats;
        }
    }
}
