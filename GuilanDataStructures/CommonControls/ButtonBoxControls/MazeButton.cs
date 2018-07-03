using GuilanDataStructures.CommonControls.ButtonBoxControls.GeoLines;
using GuilanDataStructures.CommonControls.ButtonBoxControls.GeoPaths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GuilanDataStructures.CommonControls.ButtonBoxControls
{
    public class MazeButton : System.Windows.Controls.Button
    {

        public ResizableButtonBox Father { get; set; }

        public bool IsStartPoint
        {
            get { return (bool)GetValue(IsStartPointProperty); }
            set { SetValue(IsStartPointProperty, value); }
        }

        public bool IsEndPoint
        {
            get { return (bool)GetValue(IsEndPointProperty); }
            set { SetValue(IsEndPointProperty, value); }
        }

        public bool IsBlock
        {
            get { return (bool)GetValue(IsBlockProperty); }
            set { SetValue(IsBlockProperty, value); }
        }

        public bool IsVisited
        {
            get { return (bool)GetValue(IsVisitedProperty); }
            set { SetValue(IsVisitedProperty, value); }
        }

        public MazeButton Previous
        {
            get { return (MazeButton)GetValue(PreviousProperty); }
            set { SetValue(PreviousProperty, value); }
        }

        public MazeButton Next
        {
            get { return (MazeButton)GetValue(NextProperty); }
            set { SetValue(NextProperty, value); }
        }

        public static readonly DependencyProperty NextProperty =
            DependencyProperty.Register("Next", typeof(MazeButton), typeof(MazeButton), null);

        public static readonly DependencyProperty PreviousProperty =
            DependencyProperty.Register("Previous", typeof(MazeButton), typeof(MazeButton), null);

        public static readonly DependencyProperty IsVisitedProperty =
            DependencyProperty.Register("IsVisited", typeof(bool), typeof(MazeButton), null);

        public static readonly DependencyProperty IsBlockProperty =
            DependencyProperty.Register("IsBlock", typeof(bool), typeof(MazeButton), null);

        public static readonly DependencyProperty IsEndPointProperty =
            DependencyProperty.Register("IsEndPoint", typeof(bool), typeof(MazeButton), null);

        public static readonly DependencyProperty IsStartPointProperty =
            DependencyProperty.Register("IsStartPoint", typeof(bool), typeof(MazeButton), null);

        public MazeButton() : base()
        {

            VerticalContentAlignment = VerticalAlignment.Center;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            Padding = new Thickness(0);

            Click += (s, e) =>
            {
                if (!IsStartPoint && !IsEndPoint)
                {
                    IsBlock = (IsBlock) ? false : true;

                    CheckFatherPanelForPath();

                }

            };

            MouseUp += (s, e) =>
            {
                if (e.RightButton == System.Windows.Input.MouseButtonState.Released)
                {
                    if (!IsBlock) {

                        if (IsStartPoint)
                        {
                            IsStartPoint = false;
                            Father.MazeStartPoint = null;
                            ResetFatherScene();
                            Content = "";
                        }
                        else if (IsEndPoint)
                        {
                            IsEndPoint = false;
                            Father.MazeEndPoint = null;
                            ResetFatherScene();
                            Content = "";
                        }
                        else if (!Father.HasEndPoint)
                        {
                            IsEndPoint = true;
                            Father.MazeEndPoint = this;
                            Content = new Image() {
                                Source =  new BitmapImage(
                                    new Uri("/GuilanDataStructures;component/Resources/Images/RedFlag.png", UriKind.Relative)),
                                Height = 18,
                                Width = 18,
                                Stretch = Stretch.None
                            };
                        }
                        else if (!Father.HasStartPoint)
                        {
                            IsStartPoint = true;
                            Father.MazeStartPoint = this;
                            Content = new Image()
                            {
                                Source = new BitmapImage(
                                    new Uri("/GuilanDataStructures;component/Resources/Images/BlueFlag.png", UriKind.Relative)),
                                Height = 18,
                                Width = 18,
                                Stretch = Stretch.None
                            };
                        }

                        CheckFatherPanelForPath();

                    }

                }

            };

        }

        public void ResetFatherScene()
        {
            foreach (var btn in Father.ButtonsList)
            {
                btn.IsVisited = false;
                btn.Previous = null;

                if (!btn.IsStartPoint && !btn.IsEndPoint)
                {
                    btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
                    btn.Content = "";
                }
            }
        }

     public  void CheckFatherPanelForPath()
        {
            if (Father.HasEndPoint && Father.HasStartPoint)
            {

                ResetFatherScene();

                if(Father.UseDFS)
                    Father.CreatePathUsingDFS(Father.MazeStartPoint);
                else
                    Father.CreatePathUsingBFS(Father.MazeStartPoint);

                var temp = Father.MazeEndPoint;
                var tempPrev = Father.MazeEndPoint.Previous;

                while (tempPrev != null && tempPrev != Father.MazeStartPoint)
                {
                    tempPrev.Next = temp;
                    // Use green-background only for bfs
                    if (!Father.UseDFS)
                    {
                        tempPrev.Background = new RadialGradientBrush(
                            (Color)ColorConverter.ConvertFromString("#FF8BFF84"),
                            (Color)ColorConverter.ConvertFromString("#FF7AE573")
                            );
                    }

                    int tempIndex = Father.ButtonsList.IndexOf(tempPrev);
                    int prevIndex = Father.ButtonsList.IndexOf(tempPrev.Previous);
                    int nextIndex = Father.ButtonsList.IndexOf(tempPrev.Next);

                    // Graphical Paths for DFS and BFS
                    if (Father.UseDFS)
                    {

                                if (prevIndex == Father.GetNorth(tempIndex))
                                {
                                    if (nextIndex == Father.GetSouth(tempIndex))
                                    {
                                        tempPrev.Content = new Vertical();
                                    }

                                    else if (nextIndex == Father.GetEast(tempIndex))
                                    {
                                        tempPrev.Content = new LeftToUp();
                                    }

                                    else if (nextIndex == Father.GetWest(tempIndex))
                                    {
                                        tempPrev.Content = new UpToRight();

                                    }
                                }

                                else if (prevIndex == Father.GetSouth(tempIndex))
                                {
                                    if (nextIndex == Father.GetNorth(tempIndex))
                                    {
                                        tempPrev.Content = new Vertical();
                                    }

                                    else if (nextIndex == Father.GetEast(tempIndex))
                                    {
                                        tempPrev.Content = new LeftToDown();
                                    }

                                    else if (nextIndex == Father.GetWest(tempIndex))
                                    {
                                        tempPrev.Content = new RightToDown();

                                    }
                                }

                                else if (prevIndex == Father.GetEast(tempIndex))
                                {
                                    if (nextIndex == Father.GetSouth(tempIndex))
                                    {
                                        tempPrev.Content = new LeftToDown();
                                    }

                                    else if (nextIndex == Father.GetNorth(tempIndex))
                                    {
                                        tempPrev.Content = new LeftToUp();
                                    }

                                    else if (nextIndex == Father.GetWest(tempIndex))
                                    {
                                        tempPrev.Content = new Horizontal();

                                    }
                                }

                                else if (prevIndex == Father.GetWest(tempIndex))
                                {
                                    if (nextIndex == Father.GetSouth(tempIndex))
                                    {
                                        tempPrev.Content = new RightToDown();
                                    }

                                    else if (nextIndex == Father.GetNorth(tempIndex))
                                    {
                                        tempPrev.Content = new UpToRight();
                                    }

                                    else if (nextIndex == Father.GetEast(tempIndex))
                                    {
                                        tempPrev.Content = new Horizontal();

                                    }
                                }

                    }
                    else
                    {
                        if (prevIndex == Father.GetNorth(tempIndex))
                            tempPrev.Content = new Up();
                        else if (prevIndex == Father.GetSouth(tempIndex))
                            tempPrev.Content = new Down();
                        else if (prevIndex == Father.GetEast(tempIndex))
                            tempPrev.Content = new Right();
                        else if (prevIndex == Father.GetWest(tempIndex))
                            tempPrev.Content = new Left();
                    }

                    temp = temp.Previous;
                    tempPrev = tempPrev.Previous;
                }
            }
        }

    }
}
