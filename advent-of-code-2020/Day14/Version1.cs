namespace advent_of_code_2020.Day14
{
    class Version1 : IDecoder
    {
        public void Decode(Memory memory, WriteInstruction writeInstruction, Mask mask)
        {
            var adjustedValue = writeInstruction.Value & mask.AndMask | mask.OrMask;

            memory.Write(writeInstruction.Index, adjustedValue);
        }
    }
}
