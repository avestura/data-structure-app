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

namespace GuilanDataStructures
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<PagedComboBoxItem> projects = new List<PagedComboBoxItem>()
        {
            new PagedComboBoxItem(){
                Content = "پروژه اول - برش مستطیل" ,
                NavigatePage = new BoresheMostatil()
            },
            new PagedComboBoxItem()
            {
                Content = "پروژه دوم - فشرده سازی متن",
                NavigatePage = new CompressedText()
            }
        };


        

        public MainWindow()
        {
            InitializeComponent();
            selectProjectCombo.ItemsSource = projects;
        }



        class PagedComboBoxItem : ComboBoxItem
        {


            public Page NavigatePage
            {
                get { return (Page)GetValue(NavigatePageProperty); }
                set { SetValue(NavigatePageProperty, value); }
            }

            // Using a DependencyProperty as the backing store for NavigatePage.  This enables animation, styling, binding, etc...
            public static readonly DependencyProperty NavigatePageProperty =
                DependencyProperty.Register("NavigatePage", typeof(Page), typeof(PagedComboBoxItem), null);

            public PagedComboBoxItem()
            {

            }

        }

        private void selectProjectCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(((PagedComboBoxItem)selectProjectCombo.SelectedItem).NavigatePage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
