using System;
using System.IO;

namespace advent_of_code_2020
{
    class Program
    {
        static void Main(string[] args)
        {
            trySolveAProblem();

            PauseForExit();
        }

        static void trySolveAProblem()
        {
            try
            {
                solveAProblem();
            }

            catch (IOException)
            {
                Console.WriteLine("***** Cannot find input file; skipping to end.");
            }

            catch (NotImplementedException)
            {
                Console.WriteLine("***** Missing method implementation; skipping to end.");
            }
        }

        static void solveAProblem()
        {
            // Put solution invocation here
        }

        static void PauseForExit()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
