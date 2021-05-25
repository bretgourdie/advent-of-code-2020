namespace advent_of_code_2020.Day14
{
    class WriteInstruction
    {
        public readonly long Value;
        public readonly long Index;

        public WriteInstruction(string line)
        {
            var split = line.Split('=');

            Index = long.Parse(split[0].Replace("mem[", "").Replace("]", ""));
            Value = long.Parse(split[1]);
        }
    }
}
