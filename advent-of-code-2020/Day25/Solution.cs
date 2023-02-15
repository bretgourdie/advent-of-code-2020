using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day25
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            var encryptionKey = new Encryption().TransformFromSubject(
                cardPublicKey: long.Parse(inputData.First()),
                doorPublicKey: long.Parse(inputData.Skip(1).First())
            );

            Console.WriteLine($"The encryption key established by the handshake is {encryptionKey}");
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            throw new NotImplementedException();
        }
    }
}
