using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2020.Day23
{
    class CupCircle
    {
        private readonly LinkedList<int> _circle;
        private LinkedListNode<int> _currentCup;
        private LinkedListNode<int> _destination;
        private readonly IDictionary<int, LinkedListNode<int>> _labelToCup;
        private readonly IList<LinkedListNode<int>> _pickedUpCups = new List<LinkedListNode<int>>(3);
        private int _movesMade;
        private readonly ICupInstructions _instructions;
        private readonly int _min;
        private readonly int _max;

        public CupCircle(ICupInstructions instructions, string cups)
        {
            _instructions = instructions;

            _circle = _instructions.makeCups(cups);

            _labelToCup = makeLookup(_circle);

            _currentCup = _circle.First;

            _min = _circle.Min();
            _max = _circle.Max();
        }

        private IDictionary<int, LinkedListNode<int>> makeLookup(LinkedList<int> circle)
        {
            var lookup = new Dictionary<int, LinkedListNode<int>>();

            var current = circle.First;

            do
            {
                lookup.Add(current.Value, current);
                current = getNextCup(current);
            } while (current != circle.First);

            return lookup;
        }

        public string MakeMoves()
        {
            for (int ii = 0; ii < _instructions.numberOfMoves; ii++)
            {
                Move();
            }

            return _instructions.getAnswer(
                _circle.Find(1),
                getNextCup);
        }


        public void Move()
        {
            pickUpCups();
            _destination = findDestinationCup();
            moveTheCups();

            selectNextCurrentCup();
        }

        private void selectNextCurrentCup()
        {
            _currentCup = getNextCup(_currentCup);
        }

        private void moveTheCups()
        {
            removePickedUpCupsFromCircle();
            insertPickedUpCupsAtDestination();
            _movesMade += 1;
        }

        private void removePickedUpCupsFromCircle()
        {
            foreach (var pickedUpCup in _pickedUpCups)
            {
                _circle.Remove(pickedUpCup);
            }
        }

        private void insertPickedUpCupsAtDestination()
        {
            var theCup = _destination;
            foreach (var pickedUpCup in _pickedUpCups)
            {
                _circle.AddAfter(theCup, pickedUpCup);
                theCup = getNextCup(theCup);
            }
        }

        private LinkedListNode<int> findDestinationCup()
        {
            var adjustedTarget = getTarget(_currentCup.Value - 1);
            return adjustedTarget;
        }

        private LinkedListNode<int> getTarget(int value)
        {
            while (value < _min || _pickedUpCups.Any(c => c.Value == value))
            {
                if (value < _min)
                {
                    value = _max;
                }

                if (_pickedUpCups.Any(c => c.Value == value))
                {
                    value -= 1;
                }
            }

            return _labelToCup[value];
        }

        private void pickUpCups()
        {
            var currentPickedUpCup = getNextCup(_currentCup);

            _pickedUpCups.Clear();
            for (int ii = 0; ii < 3; ii++)
            {
                _pickedUpCups.Add(currentPickedUpCup);
                currentPickedUpCup = getNextCup(currentPickedUpCup);
            }
        }

        private LinkedListNode<int> getNextCup(LinkedListNode<int> fromCup) =>
            fromCup.Next ?? _circle.First;

        public override string ToString()
        {
            var cupRepresentation = new StringBuilder();
            foreach (var cup in _circle)
            {
                if (cup == _currentCup.Value)
                {
                    cupRepresentation.Append($" ({cup})");
                }

                else
                {
                    cupRepresentation.Append($" {cup}");
                }
            }

            var pickedUpCups = String.Join(", ", _pickedUpCups);

            return
                $"-- move {_movesMade + 1} --{Environment.NewLine}"
                + $"cups: {cupRepresentation}{Environment.NewLine}"
                + $"pick up: {pickedUpCups}{Environment.NewLine}"
                + $"destination: {_destination.Value}{Environment.NewLine}";
        }
    }
}
