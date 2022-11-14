using System;

namespace advent_of_code_2020.Day12
{
    public class WaypointRotateCommand : ICommand
    {
        private readonly int degrees;
        private readonly RotationDirection rotationDirection;

        public WaypointRotateCommand(
            int degrees,
            RotationDirection rotationDirection)
        {
            this.degrees = degrees;
            this.rotationDirection = rotationDirection;
        }

        public Transform Resolve(Transform t)
        {
            int x = t.X;
            int y = t.Y;

            switch (rotationDirection)
            {
                case RotationDirection.Clockwise:
                    rotate(ref x, ref y);
                    break;
                case RotationDirection.CounterClockwise:
                    rotate(ref y, ref x);
                    break;
                default:
                    throw new NotImplementedException();
            }
            
            return new Transform(
                x,
                y,
                t.Direction);
        }

        private void rotate(
            ref int left,
            ref int right)
        {
            int times = degrees / 90;

            for (int ii = 0; ii < times; ii++)
            {
                int temp = left;
                left = right;
                right = temp * -1;
            }
        }
    }
}
