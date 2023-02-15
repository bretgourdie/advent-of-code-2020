using System;

namespace advent_of_code_2020.Day25
{
    class Encryption
    {
        private const long START_VALUE = 1;
        private const long SUBJECT = 7;
        private const long DIVISOR = 20201227;

        public long TransformFromSubject(
            long cardPublicKey,
            long doorPublicKey)
        {
            long cardSecretLoopSize = findLoopSize(cardPublicKey);
            long doorSecretLoopSize = findLoopSize(doorPublicKey);

            long cardEncryptionKey = findEncryptionKey(doorPublicKey, cardSecretLoopSize);
            long doorEncryptionKey = findEncryptionKey(cardPublicKey, doorSecretLoopSize);

            if (cardEncryptionKey != doorEncryptionKey)
            {
                throw new ApplicationException(
                    $"Encryption keys don't match; card: \"{cardEncryptionKey}\"; door: \"{doorEncryptionKey}\"");
            }

            return cardEncryptionKey;
        }

        private long findLoopSize(
            long publicKey)
        {
            long value = START_VALUE;
            long loopSize = 0;

            while (value != publicKey)
            {
                value *= SUBJECT;
                value %= DIVISOR;
                loopSize += 1;
            }

            return loopSize;
        }

        private long findEncryptionKey(
            long subject,
            long loopSize)
        {
            long value = START_VALUE;

            for (long ii = 0; ii < loopSize; ii++)
            {
                value *= subject;
                value %= DIVISOR;
            }

            return value;
        }
    }
}
