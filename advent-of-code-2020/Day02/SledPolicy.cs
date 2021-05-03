using System.Linq;

namespace advent_of_code_2020.Day02
{
    class SledPolicy : IPolicy
    {
        public bool Passes(
            int min,
            int max,
            char letter,
            string password)
        {
            var letterUsed = password.Count(x => x == letter);

            return min <= letterUsed && letterUsed <= max;
        }
    }
}
