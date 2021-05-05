using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day06
{
    class AllAnswered : IAnswered
    {
        public void Record(
            ISet<char> questionsAnswered,
            string line)
        {
            questionsAnswered.IntersectWith(line);
        }

        public ISet<char> Initialize()
        {
            var alphabet = Enumerable
                .Range('a', 26)
                .Select(x => (char) x);

            return new HashSet<char>(alphabet);
        }
    }
}
