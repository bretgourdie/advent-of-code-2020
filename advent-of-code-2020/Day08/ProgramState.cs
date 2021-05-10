using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day08
{
    class ProgramState
    {
        private int accumulator = 0;
        private int currentInstruction = 0;

        private IList<string> instructions;
        private ISet<int> executedInstructions;
        private IDictionary<string, Action<int>> instructionToMethod;

        public ProgramState(IList<string> instructions)
        {
            this.instructions = instructions;
            this.executedInstructions = new SortedSet<int>();

            this.instructionToMethod = new Dictionary<string, Action<int>>();
            this.instructionToMethod.Add("acc", accumulate);
            this.instructionToMethod.Add("jmp", jump);
            this.instructionToMethod.Add("nop", noop);
        }

        public int ExecuteInstructions()
        {
            while (!isStuck() && !IsFinished())
            {
                executeInstruction(instructions[currentInstruction]);
            }

            return accumulator;
        }

        private bool isStuck()
        {
            return executedInstructions.Contains(currentInstruction);
        }

        public bool IsFinished()
        {
            return currentInstruction >= instructions.Count;
        }

        private void executeInstruction(string line)
        {
            executedInstructions.Add(currentInstruction);

            var instructionAndArgument = line.Split(' ');
            instructionToMethod[instructionAndArgument[0]].Invoke(int.Parse(instructionAndArgument[1]));
        }

        private void accumulate(int argument)
        {
            accumulator += argument;
            currentInstruction += 1;
        }

        private void jump(int argument)
        {
            currentInstruction += argument;
        }

        private void noop(int argument)
        {
            currentInstruction += 1;
        }
    }
}
