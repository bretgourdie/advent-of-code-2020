using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day14
{
    class Memory
    {
        public readonly IDictionary<long, long> buffer;

        public int Length => buffer.Count;

        public Memory()
        {
            buffer = new Dictionary<long, long>();
        }

        public void Write(long index, long value)
        {
            buffer[index] = value;
        }

        public long SumOfAllMemoryValues()
        {
            return buffer.Sum(x => x.Value);
        }
    }
}
