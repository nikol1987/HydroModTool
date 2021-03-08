using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroModTool.Views;

namespace HydroModTool
{
    public static class Packaging
    {
        public static void PackageProject(ProjectListing project, ConfigViewModel config)
        {
            string output = Path.Combine(project.OutputPath, "Staging");
            if(Directory.Exists(output))
                Directory.Delete(output, true);
            Directory.CreateDirectory(output);

            foreach (var file in project.IncludedFiles)
            {
                if (file.EndsWith(".uexp"))
                {
                    var patched = PatchFile(file, config, project.OutputPath.Length + 2);
                    var subdirName = file.Substring(project.InputPath.Length);
                    var outpath =  output + subdirName;
                    Directory.CreateDirectory(Path.GetDirectoryName(outpath));
                    using (var fo = File.Create(outpath, (int)patched.Length, FileOptions.Asynchronous | FileOptions.SequentialScan))
                    {
                        patched.Position = 0;
                        patched.CopyTo(fo);
                    }
                }
                else
                {
                    var subdirName = file.Substring(project.InputPath.Length);
                    var outpath = output + subdirName;
                    var outdir = Path.GetDirectoryName(outpath);
                    Directory.CreateDirectory(outdir);
                    File.Copy(file, outpath);
                }
            }
        }

        private static MemoryStream PatchFile(string path, ConfigViewModel config, int pathOffsetHack)
        {
            var fi = File.Open(path, FileMode.Open, FileAccess.Read);
            var ms = new MemoryStream((int)fi.Length);
            fi.CopyTo(ms);
            fi.Dispose();
            ms.Position = 0;

            var buf = new byte[16];

            bool found = false;

            while (ms.Position < ms.Length - 16)
            {
                var b = (byte)ms.ReadByte();

                //we found a byte matching one of our guids. Grab the next 15 bytes and try to match to the list
                if (config.FirstBytes.Contains(b))
                {
                    buf[0] = b;
                    ms.Read(buf, 1, 15);
                    foreach (var entry in config.Guids)
                    {
                        //we found the guid, now replace the value in the stream
                        if (Utilities.CompareBytes(buf, entry.ModifiedBytes))
                        {
                            ms.Seek(-16, SeekOrigin.Current);
                            ms.Write(entry.RetailBytes, 0, 16);
                            if (!found && (found = true))
                                Console.WriteLine("Patched " + path.Substring(pathOffsetHack));
                            Console.WriteLine(entry.Name);
                            //found and replaced the ID, continue 
                            goto Hit;
                        }
                    }
                    //we didn't find a full match, rewind to where we were
                    ms.Seek(-15, SeekOrigin.Current);
                    
                }
                //don't @ me
                Hit: ;
            }

            return ms;
        }
    }
}
