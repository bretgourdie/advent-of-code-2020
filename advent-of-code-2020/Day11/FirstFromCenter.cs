using System.Collections.Generic;

namespace advent_of_code_2020.Day11
{
    class FirstFromCenter : IOccupiedSeatCounter
    {
        private readonly IList<Point2D> directionVectors;

        public FirstFromCenter()
        {
            directionVectors = loadDirectionVectors();
        }

        private IList<Point2D> loadDirectionVectors()
        {
            var directions = new List<Point2D>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x != 0 || y != 0)
                    {
                        directions.Add(new Point2D(x, y));
                    }
                }
            }

            return directions;
        }

        public int GetAdjacentOccupiedSeats(Point2D center, SeatStatus[,] seats)
        {
            int occupiedSeats = 0;
            int xLength = seats.GetLength(0);
            int yLength = seats.GetLength(1);

            foreach (var vector in directionVectors)
            {
                bool seatFound = false;
                int x = center.X + vector.X;
                int y = center.Y + vector.Y;

                while (inBounds(x, xLength) && inBounds(y, yLength) && !seatFound)
                {
                    var testingSeat = seats[x, y];

                    if (testingSeat == SeatStatus.Occupied)
                    {
                        occupiedSeats += 1;
                    }

                    seatFound = testingSeat == SeatStatus.Occupied || testingSeat == SeatStatus.Empty;
                    x += vector.X;
                    y += vector.Y;
                }
            }

            return occupiedSeats;
        }

        private bool inBounds(int number, int length)
        {
            return 0 <= number && number < length;
        }

        public int EmptyThreshold => 5;
    }
}
