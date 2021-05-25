namespace advent_of_code_2020.Day14
{
    interface IDecoder
    {
        void Decode(Memory memory, WriteInstruction writeInstruction, Mask mask);
    }
}
