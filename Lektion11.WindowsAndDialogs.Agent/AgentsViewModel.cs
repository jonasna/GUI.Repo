using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MvvmFoundation.Wpf;

namespace Lektion11.WindowsAndDialogs.Agent
{
    public class AgentsViewModel : ObservableCollection<Agent>, INotifyPropertyChanged
    {
        public AgentsViewModel()
        {
            Add(new Agent("25", "Jonas Andersen", "Super smooth", "Has to buy candy asdasdasdasdasdasdasdasd"));
            Add(new Agent("661", "Bella", "Super dejlig", "Skal også hente slik"));
        }

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
                () => CurrentIndex != Count - 1));
        
        private ICommand _previousAgentCommand;
        public ICommand PreviousAgentCommand =>
            _previousAgentCommand ??
            (_previousAgentCommand = new RelayCommand(
                () => CurrentIndex--,
                () => CurrentIndex > 0));

        #endregion

        #region Functions

        public void ModifySelected(string Id, string codename, string speciality, string assignment)
        {
            if (CurrentIndex == -1)
                return;

            var agent = Items[CurrentIndex];

            Items[CurrentIndex].ID = Id;
            agent.CodeName = codename;
            agent.Speciality = speciality;
            agent.Assignment = assignment;
        }

        #endregion

        #region Properties
        private int _currentIndex = -1;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (_currentIndex == value) return;
                _currentIndex = value;
                NotifyPropertyChanged();
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