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

namespace Lektion06.AgentAssignment.Delopgave3
{
    public class Agents : ObservableCollection<Agent>, INotifyPropertyChanged
    {
        #region Commands

        private ICommand _createNewAgenCommand;
        public ICommand CreateNewAgentCommand => 
            _createNewAgenCommand ?? (_createNewAgenCommand = new RelayCommand(CreateNewAgentHandler));

        private void CreateNewAgentHandler()
        {
            Add(new Agent());
            CurrentIndex = Count - 1;
        }

        private ICommand _deleteAgentCommand;
        public ICommand DeleteAgentCommand => _deleteAgentCommand ?? (_deleteAgentCommand = new RelayCommand(DeleteAgentHandler, DeleteAgentHandler_CanExecute));

        private void DeleteAgentHandler()
        {
            RemoveAt(CurrentIndex);
        }

        private bool DeleteAgentHandler_CanExecute()
        {
            return (Count > 0 && CurrentIndex >= 0);
        }

        private ICommand _nextAgentCommand;
        public ICommand NextAgentCommand => _nextAgentCommand ?? (_nextAgentCommand = new RelayCommand(NextAgentHandler, NextAgentHandler_CanExecute));

        private void NextAgentHandler()
        {
            CurrentIndex++;
        }

        private bool NextAgentHandler_CanExecute()
        {
            return (CurrentIndex != Count - 1);
        }

        private ICommand _previousAgentCommand;

        public ICommand PreviousAgentCommand =>
            _previousAgentCommand ??
            (_previousAgentCommand = new RelayCommand(() => CurrentIndex--, () => CurrentIndex > 0));

        #endregion

        public static Agents GetAgents(int number)
        {
            var agents = new Agents
            {
                new Agent(number.ToString(), "James Bond", "No where...", "To kill", "Off work"),
                new Agent(number.ToString(), "Mads Madsen", "Rosenvænget 12", "Græsslåning", "Kaffepause")
            };
            return agents;
        }

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

        private void NotifyPropertyChanged([CallerMemberName] string membername = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(membername));
        }

    };  // Just to reference it from xaml
}
