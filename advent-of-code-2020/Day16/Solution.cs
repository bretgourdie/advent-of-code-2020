using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day16
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            int sumOfAllInvalidTicketNumbers = 
                new TicketValidator(getValidations(inputData))
                    .GetScanErrorRate(getNearbyTickets(inputData));

            Console.WriteLine(
                $"The sum of all invalid ticket numbers is {sumOfAllInvalidTicketNumbers}");
        }

        protected override void performWorkForExample2(IList<string> inputData)
        {
            getProductOfTargetFields(inputData, "row");
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            getProductOfTargetFields(inputData, "departure");
        }

        private void getProductOfTargetFields(
            IList<string> inputData,
            string targetPrefix)
        {
            var ticketValidator = new TicketValidator(getValidations(inputData));

            var ticketNameToPosition = ticketValidator
                .GetFieldPositions(getNearbyTickets(inputData));

            var ticketNumbers = getYourTicket(inputData).Fields;

            var targetFieldProduct = ticketNameToPosition
                .Keys.Where(x => x.StartsWith(targetPrefix))
                .Select(targetLabel => (long)ticketNumbers[ticketNameToPosition[targetLabel]])
                .Aggregate((x, y) => x * y);

            Console.WriteLine(
                $"The product of fields beginning with \"{targetPrefix}\" is {targetFieldProduct}");
        }

        private IEnumerable<Validation> getValidations(
            IList<string> inputData)
        {
            int ii = 0;
            while (inputData[ii] != String.Empty)
            {
                yield return new Validation(inputData[ii++]);
            }
        }

        private Ticket getYourTicket(
            IList<string> inputData)
        {
            int ii = 0;
            while (inputData[ii] != "your ticket:")
            {
                ii += 1;
            }

            return new Ticket(inputData[ii + 1]);
        }

        private IEnumerable<Ticket> getNearbyTickets(
            IList<string> inputData)
        {
            int ii = 0;
            while (inputData[ii] != "nearby tickets:")
            {
                ii += 1;
            }

            for (ii = ii + 1; ii < inputData.Count; ii++)
            {
                yield return new Ticket(inputData[ii]);
            }
        }
    }
}
