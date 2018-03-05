using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Lektion09.Settings.Properties;

namespace Lektion09.Settings
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            if (Lektion09.Settings.Properties.Settings.Default.CallUpgrade)
            {
                Lektion09.Settings.Properties.Settings.Default.Upgrade();
                Lektion09.Settings.Properties.Settings.Default.CallUpgrade = false;
            }

        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            Lektion09.Settings.Properties.Settings.Default.Save();
        }
    }
}
