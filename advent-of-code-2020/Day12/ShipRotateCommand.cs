using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day12
{
    public class ShipRotateCommand : ICommand
    {
        private readonly int degrees;
        private readonly RotationDirection rotationDirection;
        private readonly IList<Direction> directionsClockwise;

        public ShipRotateCommand(
            int degrees,
            RotationDirection rotationDirection,
            IList<Direction> directionsClockwise)
        {
            this.degrees = degrees;
            this.rotationDirection = rotationDirection;
            this.directionsClockwise = directionsClockwise;
        }

        public Transform Resolve(Transform t)
        {
            var directions = directionsClockwise.AsQueryable();
            if (rotationDirection == RotationDirection.CounterClockwise)
            {
                directions = directions.Reverse();
            }

            var theseDirections = directions.ToList();

            var currentIndex = theseDirections.IndexOf(t.Direction);

            var numberOfTurns = degrees / 90;

            var unboundNewIndex = currentIndex + numberOfTurns;

            var boundNewIndex = unboundNewIndex % theseDirections.Count;

            return new Transform(
                t.X,
                t.Y,
                theseDirections[boundNewIndex]);
        }
    }
}
