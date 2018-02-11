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
using GuilanDataStructures;

namespace GuilanDataStructures.Projects.Project1
{
    /// <summary>
    /// Interaction logic for BoresheMostatil.xaml
    /// </summary>
    public partial class BoresheMostatil : Page
    {
        public BoresheMostatil()
        {
            InitializeComponent();
        }

        #region Code Area :: Draw Graphical Rectangles
        private void DrawVisualRectangles(string[] token = null)
        {
            try
            {
                visualView.Children.Clear();

                // If there is no data
                if (token == null || token[0] == string.Empty)
                {
                    // Create a simple text message to display there is no data to draw
                    var inputIsEmpty = new TextBlock()
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Text = "داده ای برای ترسیم بصری در دسترس نیست",
                        Foreground = Brushes.DodgerBlue
                    };
                    visualView.Children.Add(inputIsEmpty);
                }
                // If there is data to draw
                else
                {
                    // Create a simple text message to display rectangles are being drawn
                    var rendering = new TextBlock()
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Text = "در حال ترسیم...",
                        Foreground = Brushes.DodgerBlue
                    };

                    var rects = new List<Rectangle>();

                    int[] numbers = new int[token.Length];

                    // Parse all string values to integer ones
                    numbers = Array.ConvertAll(token, int.Parse);

                    // Finding maximum height. Needed to calculate height rectangles can fit to the viewport
                    int maxHeight = numbers.Max();

                    double maximumView = visualView.ActualHeight;

                    double ratio = maximumView / maxHeight;

                    // Add rectangles one by one
                    for (int i = 0; i < token.Length; i++)
                    {
                        rects.Add(new Rectangle()
                        {
                            Height = (double.Parse(token[i]) * ratio),
                            Width = 20,
                            VerticalAlignment = VerticalAlignment.Bottom,
                            Stroke = Brushes.Gray
                        });
                    }
                    visualView.Children.Remove(rendering);
                    foreach (var rect in rects) visualView.Children.Add(rect);

                }
            }
            catch (Exception ex)
            {
                visualView.Children.Clear();

                // Create a simple text message to display there is an error in your input
                var errorDrawing = new TextBlock()
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = Brushes.DarkRed,
                    Text = "خطا در ترسیم"
                };
                visualView.Children.Add(errorDrawing);
            }
           
        }
        #endregion

        private void CalculationProcess()
        {
            try
            {
                string[] token = inputTextBox.Text.TrimStart(' ').TrimEnd(' ').Split(' ');
                int tokenLen = token.Length;

              // Async operation to draw visual rectangles using a single-thread async task
              Task.Run(() =>
              {
                Dispatcher.Invoke(() => { DrawVisualRectangles(token); });
              });
                

              int maxArea = CalculateMaximumArea(Array.ConvertAll(token, int.Parse));


              resultTextBox.Background = Brushes.White;
              resultTextBox.Text = maxArea.ToString();

            }
            catch (Exception ex)
            {
                try
                {
                    resultTextBox.Background = Brushes.LightPink;
                    resultTextBox.Text = "خطا در محاسبه";
                }
                catch { }

            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(inputTextBox.Text == string.Empty)
            {
                resultTextBox.Background = Brushes.LightYellow;
                resultTextBox.Text = "لطفا مقداری را وارد نمایید";

                // Dispaly there is no data to preview in viewport
                Task.Run(() =>
                {
                    Dispatcher.Invoke(() => { DrawVisualRectangles(null); });
                });

            }
            else CalculationProcess();


        }

        private int CalculateMaximumArea(int[] heights)
        {
            // Stack to hold Index to Values
            var stack = new Stack<int>();

            // Number of elements in Heights Array
            var count = heights.Length;

            // Result to be returned
            int result = 0;

            // Itrate in heights
            int itrator = 0;

            while(itrator < count)
            {
                /*
                 * Index of Itrator should be pushed in stack in only two conditions:
                 * 1- Stack is empty
                 * 2- Heigh of current item is higher than element with index of top of the stack
                 */
                if (stack.IsEmpty() || heights[itrator] >= heights[stack.Peek()]) stack.Push(itrator++);
                /*
                 * If none of above conditions are true, it's time to calculate created areas
                 */
                else
                {
                    int popedValue = stack.Pop();

                    int calculationFactor = (stack.IsEmpty()) ? itrator : (itrator - stack.Peek() - 1);

                    int area = heights[popedValue] * calculationFactor;

                    if (area > result) result = area;
                   
                }
            }
            // Recalculate un-empty stack using method above in "else" block
            while (!stack.IsEmpty())
            {
                int popedValue = stack.Pop();

                int calculationFactor = (stack.IsEmpty()) ? itrator : (itrator - stack.Peek() - 1);

                int area = heights[popedValue] * calculationFactor;

                if (area > result) result = area;
            }

            return result;
        }

        private void resultTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize example text
            inputTextBox.Text = "5 4 3 2 1";
            CalculationProcess();
        }

        private void visualView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            string[] token = inputTextBox.Text.TrimEnd(' ').Split(' ');
            int tokenLen = token.Length;

       
                Task.Run(() =>
                {
                    Dispatcher.Invoke(() => { DrawVisualRectangles(token); });
                });
           
        }
    }
}
