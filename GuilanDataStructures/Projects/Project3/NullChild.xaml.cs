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

namespace GuilanDataStructures.Projects.Project3
{
    /// <summary>
    /// Interaction logic for NullChild.xaml
    /// </summary>
    public partial class NullChild : UserControl
    {

        public bool IsLeft = false;

        public NullChild(bool isLeft)
        {
            InitializeComponent();

            if (isLeft)
            {
                textBlock.Text = "Without Left";
                control.Margin = new Thickness(-15, 0, 0, 0);
            }
            else
            {
                textBlock.Text = "Without Right";
                control.Margin = new Thickness(-10, 0, 0, 0);
            }
        }
    }
}
