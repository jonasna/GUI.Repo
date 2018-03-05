using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmFoundation.Wpf;

namespace Lektion07.AgentAssignmentTriggersAndStyles
{
    public class Agents : ObservableCollection<Agent>, INotifyPropertyChanged
    {
        #region Commands

        private ICommand _createNewAgenCommand;
        public ICommand CreateNewAgentCommand =>
            _createNewAgenCommand ?? (_createNewAgenCommand = new RelayCommand(
                () =>
                {
                    Add(new Agent());
                    CurrentIndex = Count - 1;
                    NotifyPropertyChanged("Count"); 
                }));


        private ICommand _deleteAgentCommand;
        public ICommand DeleteAgentCommand =>
            _deleteAgentCommand ?? (_deleteAgentCommand = new RelayCommand(
                () =>
                {
                    RemoveAt(CurrentIndex);
                    NotifyPropertyChanged("Count");
                },
                () => Count > 0 && CurrentIndex >= 0));


        private ICommand _nextAgentCommand;
        public ICommand NextAgentCommand =>
            _nextAgentCommand ?? (_nextAgentCommand = new RelayCommand(
                () => CurrentIndex++, 
                () => CurrentIndex != Count -1));


        private ICommand _previousAgentCommand;
        public ICommand PreviousAgentCommand =>
            _previousAgentCommand ??
            (_previousAgentCommand = new RelayCommand(
                () => CurrentIndex--,
                () => CurrentIndex > 0));

        #endregion

        #region Properties
        private int _currentIndex = -1;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (_currentIndex != value)
                {
                    _currentIndex = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        public new event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string memberName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

    };  // Just to reference it from xaml
}
