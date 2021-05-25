using System;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_2020.Day14
{
    class Version2 : IDecoder
    {
        public void Decode(Memory memory, WriteInstruction writeInstruction, Mask mask)
        {
            var maskedAddress = maskAddress(mask.MaskPart, writeInstruction.Index);

            foreach (var permutedAddress in enumerateFloatingBits(maskedAddress))
            {
                var longAddress = Convert.ToInt64(permutedAddress, 2);
                memory.Write(longAddress, writeInstruction.Value);
            }
        }

        private string maskAddress(
            string mask,
            long address)
        {
            var totalLength = mask.Length;
            string bAddress = Convert.ToString(address, 2);
            bAddress = bAddress.PadLeft(totalLength, '0');

            StringBuilder newAddress = new StringBuilder();

            for (int ii = 0; ii < totalLength; ii++)
            {
                var bit = mask[ii];

                if (bit == '1' || bit == 'X')
                {
                    newAddress.Append(bit);
                }

                else
                {
                    newAddress.Append(bAddress[ii]);
                }
            }

            return newAddress.ToString();
        }

        private IList<string> enumerateFloatingBits(string mask)
        {
            var permutations = new List<string>();

            var firstX = mask.IndexOf('X');

            if (firstX < 0)
            {
                permutations.Add(mask);
                return permutations;
            }

            for (int ii = 0; ii <= 1; ii++)
            {
                string permuted =
                    mask.Substring(0, firstX)
                    + ii.ToString()
                    + mask.Substring(firstX + 1);

                permutations.AddRange(enumerateFloatingBits(permuted));
            }

            return permutations;
        }
    }
}
