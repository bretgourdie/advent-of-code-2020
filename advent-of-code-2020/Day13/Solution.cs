using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day13
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            var earliestDeparture = getEarliestDeparture(inputData);

            var buses = getBuses(inputData);

            Console.WriteLine("Earliest bus multiplied by the number of minutes to wait is "
            + getEarliestBusTimesMinutesWaiting(earliestDeparture, buses));
        }

        private int getEarliestDeparture(IEnumerable<string> inputData)
        {
            return int.Parse(inputData.First());
        }

        private IList<int> getBuses(IEnumerable<string> inputData)
        {
            var buses = new List<int>();

            var busSchedule =
                inputData.Skip(1)
                    .First()
                    .Split(',');

            foreach (var bus in busSchedule)
            {
                if (int.TryParse(bus, out int id))
                {
                    buses.Add(id);
                }
            }

            return buses;
        }

        private int getEarliestBusTimesMinutesWaiting(
            int myEarliestDeparture,
            IList<int> buses)
        {
            var earliestBusAndDeparture = new BusAndDeparture(-1, int.MaxValue);

            foreach (var bus in buses)
            {
                var compare = new BusAndDeparture(bus, getFirstDeparture(myEarliestDeparture, bus));

                if (compare.FirstDeparture < earliestBusAndDeparture.FirstDeparture)
                {
                    earliestBusAndDeparture = compare;
                }
            }

            var minutesWaited = earliestBusAndDeparture.FirstDeparture - myEarliestDeparture;

            return minutesWaited * earliestBusAndDeparture.Id;
        }

        private long leastCommonMultiple(long a, long b)
        {
            return Math.Abs(a * b) / greatestCommonDivisor(a, b);
        }

        private long greatestCommonDivisor(long a, long b)
        {
            long t;

            while (b != 0)
            {
                t = b;
                b = a % b;
                a = t;
            }

            return a;
        }

        private int getFirstDeparture(
            int earliestDeparture,
            int bus)
        {
            return earliestDeparture + (bus - earliestDeparture % bus);
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            var busDepartures = loadBusDepartures(inputData);
            var recalculateStep = loadRecalculateStepArray(busDepartures);

            long stepAmount = 1;

            for (long timeStamp = 0; timeStamp < long.MaxValue; timeStamp += stepAmount)
            {
                int continuousBusDepartures = 0;

                for (int busIndex = 0; busIndex < busDepartures.Length && busIndex == continuousBusDepartures; busIndex++)
                {
                    var busDeparture = busDepartures[busIndex];

                    if (busDeparture.TimeStampWorks(timeStamp, busIndex))
                    {
                        continuousBusDepartures += 1;

                        if (recalculateStep[busIndex])
                        {
                            stepAmount = leastCommonMultiple(stepAmount, busDeparture.Id);
                            recalculateStep[busIndex] = false;
                        }
                    }
                }

                if (continuousBusDepartures == busDepartures.Length)
                {
                    Console.WriteLine($"All listed bus IDs depart at offsets matching their positions at {timeStamp}");
                    return;
                }
            }
        }

        private bool[] loadRecalculateStepArray(BusDeparture[] busDepartures)
        {
            var recalcSteps = new bool[busDepartures.Length];

            for (int ii = 0; ii < busDepartures.Length; ii++)
            {
                recalcSteps[ii] = !(busDepartures[ii] is FlexibleBusDeparture);
            }

            return recalcSteps;
        }

        private BusDeparture[] loadBusDepartures(IList<string> inputData)
        {
            var departures = inputData.Skip(1).First();

            var splitDepartures = departures.Split(',');

            var busDepartures = new BusDeparture[splitDepartures.Length];

            for (int ii = 0; ii < splitDepartures.Length; ii++)
            {
                var departure = splitDepartures[ii];
                if (int.TryParse(departure, out int busId))
                {
                    busDepartures[ii] = new BusDeparture(busId);
                }

                else
                {
                    busDepartures[ii] = new FlexibleBusDeparture();
                }
            }

            return busDepartures;
        }
    }
}
