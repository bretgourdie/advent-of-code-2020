using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2020.Day23
{
    class CupCircle
    {
        private int[] _cups;
        private int _currentCupIndex;
        private int[] _pickedUpCups = new int[3];
        private int _destination;
        private int _moves;

        public CupCircle(string cups)
        {
            _cups = new int[cups.Length];

            for (int ii = 0; ii < cups.Length; ii++)
            {
                _cups[ii] = int.Parse(cups[ii].ToString());
            }

            _currentCupIndex = 0;
        }

        public void PerformMoves()
        {
            for (int ii = 0; ii < 10; ii++)
            {
                Move();
                Console.WriteLine(ToString());
            }
        }

        private void Move()
        {
            _destination = -1;

            for (int ii = 0; ii < 3; ii++)
            {
                var circleIndex = (_currentCupIndex + 1 + ii) % _cups.Length;
                _pickedUpCups[ii] = _cups[circleIndex];
            }

            var target = _cups[_currentCupIndex] - 1;

            _destination = getAdjustedTarget(target);

            var destinationIndex = getDestinationCupIndex(_destination);

        }

        private int getAdjustedTarget(int target)
        {
            while (targetInPickedUpCups(target) || targetBelowPossibleValue(target))
            {
                if (targetInPickedUpCups(target))
                {
                    target -= 1;
                }

                if (targetBelowPossibleValue(target))
                {
                    target = _cups.Max();
                }
            }

            return target;
        }

        private bool targetInPickedUpCups(int target) => _pickedUpCups.Contains((target));

        private bool targetBelowPossibleValue(int target) => target < _cups.Min();

        private int getDestinationCupIndex(int destination)
        {
            for (int ii = 0; ii < _cups.Length; ii++)
            {
                if (_cups[ii] == destination)
                    return ii;
            }

            throw new IndexOutOfRangeException();
        }

        public override string ToString()
        {
            var cupRepresentation = new StringBuilder();
            for (int ii = 0; ii < _cups.Length; ii++)
            {
                if (ii == _currentCupIndex)
                {
                    cupRepresentation.Append($" ({ii})");
                }

                else
                {
                    cupRepresentation.Append(ii);
                }
            }

            var pickedUpCups = String.Join(", ", _pickedUpCups);

            return
                $"-- move {_moves} --{Environment.NewLine}"
                + $"cups: {cupRepresentation}{Environment.NewLine}"
                + $"pick up: {pickedUpCups}{Environment.NewLine}"
                + $"destination: {_destination}{Environment.NewLine}";
        }
    }
}
