using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace GuilanDataStructures.CommonControls.ButtonBoxControls
{
    /// <summary>
    /// Interaction logic for ResizableButtonBox.xaml
    /// </summary>
    public partial class ResizableButtonBox : UserControl
    {
        

        public bool HasStartPoint { get { return MazeStartPoint != null; } }
        public bool HasEndPoint { get { return MazeEndPoint != null; } }

        public int MazeWidth { get; set; }
        public int MazeHeight { get; set; }

        public List<MazeButton> ButtonsList { get; set; } = new List<MazeButton>();

        public MazeButton MazeStartPoint { get; set; }
        public MazeButton MazeEndPoint { get; set; }




        public bool UseDFS
        {
            get { return (bool)GetValue(UseDFSProperty); }
            set { SetValue(UseDFSProperty, value); }
        }

        public static readonly DependencyProperty UseDFSProperty =
            DependencyProperty.Register("UseDFS", typeof(bool), typeof(ResizableButtonBox), null);




        public ResizableButtonBox()
        {
            InitializeComponent();
            
        }

 
        private void mainArea_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Thumb_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            areaText.Visibility = Visibility.Visible;

            MazeStartPoint = null;
            MazeEndPoint = null;

            ButtonsList.Clear();

            mainArea.Children.Clear();
            mainArea.Background = Brushes.LightBlue;


            rect.Stroke = Brushes.Blue;
            rect.StrokeDashArray = new DoubleCollection(new double[] { 4, 4});

      
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            
            int horizontalChange = (int)e.HorizontalChange;
            int verticalChange = (int)e.VerticalChange;

            bool newHorizontalSizeAvailable = (canvas.ActualWidth + horizontalChange) >= 42;
            bool newVerticalSizeAvailable = (canvas.ActualHeight + verticalChange ) >= 42;

            if (newHorizontalSizeAvailable)
            {
                canvas.Width += horizontalChange;
                Canvas.SetLeft(areaText, (canvas.ActualWidth - areaText.ActualWidth)/2d);
                areaText.Text = $"{(int)canvas.ActualWidth / 20} در {(int)canvas.ActualHeight / 20}";


            }
            if (newVerticalSizeAvailable)
            {
                canvas.Height += verticalChange;
                Canvas.SetTop(areaText, (canvas.ActualHeight - areaText.ActualHeight) / 2d);
                areaText.Text = $"{(int)canvas.ActualWidth / 20} در {(int)canvas.ActualHeight / 20}";


            }
        }


        public void RecreateButtons(int buttons)
        {
            mainArea.Background = Brushes.White;

            for (int i = 0; i < buttons; i++)
            {

                var createdButton = new MazeButton()
                {
                    Width = 20,
                    Height = 20,
                    IsBlock = false,
                    IsEndPoint = false,
                    IsStartPoint = false,
                    IsVisited = false,
                    Father = this,
                    //Content = i
                };
                ButtonsList.Add(createdButton);
                mainArea.Children.Add(createdButton);
            }


        }

        private void Thumb_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            areaText.Visibility = Visibility.Hidden;
            rect.Stroke = Brushes.Gray;
            rect.StrokeDashArray = new DoubleCollection() { 21 };

            int horizontalButtonSpace = ((int)canvas.ActualWidth / 20);
            int verticalButtonSpace = ((int)canvas.ActualHeight / 20);

            canvas.Width = (horizontalButtonSpace * 20d) + 2d;
            canvas.Height = (verticalButtonSpace * 20d) + 2d;

            MazeWidth = horizontalButtonSpace;
            MazeHeight = verticalButtonSpace;

            RecreateButtons(horizontalButtonSpace * verticalButtonSpace);
        }

        private void Thumb_DragOver(object sender, DragEventArgs e)
        {
            
        }

        
        public void CreatePathUsingDFS(MazeButton mazeButton)
        {
            mazeButton.IsVisited = true;

            int buttonIndex = ButtonsList.IndexOf(mazeButton);
            
            foreach (var btn in GetAdjacent(buttonIndex))
            {
                if (!btn.IsVisited && !btn.IsBlock)
                {
                    btn.Previous = mazeButton;
                    CreatePathUsingDFS(btn);

                }

            }

            

        }

        public void CreatePathUsingBFS(MazeButton mazeButton)
        {
            var queue = new Queue<MazeButton>();

            mazeButton.IsVisited = true;
            queue.Enqueue(mazeButton);
            while(queue.Count != 0)
            {
                var e = queue.Dequeue();
                int buttonIndex = ButtonsList.IndexOf(e);

                foreach (var btn in GetAdjacent(buttonIndex))
                {
                    if (btn == MazeEndPoint)
                    {
                        MazeEndPoint.Previous = e;
                        return;
                    }
                    if (!btn.IsVisited && !btn.IsBlock)
                    {
                        btn.Previous = e;
                        btn.IsVisited = true;
                      
                        queue.Enqueue(btn);
                       
                    }
                }
            }
        }


        public bool HasNorth(int index) { return index >= MazeWidth; }
        public int GetNorth(int index) { return index - MazeWidth; }
        public bool HasSouth(int index) { return (index / MazeWidth) < MazeHeight - 1 ; }
        public int GetSouth(int index) { return index + MazeWidth; }
        public bool HasWest(int index) { return (index + 1) % MazeWidth != 0; }
        public int GetWest(int index) { return index + 1; }
        public bool HasEast(int index) { return index % MazeWidth != 0; }
        public int GetEast(int index) { return index - 1; }
        public MazeButton[] GetAdjacent(int index)
        {
            var adj = new List<MazeButton>();
            if (HasWest(index)) adj.Add(ButtonsList[GetWest(index)]);
            if (HasNorth(index)) adj.Add(ButtonsList[GetNorth(index)]);
            if (HasEast(index)) adj.Add(ButtonsList[GetEast(index)]);
            if (HasSouth(index)) adj.Add(ButtonsList[GetSouth(index)]);

            //MessageBox.Show(GetEast(index).ToString());
            //MessageBox.Show(GetWest(index).ToString());
            //MessageBox.Show(GetNorth(index).ToString());
            //MessageBox.Show(GetSouth(index).ToString());

            return adj.ToArray();
        }


    }



    public class HeighDecreaser : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                int val = (int)((double)value);
                return val - 2;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
            int val = (int)((double)value);
            return val + 2;
            }
        }



}
