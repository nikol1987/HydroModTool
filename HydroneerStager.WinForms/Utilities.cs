using ComponentFactory.Krypton.Toolkit;
using HydroneerStager.WinForms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HydroneerStager.WinForms
{
    internal static class Utilities
    {
        public static TreeNode BuildFileStruture(IReadOnlyCollection<ProjectItemModel> items)
        {
            var root = new TreeNode()
            {
                Text = "Root",
                Name = "root"
            };

            foreach (var item in items)
            {
                var pathParts = item.Path.Split("\\", StringSplitOptions.RemoveEmptyEntries);

                var lastTraversedNode = root;

                for (int i = 0; i < pathParts.Length; i++)
                {
                    var pathPart = pathParts[i];

                    var partNode = lastTraversedNode?.Nodes.Find("node-" + pathPart, true).FirstOrDefault();

                    if (partNode == null && i == 0)
                    {
                        var node = new KryptonTreeNode(pathPart)
                        {
                            Name = "node-" + pathPart,
                            Text = pathPart
                        };

                        lastTraversedNode = node;

                        root.Nodes.Add(node);
                        continue;
                    }
                    else if (partNode == null && i == pathParts.Length-1)
                    {
                        lastTraversedNode.Nodes.Add(new KryptonTreeNode(pathPart)
                        {
                            Name = item.Id.ToString(),
                            Text = pathPart
                        });
                    }
                    else if (partNode == null && i > 0)
                    {
                        var node = new KryptonTreeNode(pathPart)
                        {
                            Name = "node-" + pathPart,
                            Text = pathPart
                        };

                        lastTraversedNode.Nodes.Add(node);

                        lastTraversedNode = node;
                        continue;
                    }

                    lastTraversedNode = (KryptonTreeNode)partNode;
                }
            }

            return root;
        }
    }
}