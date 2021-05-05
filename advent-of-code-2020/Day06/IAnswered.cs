using System.Collections.Generic;

namespace advent_of_code_2020.Day06
{
    interface IAnswered
    {
        void Record(ISet<char> questionsAnswered, string line);
        ISet<char> Initialize();
    }
}
