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

namespace Lektion11.WindowsAndDialogs.Agent
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

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var vmodel = (AgentsViewModel)FindResource("Awm");

            AgentEditView view = new AgentEditView();
            view.Owner = this;

            view.Id = IdTxtBox.Text;
            view.Assignment = AssignTxtBox.Text;
            view.Codename = CodenameTxtBox.Text;
            view.Speciality = SpecialityTxtBox.Text;

            if (view.ShowDialog() == true)
            {
                vmodel.ModifySelected(view.Id, view.Codename, view.Speciality, view.Assignment);
                AgentsDataGrid.Items.Refresh();
                FocusManager.SetFocusedElement(this, EditBtn);
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var vmodel = (AgentsViewModel)FindResource("Awm");
            vmodel.CreateNewAgentCommand.Execute(null);

            var view = new AgentEditView();
            view.Owner = this;        

            if (view.ShowDialog() == true)
            {
                
                vmodel.ModifySelected(view.Id, view.Codename, view.Speciality, view.Assignment);
                AgentsDataGrid.Items.Refresh();
                FocusManager.SetFocusedElement(this, AddBtn);
            }
            else
            {
                vmodel.DeleteAgentCommand.Execute(null);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var vmodel = (AgentsViewModel)FindResource("Awm");

            var result = MessageBox.Show("Are you sure you want to delete the selected item?",
                "Delete Agent",
                MessageBoxButton.YesNo,
                MessageBoxImage.Exclamation);

            if(result == MessageBoxResult.Yes)
                vmodel.DeleteAgentCommand.Execute(null);
        }
    }
}
