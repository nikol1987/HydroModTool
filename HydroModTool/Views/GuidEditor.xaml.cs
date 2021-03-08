using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HydroModTool.Views
{
    /// <summary>
    /// Interaction logic for GuidEditor.xaml
    /// </summary>
    public partial class GuidEditor : UserControl
    {
        public GuidEditor()
        {
            InitializeComponent();
            InitControls();
            //DataContextChanged += GuidEditor_DataContextChanged;
        }

        private void GuidEditor_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void InitControls()
        {
            IdGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            IdGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            IdGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });

            IdGrid.RowDefinitions.Add(new RowDefinition(){Height = new GridLength(25)});

            var cl = new Label
            {
                Content = "NAme",
                VerticalAlignment = VerticalAlignment.Center
            };
            cl.SetValue(Grid.ColumnProperty, 0);
            cl.SetValue(Grid.RowProperty, 0);
            cl.Margin = new Thickness(1);
            cl.FontWeight = FontWeights.Bold;
            IdGrid.Children.Add(cl);
        }
        private void UpdateControls()
        {

        }
    }
}
