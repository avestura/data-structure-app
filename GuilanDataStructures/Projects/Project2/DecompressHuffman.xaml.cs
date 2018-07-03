using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for DecompressHuffman.xaml
    /// </summary>
    public partial class DecompressHuffman : Page
    {
        public DecompressHuffman()
        {
            InitializeComponent();
        }

        private void openInput(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog() == true)
                inputURLTextbox.Text = openDialog.FileName;
        }

        private void outputOpen(object sender, RoutedEventArgs e)
        {
            var openDialog = new SaveFileDialog();
            if (openDialog.ShowDialog() == true)
                outputURLTextbox.Text = openDialog.FileName;
        }

        private void decodeHuffman_Click(object sender, RoutedEventArgs e)
        {
            decodeHuffman.IsEnabled = false;
            decodeHuffman.Content = "Decompressing...";
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);

                Dispatcher.Invoke(() =>
                {

                    new DataStructures.Huffman.Decoder().Decode(inputURLTextbox.Text, outputURLTextbox.Text);

                    decodeHuffman.IsEnabled = true;
                    decodeHuffman.Content = "Decompress file to a text file";
                });

            });

        }
    }
}
