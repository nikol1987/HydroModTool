using HydroneerStager.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace HydroneerStager
{
    public static class Stager
    {
        public static void Stage(BackgroundWorker thisWorker, Project project)
        {
            var basePathSrc = project.Path;
            var basePathOut = Path.Combine(project.OutputPath, "Staging", "Mining");

            var count = 0;
            foreach (var projectItem in project.Items)
            {
                thisWorker.ReportProgress(MapProgress((decimal)count, (decimal)project.Items.Count), projectItem);

                var newPath = projectItem.Path.Replace(basePathSrc, basePathOut);
                Directory.CreateDirectory(Path.GetDirectoryName(basePathOut + newPath));



                if (projectItem.Name.EndsWith(".uexp"))
                {
                    var patched = PatchFile(basePathSrc + projectItem.Path, Store.Instance.Guids);
                    Directory.CreateDirectory(Path.GetDirectoryName(basePathOut + newPath));

                    using (var file = File.Create(basePathOut + newPath, (int)patched.Length, FileOptions.Asynchronous | FileOptions.SequentialScan))
                    {
                        patched.Position = 0;
                        patched.CopyTo(file);
                    }
                }
                else
                {
                    File.Copy(basePathSrc+projectItem.Path, basePathOut+newPath, true);
                }

                count++;
            }
        }

        private static MemoryStream PatchFile(string fileSrc, IReadOnlyCollection<GuidItem> guids)
        {
            var bytes = new HashSet<byte>();
            foreach (var entry in guids)
                if (entry.ModdedGuid.ToByteArray().Length > 0)
                    bytes.Add(entry.ModdedGuid.ToByteArray()[0]);


            var fi = File.Open(fileSrc, FileMode.Open, FileAccess.Read);
            var ms = new MemoryStream((int)fi.Length);
            fi.CopyTo(ms);
            fi.Dispose();
            ms.Position = 0;

            var buf = new byte[16];

            while (ms.Position < ms.Length - 16)
            {
                var b = (byte)ms.ReadByte();

                if (bytes.Contains(b))
                {
                    buf[0] = b;
                    ms.Read(buf, 1, 15);
                    foreach (var entry in guids)
                    {
                        if (entry.ModdedGuid == null || entry.OriginalGuid == null)
                            continue;

                        if (Utilities.CompareBytes(buf, entry.ModdedGuid.ToByteArray()))
                        {
                            ms.Seek(-16, SeekOrigin.Current);
                            ms.Write(entry.OriginalGuid.ToByteArray(), 0, 16);

                            goto Hit;
                        }
                    }
                    ms.Seek(-15, SeekOrigin.Current);

                }
            Hit:;
            }

            return ms;
        }

        private static int MapProgress(this decimal value, decimal toSource)
        {
            decimal part1 = (value / toSource) * (decimal)100.00;

            return (int)part1 ;
        }
    }
}
