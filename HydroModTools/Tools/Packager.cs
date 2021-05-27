using HydroModTools;
using HydroModTools.Common.Models;
using HydroModTools.Contracts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HydroModTools.Tools
{
    public class Packager
    {
        public async Task PackageAsync(Action<ProgressbarStateModel> reportProgress, int progressMin, int progressMax, ProjectModel project)
        {
            var stagedFilesDir = Path.Combine(project.OutputPath, "Staging", project.Name, "Mining");
            var outputFile = Utilities.GetOutFile(project);

            var stuff = new OldStuff();

            reportProgress.Invoke(new ProgressbarStateModel((int)Math.Floor(Utilities.Remap(10, 0, 100, progressMin, progressMax)), "Preparing to Package"));

            Directory.CreateDirectory(Path.GetDirectoryName(outputFile));

            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }

            var bytes = stuff.GetBytes(stagedFilesDir);

            reportProgress.Invoke(new ProgressbarStateModel((int)Math.Floor(Utilities.Remap(80, 0, 100, progressMin, progressMax)), "Creating pak"));

            File.WriteAllBytes(outputFile, bytes);

            reportProgress.Invoke(new ProgressbarStateModel((int)Math.Floor(Utilities.Remap(80, 0, 100, progressMin, progressMax)), "Pak created"));
        }

        public class OldStuff
        {
            private class FileList
            {
                public List<byte> Bytes { get; set; }

                public List<byte> FileCheck { get; set; }

                public int GameFolderIndex { get; set; }

                public int FileCount { get; set; }
            }

            public byte[] GetBytes(string srcFolder)
            {
                return FolderToPak(srcFolder).ToArray();
            }

            private void GetFileList(string dir, ref FileList A_2)
            {

                string[] files = Directory.GetFiles(dir);
                string[] directories = Directory.GetDirectories(dir);
                int i = 0;
                int num = directories.Length;
                while (i < num)
                {
                    this.GetFileList(directories[i], ref A_2);
                    i++;
                }
                for (int j = 0; j < files.Length; j++)
                {
                    byte[] oldRange = File.ReadAllBytes(files[j]);
                    A_2.FileCheck.AddRange(this.AddFileToPak(oldRange, A_2.Bytes, files[j].Substring(A_2.GameFolderIndex).Replace('\\', '/')));

                    int fileCount = A_2.FileCount;
                    A_2.FileCount = fileCount + 1;
                }
            }

            private List<byte> FolderToPak(string input)
            {
                var fileData = new FileList()
                {
                    FileCheck = new List<byte>(),
                    GameFolderIndex = input.LastIndexOf('\\') + 1,
                    FileCount = 0,
                    Bytes = new List<byte>()
                };

                List<byte> list = new List<byte>();
                GetFileList(input, ref fileData);
                if (Directory.Exists(Path.GetFullPath(Path.Combine(input, "..\\Engine\\"))))
                {
                    GetFileList(Path.GetFullPath(Path.Combine(input, "..\\Engine\\")), ref fileData);
                }
                int count = fileData.Bytes.Count;
                list.AddRange(new byte[]
                {
                10,
                0,
                0,
                0,
                46,
                46,
                47,
                46,
                46,
                47,
                46,
                46,
                47,
                0
                });
                list.AddRange(BitConverter.GetBytes(fileData.FileCount));
                list.AddRange(fileData.FileCheck);
                fileData.Bytes.AddRange(list);
                fileData.Bytes.AddRange(BitConverter.GetBytes(1517228769));
                fileData.Bytes.AddRange(BitConverter.GetBytes(3));
                fileData.Bytes.AddRange(BitConverter.GetBytes((long)count));
                fileData.Bytes.AddRange(BitConverter.GetBytes((long)fileData.FileCheck.Count + 18L));
                fileData.Bytes.AddRange(new SHA1CryptoServiceProvider().ComputeHash(list.ToArray()));
                return fileData.Bytes;
            }

            private byte[] AddFileToPak(byte[] oldRange, List<byte> fileCore, string FileName)
            {
                List<byte> list = new List<byte>();
                int num = fileCore.Count;
                int num2 = (int)Math.Ceiling(oldRange.Length / 65536m);
                int[] array = new int[num2];
                Console.WriteLine("Sectors: " + num2.ToString());
                for (int i = 0; i < num2; i++)
                {
                    Console.WriteLine(FileName + " : " + i.ToString());
                    byte[] array2 = new byte[(oldRange.Length - 65536 * i > 65536) ? 65536 : (oldRange.Length - 65536 * i)];
                    Buffer.BlockCopy(oldRange, 65536 * i, array2, 0, array2.Length);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (System.IO.Compression.DeflateStream deflateStream = new System.IO.Compression.DeflateStream(memoryStream, System.IO.Compression.CompressionMode.Compress))
                        {
                            deflateStream.Write(array2, 0, array2.Length);
                        }
                        list.AddRange(BitConverter.GetBytes((short)-25480));
                        byte[] array3 = memoryStream.ToArray();
                        list.AddRange(array3);
                        list.AddRange(BitConverter.GetBytes(Utilities.Adler32Checksum(array2)).Reverse());
                        array[i] = array3.Length + 6;
                    }
                }
                byte[] array4 = new byte[57 + 16 * num2];
                Buffer.BlockCopy(BitConverter.GetBytes(num), 0, array4, 0, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(list.Count), 0, array4, 8, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(oldRange.Length), 0, array4, 16, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(1), 0, array4, 24, 4);
                Buffer.BlockCopy(new SHA1CryptoServiceProvider().ComputeHash(list.ToArray()), 0, array4, 28, 20);
                Buffer.BlockCopy(BitConverter.GetBytes(num2), 0, array4, 48, 4);
                num += array4.Length;
                for (int j = 0; j < num2; j++)
                {
                    Buffer.BlockCopy(BitConverter.GetBytes((long)num), 0, array4, 52 + j * 16, 4);
                    num += array[j];
                    Buffer.BlockCopy(BitConverter.GetBytes((long)num), 0, array4, 60 + j * 16, 4);
                }
                Buffer.BlockCopy(new byte[]
                {
                1
                }, 0, array4, 55 + num2 * 16, 1);
                byte[] array5 = new byte[FileName.Length + 5 + array4.Length];
                Array.Copy(Utilities.FileNameToHeaderBytes(FileName), 0, array5, 0, FileName.Length + 5);
                Array.Copy(array4, 0, array5, FileName.Length + 5, array4.Length);
                fileCore.AddRange(array4);
                fileCore.AddRange(list);
                return array5;
            }

            private static class Utilities
            {
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
            }
        }
    }
}