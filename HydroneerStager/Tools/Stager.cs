using Hydroneer.Contracts.Models;
using HydroneerStager.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace HydroneerStager.Tools
{
    public class Stager
    {
        public void Stage(Action<ProgressbarStateModel> reportProgress, int progressMin, int progressMax, Project project, IReadOnlyCollection<GuidItem> guids)
        {
            var store = Store.GetInstance();

            var basePathSrc = project.Path;
            var basePathOut = Path.Combine(project.OutputPath, "Staging", project.Name, "Mining");

            reportProgress.Invoke(new ProgressbarStateModel((int)Math.Floor(Utilities.Remap(10, 0, 100, progressMin, progressMax)), "Checking path"));

            if (Directory.Exists(basePathOut))
            {
                Directory.Delete(basePathOut, true);
            }

            var count = 0;
            foreach (var projectItem in project.Items)
            {
                reportProgress.Invoke(new ProgressbarStateModel((int)Math.Floor(Utilities.Remap((decimal)count, 0, (decimal)project.Items.Count, progressMin, progressMax)), $"Staging {count}/{project.Items.Count} ({projectItem.Name})"));

                var newPath = projectItem.Path.Replace(basePathSrc, basePathOut);
                Directory.CreateDirectory(Path.GetDirectoryName(basePathOut + newPath));



                if (projectItem.Name.EndsWith(".uexp"))
                {
                    var patched = PatchFile(basePathSrc + projectItem.Path, guids);
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

        private MemoryStream PatchFile(string fileSrc, IReadOnlyCollection<GuidItem> guids)
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
    }
}
