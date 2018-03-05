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
using System.Windows.Threading;
using MvvmFoundation.Wpf;

namespace Lektion07.AgentAssignmentTriggersAndStyles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeLabel.Content = DateTime.Now.ToLongTimeString();
            CommandManager.InvalidateRequerySuggested();
        }

        #region Background color commands

        private ICommand _setColorFunOrangeCommand;
        public ICommand SetColorFunOrangeCommand =>
            _setColorFunOrangeCommand ?? (_setColorFunOrangeCommand = new RelayCommand(
                () => AgentGrid.Background = (LinearGradientBrush)FindResource("BackGroundBrush"),
                () => (LinearGradientBrush)AgentGrid.Background != (LinearGradientBrush)FindResource("BackGroundBrush")));

        private ICommand _setColorFunRedCommand;
        public ICommand SetColorFunRedCommand =>
            _setColorFunRedCommand ?? (_setColorFunRedCommand = new RelayCommand(
                () => AgentGrid.Background = (LinearGradientBrush)FindResource("AnotherBackGroundBrush"),
                () => (LinearGradientBrush)AgentGrid.Background != (LinearGradientBrush)FindResource("AnotherBackGroundBrush")));

        #endregion

        #region Persistance commands

        private ICommand _saveAsCommand;
        public ICommand SaveAsCommand;

        private ICommand _saveCommand;
        public ICommand SaveCommand;

        private ICommand _openCommand;
        public ICommand OpenCommand;

        #endregion

        private void CreateNewAgentBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(IdBox);
        }
    }
}
