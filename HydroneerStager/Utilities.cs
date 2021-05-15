using HydroneerStager.Models;
using System;
using System.IO;
using System.Text;

namespace HydroneerStager
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

        public static string GetOutFile(Project project)
        {
            var fileName = $"500-{project.Name}_P.pak";
            var outPath = Path.Combine(project.OutputPath, "dist");
            var outputFile = Path.Combine(outPath, fileName);

            return outputFile;
        }
    }
}