using System.Linq;

namespace advent_of_code_2020.Day16
{
    class Ticket
    {
        public readonly int[] Fields;

        public Ticket(string line)
        {
            Fields = line
                .Split(',')
                .Select(x => int.Parse(x))
                .ToArray();
        }
    }
}
