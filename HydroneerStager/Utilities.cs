using HydroneerStager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HydroneerStager
{
    internal static class Utilities
    {
        public static TreeNode BuildFileStruture(ContextMenuStrip menuStrip, IReadOnlyCollection<ProjectItem> items)
        {
            var root = new TreeNode()
            {
                Text = "Root",
                Name = "root"
            };

            foreach (var item in items)
            {
                var pathParts = item.Path.Split("\\", StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < pathParts.Length; i++)
                {
                    var pathPart = pathParts[i];

                    var partNode = root.Nodes.Find("node-" + pathPart, true).FirstOrDefault();

                    if (partNode == null && i == 0)
                    {
                        root.Nodes.Add(new TreeNode(pathPart)
                        {
                            Name = "node-"+pathPart,
                            ContextMenuStrip = menuStrip,
                            Text = pathPart
                        });
                        continue;
                    }
                    else if (partNode == null && i == pathParts.Length-1)
                    {
                        var parentNode = root.Nodes.Find("node-" + pathParts[i - 1], true).FirstOrDefault();

                        parentNode.Nodes.Add(new TreeNode(pathPart)
                        {
                            Name = item.Id.ToString(),
                            ContextMenuStrip = menuStrip,
                            Text = pathPart
                        });
                    }
                    else if (partNode == null && i > 0)
                    {
                        var parentNode = root.Nodes.Find("node-" + pathParts[i - 1], true).FirstOrDefault();

                        parentNode.Nodes.Add(new TreeNode(pathPart)
                        {
                            Name = "node-" + pathPart,
                            ContextMenuStrip = menuStrip,
                            Text = pathPart
                        });
                        continue;
                    }
                }
            }

            return root;
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