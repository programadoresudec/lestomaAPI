namespace lestoma.CRC
{
    public static class CrcHelper
    {
        public static ulong ReverseBits(ulong ul, int valueLength)
        {
            ulong newValue = 0;

            for (int i = valueLength - 1; i >= 0; i--)
            {
                newValue |= (ul & 1) << i;
                ul >>= 1;
            }

            return newValue;
        }
    }
}