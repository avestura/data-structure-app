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
    /// Interaction logic for CharFrequency.xaml
    /// </summary>
    public partial class CharFrequency : UserControl
    {


        public bool IsBlue { get; set; }
        public char Character { get; set; }
        public int Frequency { get; set; }

        public CharFrequency(char c, int f, bool isBlue)
        {


            Character = c; Frequency = f; IsBlue = isBlue;

     

            InitializeComponent();


            @char.Text = Character.ToString();
            freq.Text = Frequency.ToString();

            if (IsBlue)
            {
                outerBorder.Background = Brushes.LightBlue;
                outerBorder.BorderBrush = Brushes.DodgerBlue;
                innerBorder.BorderBrush = Brushes.DodgerBlue;

                @char.Foreground = Brushes.DodgerBlue;
                freq.Foreground = Brushes.DodgerBlue;

            }
            else
            {
                outerBorder.Background = Brushes.PaleVioletRed;
                outerBorder.BorderBrush = Brushes.DarkRed;
                innerBorder.BorderBrush = Brushes.DarkRed;

                @char.Foreground = Brushes.DarkRed;
                freq.Foreground = Brushes.DarkRed;
            }

        }

        private void Border_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }
    }
}
