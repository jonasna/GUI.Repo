using System;
using System.Collections.Generic;
using System.IO;
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
using MvvmFoundation.Wpf;
using Newtonsoft.Json;

namespace Lektion06.AgentAssignment.Delopgave3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveAsCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var agents = (Agents)FindResource("_Agents");
            var writer = new StreamWriter("TestDoc");
            var serializer = new JsonSerializer();
            serializer.Serialize(writer, agents);
            writer.Close();
        }

        #region Outdated functionality
        private void BckButton_Click(object sender, RoutedEventArgs e)
        {
            if (AgentsListBox.SelectedIndex > 0)
                AgentsListBox.SelectedIndex--;
        }

        private void FwdButton_Click(object sender, RoutedEventArgs e)
        {
            if (AgentsListBox.SelectedIndex < AgentsListBox.Items.Count)
                AgentsListBox.SelectedIndex++;
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            Agents agents = (Agents)this.FindResource("_Agents");
            agents.Add(new Agent());
            AgentsListBox.SelectedIndex = AgentsListBox.Items.Count - 1;
            IdBox.Focus();
        }
        #endregion
        
    }
}
