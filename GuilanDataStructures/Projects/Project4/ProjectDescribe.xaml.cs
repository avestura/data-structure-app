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

namespace GuilanDataStructures.Projects.Project4
{
    /// <summary>
    /// Interaction logic for ProjectDescribe.xaml
    /// </summary>
    public partial class ProjectDescribe : Page
    {
        public ProjectDescribe()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.MainWindowApp.mainFrame.Navigate(new MainPage() { UseDFS = false });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            App.MainWindowApp.mainFrame.Navigate(new MainPage() { UseDFS = true });

        }
    }
}
