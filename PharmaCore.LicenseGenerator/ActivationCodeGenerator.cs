using System;

namespace PharmaCore.ActivationManager
{
    public static class ActivationCodeGenerator
    {
        private static readonly char[] _chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();
        private static readonly Random _rand = new Random();

        public static string Generate()
        {
            return $"{Block(4)}-{Block(5)}-{Block(4)}-{Block(4)}";
        }

        private static string Block(int length)
        {
            var buffer = new char[length];
            for (int i = 0; i < length; i++)
                buffer[i] = _chars[_rand.Next(_chars.Length)];
            return new string(buffer);
        }
    }
}
