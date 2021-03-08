using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroModTool
{
    public static class Utilities
    {
        public static bool Contains(this string text, string testSequence, StringComparison comparison)
        {
            return text.IndexOf(testSequence, comparison) != -1;
        }

        private static Dictionary<char, Dictionary<char, byte>> _byteTable = null;
        public static byte[] StringToByteArray(string hex)
        {
            if (_byteTable == null)
                CalculateHex();

            var chars = hex.Length;
            var bytes = new byte[chars / 2];
            for (var i = 0; i < chars; i += 2)
                bytes[i / 2] = _byteTable[hex[i]][hex[i + 1]];
            return bytes;
        }

        private static void CalculateHex()
        {
            _byteTable = new Dictionary<char, Dictionary<char, byte>>(16);
            var chars = new[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
            byte b = 0;
            for (int i = 0; i < 16; i++)
            {
                var d = new Dictionary<char, byte>(16);
                _byteTable[chars[i]] = d;

                for (int j = 0; j < 16; j++)
                {
                    d[chars[j]] = b;
                    b++;
                }
            }
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

        public static string GetPathAt(int depth, string path)
        {
            int d = 0;

            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == '\\' && ++d > depth)
                    return path.Substring(0, i);
            }

            if(d == depth)
            return path;
            return null;
        }
    }
}
