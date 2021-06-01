using System;

namespace advent_of_code_2020.Day11
{
    class ImmediatelyAdjacent : IOccupiedSeatCounter
    {
        public int GetAdjacentOccupiedSeats(Point2D testingSeat, SeatStatus[,] seats)
        {
            int occupiedAdjacentSeats = 0;

            int xLength = seats.GetLength(0);
            int yLength = seats.GetLength(1);

            for (int x = getStart(testingSeat.X); x <= getEnd(testingSeat.X, xLength); x++)
            {
                for (int y = getStart(testingSeat.Y); y <= getEnd(testingSeat.Y, yLength); y++)
                {
                    if (!(x == testingSeat.X && y == testingSeat.Y))
                    {
                        if (seats[x, y] == SeatStatus.Occupied)
                        {
                            occupiedAdjacentSeats += 1;
                        }
                    }
                }
            }

            return occupiedAdjacentSeats;
        }

        private int getStart(int number)
        {
            return Math.Max(number - 1, 0);
        }

        private int getEnd(int number, int length)
        {
            return Math.Min(number + 1, length - 1);
        }

        public int EmptyThreshold => 4;
    }
}
