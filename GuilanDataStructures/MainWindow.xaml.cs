using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using GuilanDataStructures.DataStructures;
using GuilanDataStructures.DataStructures.Generic;

namespace GuilanDataStructures
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

         ProjectSelector selectorPage = new ProjectSelector();

        public MainWindow()
        {

            InitializeComponent();
            //selectProjectCombo.ItemsSource = projects;
            App.MainWindowApp = this;

        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void mainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(selectorPage);
        }

        private void showSourceButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(selectorPage);
            backToHome.HideUsingLinearAnimation();
        }
    }
}
