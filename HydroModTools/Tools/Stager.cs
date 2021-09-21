using HydroModTools.Common.Models;
using HydroModTools.Contracts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HydroModTools.Tools
{
    public class Stager
    {
        public Task StageAsync(Action<ProgressbarStateModel> reportProgress, int progressMin, int progressMax, ProjectModel project, IReadOnlyCollection<GuidItemModel> guids)
        {
            var basePathSrc = project.Path;
            var basePathOut = Path.Combine(project.OutputPath, "Staging", project.Name, "Mining");

            reportProgress.Invoke(new ProgressbarStateModel((int)Math.Floor(Utilities.Remap(10, 0, 100, progressMin, progressMax)), "Checking path"));

            if (Directory.Exists(basePathOut))
            {
                Directory.Delete(basePathOut, true);
            }

            var missingFiles = VerifyFiles(basePathSrc, project.Items);

            if (missingFiles.Count > 0)
            {
                MessageBox.Show($"These assets are missing: \n{string.Join("\n", missingFiles)} \n Stating aborted", "Missing Assets");

                return Task.CompletedTask;
            }

            var count = 0;
            foreach (var projectItem in project.Items)
            {
                reportProgress.Invoke(new ProgressbarStateModel((int)Math.Floor(Utilities.Remap((decimal)count, 0, (decimal)project.Items.Count, progressMin, progressMax)), $"Staging {count + 1}/{project.Items.Count} ({projectItem.Name})"));

                var newPath = projectItem.Path.Replace(basePathSrc, basePathOut);
                Directory.CreateDirectory(Path.GetDirectoryName(basePathOut + newPath));

                if (projectItem.Name.EndsWith(".uexp"))
                {
                    var patched = PatchFile(basePathSrc + projectItem.Path, guids);

                    if (patched == null)
                    {
                        File.Copy(basePathSrc + projectItem.Path, basePathOut + newPath, true);
                        count++;

                        continue;
                    }

                    Directory.CreateDirectory(Path.GetDirectoryName(basePathOut + newPath));

                    using (var file = File.Create(basePathOut + newPath, (int)patched.Length, FileOptions.Asynchronous | FileOptions.SequentialScan))
                    {
                        patched.Position = 0;
                        patched.CopyTo(file);
                    }
                }
                else
                {
                    File.Copy(basePathSrc + projectItem.Path, basePathOut + newPath, true);
                }

                count++;
            }

            return Task.CompletedTask;
        }

        private IReadOnlyCollection<string> VerifyFiles(string basepath, IReadOnlyCollection<ProjectItemModel> items)
        {
            var itemList = new List<ProjectItemModel>(items);

            return itemList.Where(item => {
                var itemPath = basepath + item.Path;

                return !File.Exists(itemPath);
            }).Select(item => basepath + item.Path).ToList();
        }

        private MemoryStream PatchFile(string fileSrc, IReadOnlyCollection<GuidItemModel> guids)
        {
            var fileBytes = File.ReadAllBytes(fileSrc);

            var patchedBytes = new byte[fileBytes.Length];
            Buffer.BlockCopy(fileBytes, 0, patchedBytes, 0, patchedBytes.Length);

            var patched = false;
            foreach (var guid in guids)
            {
                var moddedGuidBytes = Utilities.Hex2Binary(guid.ModdedGuid.ToString("N"));
                var originalGuidBytes = Utilities.Hex2Binary(guid.OriginalGuid.ToString("N"));

                var position = Utilities.SearchBytePattern(moddedGuidBytes, patchedBytes);

                if (!position.HasValue)
                {
                    continue;
                }

                var tmpPatchedBytes = new byte[patchedBytes.Length];

                Buffer.BlockCopy(patchedBytes, 0, tmpPatchedBytes, 0, position.Value);
                Buffer.BlockCopy(originalGuidBytes, 0, tmpPatchedBytes, position.Value, 16);
                Buffer.BlockCopy(patchedBytes, position.Value + 16, tmpPatchedBytes, position.Value + 16, patchedBytes.Length - (position.Value + 16));

                patched = true;

                Buffer.BlockCopy(tmpPatchedBytes, 0, patchedBytes, 0, patchedBytes.Length);
            }

            if (!patched)
            {
                return null;
            }


            var ms = new MemoryStream(patchedBytes);

            return ms;
        }
    }
}
