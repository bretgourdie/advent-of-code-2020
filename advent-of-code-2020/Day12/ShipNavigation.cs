using System.Collections.Generic;

namespace advent_of_code_2020.Day12
{
    public class ShipNavigation : NavigationStrategy
    {
        private Transform ship;

        private readonly IList<Direction> rotationsClockwise = new List<Direction>()
        {
            Direction.North,
            Direction.East,
            Direction.South,
            Direction.West
        };

        public ShipNavigation(Transform ship)
        {
            this.ship = ship;
        }

        protected override void navigate(ICommand command)
        {
            ship = command.Resolve(ship);
        }

        protected override ICommand parseRotation(int degrees, RotationDirection rotationDirection)
        {
            return new ShipRotateCommand(degrees, rotationDirection, rotationsClockwise);
        }

        protected override ICommand parseForwardMove(int amount)
        {
            return new MovementCommand(amount, ship.Direction);
        }

        public override int GetManhattanDistance()
        {
            return getManhattanDistance(ship);
        }
    }
}
