using HydroneerStager.Pages;
using System.Windows.Forms;

namespace HydroneerStager
{
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();

            tabControl1.VisibleChanged += TabControl1_VisibleChanged;
        }

        private void TabControl1_VisibleChanged(object sender, System.EventArgs e)
        {
            var projectsPage = new ProjectsPage();
            projectsPage.Dock = DockStyle.Fill;

            tabControl1.TabPages["projectsTab"].Controls.Add(projectsPage);

            var guidsPage = new GuidsPage();
            guidsPage.Dock = DockStyle.Fill;

            tabControl1.TabPages["guidsTab"].Controls.Add(guidsPage);
        }
    }
}
