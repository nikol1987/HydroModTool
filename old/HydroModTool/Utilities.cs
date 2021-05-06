using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HydroModTool
{
    public static class Utilities
    {
        public static bool Contains(this string text, string testSequence, StringComparison comparison)
        {
            return text.IndexOf(testSequence, comparison) != -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char ToUpper(this char c)
        {
            if (c < 97 || c > 122)
                return c;

            return (char)(c - 32);
        }


        private static Dictionary<char, Dictionary<char, byte>> _byteTable;
        private static Regex _hexMatch = new Regex(@"^[A-Fa-f0-9]*$");
        public static byte[] HexToByteArray(string hex)
        {
            if (_byteTable == null)
                _byteTable = CalculateHex();

            if (string.IsNullOrEmpty(hex))
                return null;

            if (!_hexMatch.IsMatch(hex))
                return null;

            var chars = hex.Length;
            var bytes = new byte[chars / 2];
            for (var i = 0; i < chars; i += 2)
                bytes[i / 2] = _byteTable[hex[i].ToUpper()][hex[i + 1].ToUpper()];
            return bytes;
        }

        private static Dictionary<char, Dictionary<char, byte>> CalculateHex()
        {
            var table = new Dictionary<char, Dictionary<char, byte>>(16);
            var chars = new[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
            byte b = 0;
            for (int i = 0; i < 16; i++)
            {
                var d = new Dictionary<char, byte>(16);
                table[chars[i]] = d;

                for (int j = 0; j < 16; j++)
                {
                    d[chars[j]] = b;
                    b++;
                }
            }

            return table;
        }

        public static bool CompareBytes(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Trim n levels from the right, preserving the root
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string TrimPath(int depth, string path)
        {
            int d = 0;

            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == '\\' && ++d > depth)
                    return path.Substring(0, i);
            }

            if (d == depth)
                return path;

            return null;
        }
    }
}
