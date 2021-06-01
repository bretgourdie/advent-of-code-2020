namespace advent_of_code_2020.Day11
{
    interface IOccupiedSeatCounter
    {
        int GetAdjacentOccupiedSeats(
            Point2D testingSeat,
            SeatStatus[,] seats);

        int EmptyThreshold { get; }
    }
}
