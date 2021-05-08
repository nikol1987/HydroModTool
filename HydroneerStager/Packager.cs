using HydroneerStager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;

namespace HydroneerStager
{
    internal static class Packager
    {
        public static void Package(BackgroundWorker thisWorker, Project project)
        {
            var stagedFilesDir = Path.Combine(project.OutputPath, "Staging", "Mining");
            var fileName = $"500-{project.Name}_P.pak";
            var outPath = Path.Combine(project.OutputPath, "dist");
            var outputFile = Path.Combine(outPath, fileName);

            thisWorker.ReportProgress(10, new PackageProgressReport()
            {
                Stage = "Preparing to Package"
            });

            var bytes = GetBytes(thisWorker, stagedFilesDir);

            thisWorker.ReportProgress(90, new PackageProgressReport()
            {
                Stage = "Creating pak"
            });

            Directory.CreateDirectory(outPath);
            File.WriteAllBytes(outputFile, bytes);

            thisWorker.ReportProgress(100, new PackageProgressReport()
            {
                Stage = "Pak created"
            });
        }

        private static byte[] GetBytes(BackgroundWorker thisWorker, string srcPath)
        {
            var mimeBytes = new byte[]
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
            };

            var fileList = new FileList
            {
                Bytes = new List<byte>(),
                FileCheck = new List<byte>(),
                GameFolderIndex = srcPath.LastIndexOf('\\') + 1
            };

            thisWorker.ReportProgress(70, new PackageProgressReport()
            {
                Stage = "Listing files"
            });

            GetFileList(srcPath, ref fileList);

            var enginePath = Path.Combine(srcPath, "..\\Engine\\");
            if (Directory.Exists(Path.GetFullPath(enginePath)))
            {
                GetFileList(Path.GetFullPath(enginePath), ref fileList);
            }

            thisWorker.ReportProgress(75, new PackageProgressReport()
            {
                Stage = "Creating pak header bytes"
            });

            var pakHeaderBytes = new List<byte>();
            pakHeaderBytes.AddRange(mimeBytes);
            pakHeaderBytes.AddRange(BitConverter.GetBytes(fileList.FileCount));
            pakHeaderBytes.AddRange(fileList.FileCheck);

            thisWorker.ReportProgress(80, new PackageProgressReport()
            {
                Stage = "Building pak bytes"
            });

            fileList.Bytes.AddRange(pakHeaderBytes);
            fileList.Bytes.AddRange(BitConverter.GetBytes(1517228769));
            fileList.Bytes.AddRange(BitConverter.GetBytes(3));
            fileList.Bytes.AddRange(BitConverter.GetBytes((long)fileList.FileCount));
            fileList.Bytes.AddRange(BitConverter.GetBytes((long)fileList.FileCount + 18L));
            fileList.Bytes.AddRange(new SHA1CryptoServiceProvider().ComputeHash(pakHeaderBytes.ToArray()));

            return fileList.Bytes.ToArray();
        }

        private static void GetFileList(string path, ref FileList fileList)
        {
            var files = Directory.GetFiles(path);
            var directories = Directory.GetDirectories(path);

            for (int i = 0; i < directories.Length; i++)
            {
                GetFileList(directories[i], ref fileList);
            }

            for (int i = 0; i < files.Length; i++)
            {
                var currentFileBytes = File.ReadAllBytes(files[i]);
                fileList.FileCheck.AddRange(AddFileBytes(currentFileBytes, fileList.Bytes, files[i].Substring(fileList.GameFolderIndex).Replace('\\', '/')));
                fileList.FileCount++;
            }
        }

        private static byte[] AddFileBytes(byte[] currentFile, List<byte> resultBytes, string fileName)
        {
            var rangeLength = (int)Math.Ceiling(currentFile.Length / 65536m);

            var fileBytes = new List<byte>();
            var blockSizes = new int[rangeLength];

            var resultByteCount = resultBytes.Count;

            for (int i = 0; i < rangeLength; i++)
            {
                var fileBlock = new byte[(currentFile.Length - 65536 * i > 65536) ? 65536 : (currentFile.Length - 65536 * i)];
                Buffer.BlockCopy(currentFile, 65536 * i, fileBlock, 0, fileBlock.Length);

                using(var memoryStream = new MemoryStream())
                {
                    using(var deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress))
                    {
                        deflateStream.Write(fileBlock, 0, fileBlock.Length);
                    }

                    var memoryBytes = memoryStream.ToArray();

                    fileBytes.AddRange(BitConverter.GetBytes((short)-25480)); //checked
                    fileBytes.AddRange(memoryBytes);
                    fileBytes.AddRange(BitConverter.GetBytes(Utilities.Adler32Checksum(fileBlock)).Reverse());
                    blockSizes[i] = memoryBytes.Length + 6;
                }
            }

            var headerBytes = new byte[57 + 16 * rangeLength]; //checked
            Buffer.BlockCopy(BitConverter.GetBytes(resultByteCount), 0, headerBytes, 0, 4);  //checked
            Buffer.BlockCopy(BitConverter.GetBytes(fileBytes.Count), 0, headerBytes, 8, 4); //checked
            Buffer.BlockCopy(BitConverter.GetBytes(currentFile.Length), 0, headerBytes, 16, 4); //checked
            Buffer.BlockCopy(BitConverter.GetBytes(1), 0, headerBytes, 24, 4); //checked
            Buffer.BlockCopy(new SHA1CryptoServiceProvider().ComputeHash(fileBytes.ToArray()), 0, headerBytes, 28, 20); //checked
            Buffer.BlockCopy(BitConverter.GetBytes(rangeLength), 0, headerBytes, 48, 4); //checked

            resultByteCount += headerBytes.Length;
            for (int i = 0; i < rangeLength; i++)
            {
                Buffer.BlockCopy(BitConverter.GetBytes((long)resultByteCount), 0, headerBytes, 52 + i * 16, 4);
                resultByteCount += blockSizes[i];
                Buffer.BlockCopy(BitConverter.GetBytes((long)resultByteCount), 0, headerBytes, 60 + i * 16, 4);
            }

            Buffer.BlockCopy(new byte[] { 1 }, 0, headerBytes, 55 + rangeLength * 16, 1);

            var finalHeaderBytes = new byte[fileName.Length + 5 + headerBytes.Length];
            Array.Copy(Utilities.FileNameToHeaderBytes(fileName), 0, finalHeaderBytes, 0, fileName.Length + 5);
            Array.Copy(headerBytes, 0, finalHeaderBytes, fileName.Length + 5, headerBytes.Length);

            resultBytes.AddRange(headerBytes);
            resultBytes.AddRange(fileBytes);

            return finalHeaderBytes;
        }


        private class FileList
        {
            public List<byte> Bytes { get; set; }

            public List<byte> FileCheck { get; set; }

            public int GameFolderIndex { get; set; }

            public int FileCount { get; set; }
        }

        public class PackageProgressReport
        {
            public string Stage { get; set; }
        }
    }
}