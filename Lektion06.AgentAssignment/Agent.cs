using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Lektion06.AgentAssignment.Annotations;

namespace Lektion06.AgentAssignment
{
    public class Agents : ObservableCollection<Agent>
    {
        public Agents()
        {
            
        }

        public static Agents GetAgents(int number)
        {
            var agents = new Agents
            {
                new Agent(number.ToString(), "James Bond", "No where...", "To kill", "Off work"),
                new Agent(number.ToString(), "Mads Madsen", "Rosenvænget 12", "Græsslåning", "Kaffepause")
            };
            return agents;
        }
    };  // Just to reference it from xaml

    [Serializable]
    public class Agent : INotifyPropertyChanged
    {
        string id;
        string codeName;
        string speciality;
        string assignment;

        public Agent()
        {
        }

        public Agent(string aId, string aName, string aAddress, string aSpeciality, string aAssignment)
        {
            id = aId;
            codeName = aName;
            speciality = aSpeciality;
            assignment = aAssignment;
        }

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string CodeName
        {
            get
            {
                return codeName;
            }
            set
            {
                codeName = value;
                OnPropertyChanged();
            }
        }

        public string Speciality
        {
            get
            {
                return speciality;
            }
            set
            {
                speciality = value;
                OnPropertyChanged();
            }
        }

        public string Assignment
        {
            get
            {
                return assignment;
            }
            set
            {
                assignment = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
