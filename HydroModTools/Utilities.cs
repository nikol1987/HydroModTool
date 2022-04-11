using HydroModTools.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace HydroModTools
{
    internal static class Utilities
    {
        public static decimal Remap(decimal value, decimal from1, decimal to1, decimal from2, decimal to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
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

        public static uint Adler32Checksum(byte[] blockData)
        {
            uint num = 1U;
            uint num2 = 0U;
            foreach (byte b in blockData)
            {
                num = (num + (uint)b) % 65521U;
                num2 = (num2 + num) % 65521U;
            }
            return num2 << 16 | num;
        }

        public static byte[] FileNameToHeaderBytes(string fileName)
        {
            var bytes = new byte[fileName.Length + 5];
            Buffer.BlockCopy(BitConverter.GetBytes(fileName.Length + 1), 0, bytes, 0, 4);
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(fileName), 0, bytes, 4, fileName.Length);

            return bytes;
        }

        public static string GetOutFile(ProjectModel project)
        {
            var fileName = $"{project.ModIndex}-{project.Name}_P.pak";
            var outPath = Path.Combine(project.OutputPath, "dist");
            var outputFile = Path.Combine(outPath, fileName);

            return outputFile;
        }

        public static int? SearchBytePattern(byte[] pattern, byte[] bytes)
        {
            List<int> positions = new List<int>();
            int patternLength = pattern.Length;
            int totalLength = bytes.Length;
            byte firstMatchByte = pattern[0];
            for (int i = 0; i < totalLength; i++)
            {
                if (firstMatchByte == bytes[i] && totalLength - i >= patternLength)
                {
                    byte[] match = new byte[patternLength];
                    Array.Copy(bytes, i, match, 0, patternLength);
                    if (match.SequenceEqual<byte>(pattern))
                    {
                        positions.Add(i);
                        i += patternLength - 1;
                    }
                }
            }

            if (positions.Count == 0)
            {
                return null;
            }

            return positions.First();
        }

        public static byte[] Hex2Binary(string hex)
        {
            var chars = hex.ToCharArray();
            var bytes = new List<byte>();
            for (int index = 0; index < chars.Length; index += 2)
            {
                var chunk = new string(chars, index, 2);
                bytes.Add(byte.Parse(chunk, NumberStyles.AllowHexSpecifier));
            }
            return bytes.ToArray();
        }
    }
}