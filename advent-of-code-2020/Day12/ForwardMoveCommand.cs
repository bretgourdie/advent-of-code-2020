namespace advent_of_code_2020.Day12
{
    public class ForwardMoveCommand : ICommand
    {
        private readonly MovementCommand movementCommand;
        private readonly int times;

        public ForwardMoveCommand(
            int amount,
            Direction direction)
        {
            movementCommand = new MovementCommand(amount, direction);
            times = 1;
        }

        public ForwardMoveCommand(
            Transform waypoint,
            int times)
        {
            movementCommand = new MovementCommand(waypoint);
            this.times = times;
        }

        public Transform Resolve(Transform ship)
        {
            for (int ii = 0; ii < times; ii++)
            {
                ship = movementCommand.Resolve(ship);
            }

            return ship;
        }
    }
}
