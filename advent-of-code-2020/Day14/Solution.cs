using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day14
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            decodeData(inputData, new Version1());
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            decodeData(inputData, new Version2());
        }

        protected override string getExample2DatasetFilename() => "example2.txt";

        protected void decodeData(
            IList<string> inputData,
            IDecoder decoderVersion)
        {
            var memory = new Memory();

            Mask mask = null;

            foreach (var instruction in inputData)
            {
                if (instruction.Contains("mask")) mask = new Mask(instruction);

                else
                {
                    decoderVersion.Decode(memory, new WriteInstruction(instruction), mask);
                }
            }

            Console.WriteLine($"The sum of all memory values is {memory.SumOfAllMemoryValues()}");
        }
    }
}
