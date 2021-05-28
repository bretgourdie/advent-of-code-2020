using System.Collections.Generic;

namespace advent_of_code_2020.Day20
{
    class Edge
    {
        public readonly IList<char> Contents;
        public readonly Reflection Reflection;

        public Edge(IList<char> side, Reflection reflection)
        {
            Contents = side;
            Reflection = reflection;
        }
    }
}
