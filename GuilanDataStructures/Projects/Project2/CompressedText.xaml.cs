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
    /// Interaction logic for CompressedText.xaml
    /// </summary>
    public partial class CompressedText : Page
    {

        public MainHost Host { get; set; }
        DataStructures.Huffman.Encoder Encoder = null;

        public CompressedText(MainHost host)
        {
            Host = host;
            InitializeComponent();
            textEditorPanel.Visibility = Visibility.Visible;
            browsePanel.Visibility = Visibility.Collapsed;
        }

        private async void TextEditorOptionChosen(object sender, RoutedEventArgs e)
        {
            try
            {
                await browsePanel.HideUsingLinearAnimationAsync(300);
                textEditorPanel.ShowUsingLinearAnimation(300);
            }
            catch { }
        }

        private async void LoadTextOptionChosen(object sender, RoutedEventArgs e)
        {
            try
            {
                await textEditorPanel.HideUsingLinearAnimationAsync(300);
                browsePanel.ShowUsingLinearAnimation(300);
            }
            catch { }
        }

        private void openInput(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog() == true)
                inputURLTextbox.Text = openDialog.FileName;
        }

        private void outputOpen(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            saveDialog.FileName = "compressed.huffman";
            if (saveDialog.ShowDialog() == true)
                outputURLTextbox.Text = saveDialog.FileName;
        }

        private void buildHuffman_Click(object sender, RoutedEventArgs e)
        {
            if (editorOption.IsChecked == true)
            {

                Task.Run(() =>
                {
                    buildHuffman.Dispatcher.Invoke(() =>
                    {
                        buildHuffman.IsEnabled = false;
                        buildHuffman.Content = "در حال فشرده سازی...";
                    });
                    Thread.Sleep(100);
                    Dispatcher.Invoke(() =>
                    {
                        Encoder = new DataStructures.Huffman.Encoder();
                        Encoder.Encode(inputData.Text, outputURLTextbox.Text);
                    });
                    buildHuffman.Dispatcher.Invoke(() =>
                    {
                        buildHuffman.IsEnabled = true;
                        buildHuffman.Content = "فشرده سازی متن";
                    });
                });

                
            }
            else
            {
                Task.Run(() =>
                {
                    buildHuffman.Dispatcher.Invoke(() =>
                    {
                        buildHuffman.IsEnabled = false;
                        buildHuffman.Content = "در حال فشرده سازی...";
                    });
                    Thread.Sleep(100);
                    Dispatcher.Invoke(() =>
                    {
                        Encoder = new DataStructures.Huffman.Encoder();
                        Encoder.EncodeFile(inputURLTextbox.Text, outputURLTextbox.Text);
                    });
                    buildHuffman.Dispatcher.Invoke(() =>
                    {
                        buildHuffman.IsEnabled = true;
                        buildHuffman.Content = "فشرده سازی متن";
                    });
                    
                    
                });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Encoder != null)
            {
                Host.hostFrame.Navigate(new TreeView(this, Encoder.Root));
            }
            else
            {
                MessageBox.Show("ابتدا متن را به فایل فشرده تبدیل کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
