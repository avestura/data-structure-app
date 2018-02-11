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

namespace GuilanDataStructures.CommonControls
{
    /// <summary>
    /// Interaction logic for ProjectCard.xaml
    /// </summary>
    public partial class ProjectCard : UserControl
    {

        public event RoutedEventHandler ClickedViewProject;


        public string ProjectTitle
        {
            get { return projectTitle.Text; }
            set { projectTitle.Text = value; }
        }



        public string QuestionDesigner
        {
            get { return questionDesigner.Text; }
            set { questionDesigner.Text = value; }
        }



        public string ProgrammingLanguage
        {
            get { return programmingLanguage.Text; }
            set { programmingLanguage.Text = value; }
        }



        public string Programmer
        {
            get { return programmer.Text; }
            set { programmer.Text = value; }
        }



        public string Tags
        {
            get { return tag.Text; }
            set { tag.Text = value; }
        }



        public ImageSource ImageSource
        {
            get { return image.Source; }
            set { image.Source = value;  }
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ProjectCard), null);

        public static readonly DependencyProperty TagsProperty =
            DependencyProperty.Register("Tags", typeof(string), typeof(ProjectCard), null);

        public static readonly DependencyProperty ProgrammerProperty =
            DependencyProperty.Register("Programmer", typeof(string), typeof(ProjectCard), null);

        public static readonly DependencyProperty ProgrammingLanguageProperty =
            DependencyProperty.Register("ProgrammingLanguage", typeof(string), typeof(ProjectCard), null);

        public static readonly DependencyProperty QuestionDesignerProperty =
            DependencyProperty.Register("QuestionDesigner", typeof(string), typeof(ProjectCard), null);

        public static readonly DependencyProperty ProjectTitleProperty =
            DependencyProperty.Register("ProjectTitle", typeof(string), typeof(ProjectCard), null);



        public ProjectCard()
        {
            InitializeComponent();
        }

        private void viewButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClickedViewProject != null)
                ClickedViewProject(this, e);
        }
    }
}
