using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HydroModTools.Client.Wpf.ExtendedControls.TreeView
{
    internal class FileTreeViewNode : TreeViewItem
    {
        public FileTreeViewNode(Guid? itemFileId)
        {
            FileId = itemFileId ?? Guid.Empty;
            
            FontFamily = new FontFamily("Consolas");
            FontWeight = FontWeights.Bold;
            FontSize = 12d;
        }

        public FileTreeViewNode()
            : this(null)
        {}

        public Guid FileId { get; } = Guid.Empty;
        
        public override string ToString()
        {
            return Header.ToString();
        }
    }
}