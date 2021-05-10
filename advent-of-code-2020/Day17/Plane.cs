using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day17
{
    class Plane<Point> where Point : struct
    {
        public int CycleCount { get; private set; }

        private readonly ISet<Point> activeCubes;
        private const char active = '#';
        private readonly INavigationStrategy<Point> navigationStrategy;

        private const int minDeactivateCubes = 2;
        private const int maxDeactivateCubes = 3;

        private const int activeNeighborsForActivation = 3;

        public Plane(
            IList<string> inputData,
            INavigationStrategy<Point> navigationStrategy)
        {
            this.navigationStrategy = navigationStrategy;

            activeCubes = new HashSet<Point>(loadPlane(inputData));
        }

        private IEnumerable<Point> loadPlane(
            IList<string> inputData)
        {
            for (int x = 0; x < inputData.Count; x++)
            {
                string line = inputData[x];

                for (int y = 0; y < line.Length; y++)
                {
                    if (inputData[x][y] == active)
                    {
                        yield return navigationStrategy.LoadCube(x, y);
                    }
                }
            }
        }

        public int CountActive()
        {
            return activeCubes.Count();
        }

        public void Cycle()
        {
            var toActivate = new List<Point>();
            var toDeactivate = new List<Point>();

            foreach (Point possibleCube in navigationStrategy.GetPossibleCubes(activeCubes))
            {
                int activeNeighbors = getActiveNeighboringCubes(
                    activeCubes,
                    possibleCube);

                if (isActive(possibleCube))
                {
                    if (!(minDeactivateCubes <= activeNeighbors && activeNeighbors <= maxDeactivateCubes))
                    {
                        toDeactivate.Add(possibleCube);
                    }
                }

                else
                {
                    if (activeNeighbors == activeNeighborsForActivation)
                    {
                        toActivate.Add(possibleCube);
                    }
                }
            }

            markActive(toActivate);
            markInactive(toDeactivate);

            CycleCount += 1;
        }

        public string GetDimensions()
        {
            return navigationStrategy.ListDimensions();
        }

        private int getActiveNeighboringCubes(
            IEnumerable<Point> activeCubes,
            Point cube)
        {
            int activeNeighbors = 0;

            foreach (var testingCube in navigationStrategy.GetNeighbors(cube))
            {
                if (activeCubes.Contains(testingCube))
                {
                    activeNeighbors += 1;

                    if (activeNeighbors > activeNeighborsForActivation)
                    {
                        return activeNeighbors;
                    }
                }
            }

            return activeNeighbors;
        }

        private bool isActive(Point cube)
        {
            return activeCubes.Contains(cube);
        }

        private void markActive(IList<Point> cubes)
        {
            foreach (var cube in cubes)
            {
                activeCubes.Add(cube);
            }
        }

        private void markInactive(IList<Point> cubes)
        {
            foreach (var cube in cubes)
            {
                activeCubes.Remove(cube);
            }
        }
    }
}
