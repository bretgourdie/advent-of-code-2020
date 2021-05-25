using System;

namespace advent_of_code_2020.Day14
{
    class Mask
    {
        public readonly string MaskPart;

        public readonly long AndMask;
        public readonly long OrMask;

        private const int binaryBase = 2;
        private const char bitmaskX = 'X';

        public Mask(string line)
        {
            var split = line.Split('=');
            MaskPart = split[1].TrimStart();

            AndMask = Convert.ToInt64(MaskPart.Replace(bitmaskX, '1'), binaryBase);
            OrMask = Convert.ToInt64(MaskPart.Replace(bitmaskX, '0'), binaryBase);
        }
    }
}
