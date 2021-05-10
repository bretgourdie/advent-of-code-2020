using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day16
{
    class TicketValidator
    {
        private IList<Validation> validations;

        public TicketValidator(IEnumerable<Validation> validations)
        {
            this.validations = validations.ToList();
        }

        public int GetScanErrorRate(IEnumerable<Ticket> tickets)
        {
            return
                tickets
                    .SelectMany(ticket => getInvalidNumbers(ticket))
                    .Sum();
        }

        private IEnumerable<int> getInvalidNumbers(Ticket ticket)
        {
            foreach (var ticketNumber in ticket.Fields)
            {
                if (!validations.Any(validation => validation.InRange(ticketNumber)))
                {
                    yield return ticketNumber;
                }
            }
        }

        public IEnumerable<Ticket> GetValidTickets(IEnumerable<Ticket> tickets)
        {
            return tickets.Where(ticket => !getInvalidNumbers(ticket).Any());
        }

        public IDictionary<string, int> GetFieldPositions(IEnumerable<Ticket> tickets)
        {
            var validationsWithTicketPositionScore = scoreValidTicketFields(GetValidTickets(tickets));

            return reduceScoresToFieldPositions(validationsWithTicketPositionScore);
        }

        private Dictionary<string, int> reduceScoresToFieldPositions(
            IDictionary<Validation, int[]> validationsWithTicketPositionScore)
        {
            var fieldPositions = new Dictionary<string, int>();

            while (fieldPositions.Count < validations.Count)
            {
                var fieldsToAssign = new List<KeyValuePair<string, int>>();

                var unassignedValidations = validations
                    .Where(validation => !fieldPositions.ContainsKey(validation.FieldName));

                foreach (var unassignedValidation in unassignedValidations)
                {
                    var fieldScores = validationsWithTicketPositionScore[unassignedValidation];
                    var maxScore = fieldScores.Max();

                    var unassignedScoreFields = getUnassignedScoreFields(fieldScores, fieldPositions);

                    var candidates = unassignedScoreFields
                        .Where(field => field.Score == maxScore);

                    if (candidates.Count() == 1)
                    {
                        var candidate = candidates.Single();

                        fieldsToAssign.Add(
                            new KeyValuePair<string, int>(
                                unassignedValidation.FieldName,
                                candidate.Index));
                    }
                }

                foreach (var fieldToAssign in fieldsToAssign)
                {
                    fieldPositions.Add(fieldToAssign.Key, fieldToAssign.Value);
                }
            }

            return fieldPositions;
        }

        private IDictionary<Validation, int[]> scoreValidTicketFields(IEnumerable<Ticket> validTickets)
        {
            var validationsWithTicketPositionScore = new Dictionary<Validation, int[]>();

            foreach (var ticket in validTickets)
            {
                var fields = ticket.Fields;

                foreach (var validation in validations)
                {
                    for (int ii = 0; ii < fields.Length; ii++)
                    {
                        if (validation.InRange(fields[ii]))
                        {
                            if (!validationsWithTicketPositionScore.ContainsKey(validation))
                            {
                                validationsWithTicketPositionScore.Add(
                                    validation,
                                    new int[fields.Length]);
                            }

                            validationsWithTicketPositionScore[validation][ii] += 1;
                        }
                    }
                }
            }

            return validationsWithTicketPositionScore;
        }

        private IEnumerable<UnassignedScoreField> getUnassignedScoreFields(
            int[] scores,
            IDictionary<string, int> assignedFields)
        {
            for (int ii = 0; ii < scores.Length; ii++)
            {
                if (!assignedFields.Values.Contains(ii))
                {
                    yield return new UnassignedScoreField(ii, scores[ii]);
                }
            }
        }
    }
}
