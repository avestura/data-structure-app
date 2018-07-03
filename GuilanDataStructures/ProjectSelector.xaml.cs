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
using GuilanDataStructures.Projects;

namespace GuilanDataStructures
{
    /// <summary>
    /// Interaction logic for ProjectSelector.xaml
    /// </summary>
    public partial class ProjectSelector : Page
    {
        public ProjectSelector()
        {
            InitializeComponent();
        }

        Projects.Project1.BoresheMostatil project1 = new Projects.Project1.BoresheMostatil();
        Projects.Project2.MainHost project2 = new Projects.Project2.MainHost();
        Projects.Project3.Treap project3 = new Projects.Project3.Treap();
        Projects.Project4.ProjectDescribe project4 = new Projects.Project4.ProjectDescribe();

        private void NavigateTo(object Navigation)
        {
            App.MainWindowApp.mainFrame.Navigate(Navigation);
            App.MainWindowApp.backToHome.ShowUsingLinearAnimation();
        }

        private void mostatilCard_ClickedViewProject(object sender, RoutedEventArgs e)
        {
            NavigateTo(project1);
        }

        private void compressCard_ClickedViewProject(object sender, RoutedEventArgs e)
        {
            NavigateTo(project2);
        }

        private void treapCard_ClickedViewProject(object sender, RoutedEventArgs e)
        {
            NavigateTo(project3);
        }

        private void mazeCard_ClickedViewProject(object sender, RoutedEventArgs e)
        {
            NavigateTo(project4);
        }

    }
}
