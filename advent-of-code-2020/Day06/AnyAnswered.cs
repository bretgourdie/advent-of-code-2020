using System.Collections.Generic;

namespace advent_of_code_2020.Day06
{
    class AnyAnswered : IAnswered
    {
        public void Record(
            ISet<char> questionsAnswered,
            string line)
        {
            questionsAnswered.UnionWith(line);
        }

        public ISet<char> Initialize()
        {
            return new HashSet<char>();
        }
    }
}
