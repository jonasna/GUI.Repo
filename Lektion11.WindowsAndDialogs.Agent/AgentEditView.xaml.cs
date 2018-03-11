using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lektion11.WindowsAndDialogs.Agent
{
    /// <summary>
    /// Interaction logic for AgentEditView.xaml
    /// </summary>
    public partial class AgentEditView : Window
    {

        public class ViewData : INotifyPropertyChanged
        {
            private string _id;
            private string _codename;
            private string _speciality;
            private string _assignment;

            #region Props
            public string Id
            {
                get => _id;
                set
                {
                    if (_id != value)
                    {
                        _id = value;
                        OnPropertyChanged();
                    }
                }
            }
            public string Codename
            {
                get => _codename;
                set
                {
                    if (_codename != value)
                    {
                        _codename = value;
                        OnPropertyChanged();
                    }
                }
            }
            public string Speciality
            {
                get => _speciality;
                set
                {
                    if (_speciality != value)
                    {
                        _speciality = value;
                        OnPropertyChanged();
                    }
                }
            }
            public string Assignment
            {
                get => _assignment;
                set
                {
                    if (_assignment != value)
                    {
                        _assignment = value;
                        OnPropertyChanged();
                    }
                }
            }
            #endregion

            #region PChanged

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            #endregion
        }

        private readonly ViewData _data = new ViewData();

        #region Props

        public string Id
        {
            get => _data.Id;
            set => _data.Id = value;
        }

        public string Codename
        {
            get => _data.Codename;
            set => _data.Codename = value;
        }

        public string Speciality
        {
            get => _data.Speciality;
            set => _data.Speciality = value;
        }

        public string Assignment
        {
            get => _data.Assignment;
            set => _data.Assignment = value;
        }

        #endregion

        #region Validation

        public static bool ValidateBindings(DependencyObject parent)
        {
            // Validate all the bindings on the parent
            bool valid = true;
            LocalValueEnumerator localValues = parent.GetLocalValueEnumerator();
            while (localValues.MoveNext())
            {
                LocalValueEntry entry = localValues.Current;
                if (BindingOperations.IsDataBound(parent, entry.Property))
                {
                    Binding binding = BindingOperations.GetBinding(parent, entry.Property);
                    foreach (ValidationRule rule in binding.ValidationRules)
                    {
                        // TODO: where to get correct culture info?
                        ValidationResult result = rule.Validate(parent.GetValue(entry.Property), null);
                        if (!result.IsValid)
                        {
                            BindingExpression expression = BindingOperations.GetBindingExpression(parent, entry.Property);
                            Validation.MarkInvalid(expression, new ValidationError(rule, expression, result.ErrorContent, null));
                            valid = false;
                        }
                    }
                }
            }

            // Validate all the bindings on the children
            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (!ValidateBindings(child)) { valid = false; }
            }

            return valid;
        }

        #endregion

        public AgentEditView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateBindings(this))
            {
                DialogResult = true;
            }           
        }
    }

    public class InputNotNull : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if ((string)value == "")
            {
                return new ValidationResult(false, "Can not be empty!");
            }

            return new ValidationResult(true, null);
        }
    }
}
