using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GuilanDataStructures
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static MainWindow MainWindowApp { get; set; }
        public static Projects.Project2.MainHost Project2MainHost { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fa-IR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("fa-IR");
        }
    }
}
