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

namespace GuilanDataStructures.Projects.Project2
{
    /// <summary>
    /// Interaction logic for MainHost.xaml
    /// </summary>
    public partial class MainHost : Page
    {

        public CompressedText compressedText ;
        public DecompressHuffman decompressHuffman = new DecompressHuffman();

        public MainHost()
        {
            compressedText = new CompressedText(this);
            InitializeComponent();
        }

        private void compressHuffman_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                hostFrame.Navigate(compressedText);
            }
            catch { }
        }

        private void hostFrame_Loaded(object sender, RoutedEventArgs e)
        {
            hostFrame.Navigate(compressedText);
        }

        private void decompressHuffman_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                hostFrame.Navigate(decompressHuffman);
            }
            catch { }
        }
    }
}
