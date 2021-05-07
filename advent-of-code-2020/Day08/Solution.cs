using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day08
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            var accumulator = new ProgramState(inputData).ExecuteInstructions();
            Console.WriteLine($"The accumulator at the loop is {accumulator}");
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            string[] exchangableInstructions = new []{"jmp", "nop"};

            for (int ii = 0; ii < inputData.Count; ii++)
            {
                string line = inputData[ii];

                for (int jj = 0; jj < exchangableInstructions.Length; jj++)
                {
                    string oldInstruction = exchangableInstructions[jj];

                    if (line.Contains(oldInstruction))
                    {
                        var copyOfInstructions = new List<string>(inputData);
                        string newInstruction = exchangableInstructions[(jj + 1) % exchangableInstructions.Length];
                        copyOfInstructions[ii] = line.Replace(oldInstruction, newInstruction);

                        var programState = new ProgramState(copyOfInstructions);
                        var accumulator = programState.ExecuteInstructions();

                        if (programState.IsFinished())
                        {
                            Console.WriteLine($"The accumulator at the finish is {accumulator}");
                            return;
                        }
                    }
                }
            }
        }
    }
}
