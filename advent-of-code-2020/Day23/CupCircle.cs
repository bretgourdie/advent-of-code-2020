using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2020.Day23
{
    class CupCircle
    {
        private LinkedList<int> _circle;
        private LinkedListNode<int> _currentCup;
        private LinkedListNode<int> _destination;
        private IList<int> _pickedUpCups;
        private int _movesMade;
        private readonly ICupInstructions _instructions;

        private List<State> allStates = new List<State>();

        public CupCircle(ICupInstructions instructions, string cups)
        {
            _instructions = instructions;

            _circle = _instructions.makeCups(cups);

            _currentCup = _circle.First;
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
            _pickedUpCups = pickUpCups();
            _destination = findDestinationCup();
            printStatus();
            moveTheCups();
            checkStatus();

            selectNextCurrentCup();
        }

        private void checkStatus()
        {
            var state = new State(_circle, _pickedUpCups, _destination, _currentCup);

            foreach (var pastState in allStates)
            {
                if (state.Matches(pastState))
                {
                    Console.WriteLine("Found cycle... ");
                    var index = allStates.IndexOf(pastState);
                    var cycle = _movesMade - index;
                    while (_movesMade + cycle < _instructions.numberOfMoves)
                    {
                        _movesMade += cycle;
                    }

                    Console.WriteLine($"Skipped to move {_movesMade}");
                }
            }

            allStates.Add(state);
        }

        private void selectNextCurrentCup()
        {
            _currentCup = getNextCup(_currentCup);
        }

        private void printStatus()
        {
            if ((_movesMade + 1) % 100 == 0)
                Console.WriteLine($" -- move {_movesMade + 1} --");
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
            while (value < _circle.Min() || _pickedUpCups.Contains(value))
            {
                if (value < _circle.Min())
                {
                    value = _circle.Max();
                }

                if (_pickedUpCups.Contains(value))
                {
                    value -= 1;
                }
            }

            return _circle.Find(value);
        }

        private IList<int> pickUpCups()
        {
            var currentPickedUpCup = getNextCup(_currentCup);
            var pickedUpCups = new List<int>();
            for (int ii = 0; ii < 3; ii++)
            {
                pickedUpCups.Add(currentPickedUpCup.Value);
                currentPickedUpCup = getNextCup(currentPickedUpCup);
            }

            return pickedUpCups;
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
