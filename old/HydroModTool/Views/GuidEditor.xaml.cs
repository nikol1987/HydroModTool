using System.Windows.Controls;

namespace HydroModTool.Views
{
    /// <summary>
    /// Interaction logic for GuidEditor.xaml
    /// </summary>
    public partial class GuidEditor : UserControl
    {

        //Words cannot express just how much I hate wpf

        public GuidEditor()
        {
            DataContext = new GuidViewModel();
            InitializeComponent();
        }

    }
}
