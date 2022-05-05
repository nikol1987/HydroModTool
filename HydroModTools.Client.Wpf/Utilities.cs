using HydroModTools.Client.Wpf.ExtendedControls;
using HydroModTools.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;
using HydroModTools.Client.Wpf.ExtendedControls.TreeView;
using Notifications.Wpf;

namespace HydroModTools.Client.Wpf
{
    internal static class Utilities
    {
        public static NotificationContent CreateInfoNotification(string title, string message)
        {
            return new NotificationContent()
            {
                Title = title,
                Message = message,
                Type = NotificationType.Information
            };
        }
        
        public static Brush GetColorFromBridgepourRibbonColor(string name)
        {
            var color = name switch
            {
                "orange" => "#f2711c",
                "green" => "#21ba45",
                "red" => "#db2828",
                "blue" => "#2185d0",
                _ => "#FF3C3C3C"
            };

            var brush = new BrushConverter().ConvertFrom(color) as SolidColorBrush;

            return brush!;
        }

        public static FileTreeViewNode? BuildFileStruture(IReadOnlyCollection<ProjectItemModel> items)
        {
            var root = new FileTreeViewNode()
            {
                Header = "Root",
                Name = "root",
                IsExpanded = true,
                Visibility = System.Windows.Visibility.Hidden
            };

            foreach (var item in items)
            {
                var pathParts = item.Path.Split("\\", StringSplitOptions.RemoveEmptyEntries);

                FileTreeViewNode lastTraversedNode = root;

                for (int i = 0; i < pathParts.Length; i++)
                {
                    var pathPart = pathParts[i];

                    var partNode = lastTraversedNode?.Items.Find("node11" + pathPart);

                    if (partNode == null && i == 0)
                    {
                        var node = new FileTreeViewNode()
                        {
                            Name = "node11" + Regex.Replace(pathPart, "[^A-Za-z0-9 -]", ""),
                            Header = pathPart,
                            IsExpanded = true
                        };

                        lastTraversedNode = node;

                        root.Items.Add(node);
                        continue;
                    }
                    else if (partNode == null && i == pathParts.Length - 1)
                    {
                        lastTraversedNode!.Items.Add(new FileTreeViewNode(item.Id)
                        {
                            Name = "nodeFile",
                            Header = pathPart
                        });
                    }
                    else if (partNode == null && i > 0)
                    {
                        var node = new FileTreeViewNode()
                        {
                            Name = "node11" +  Regex.Replace(pathPart, "[^A-Za-z0-9 -]", ""),
                            Header = pathPart,
                            IsExpanded = true
                        };

                        lastTraversedNode!.Items.Add(node);

                        lastTraversedNode = node;
                        continue;
                    }

                    lastTraversedNode = partNode;
                }
            }

            FileTreeViewNode? rootChild = root.Items.Count > 0 ? (FileTreeViewNode)root.Items[0] : null;

            if (rootChild == null)
            {
                return null;
            }

            var newContentRoot = new FileTreeViewNode(rootChild?.FileId)
            {
                Header = rootChild!.Header,
                Name = rootChild!.Name,
                IsExpanded = true
            };

            var itemCount = rootChild.Items.Count;

            for (int i = 0; i < itemCount; i++)
            {
                var item = rootChild.Items[0];
                rootChild.Items.RemoveAt(0);

                newContentRoot.Items.Add(item);
            }

            return newContentRoot;
        }

        private static FileTreeViewNode? Find(this ItemCollection itemCollection, string name)
        {
            foreach (var item in itemCollection)
            {
                if (((FileTreeViewNode)item).Name == name)
                {
                    return (FileTreeViewNode)item;
                }

                var nextChild = ((FileTreeViewNode)item).Items.Find(name); ;

                if (nextChild != null)
                {
                    return nextChild;
                }
            }

            return null;
        }
    }
}
