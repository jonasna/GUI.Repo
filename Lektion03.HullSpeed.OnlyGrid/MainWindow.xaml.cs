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

namespace Lektion03.HullSpeed.OnlyGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Sailboat _sailboat = new Sailboat();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            _sailboat.Name = NameInput.Text;
        }

        private void LengthInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _sailboat.Length = double.Parse(LengthInput.Text);
            }
            catch (FormatException)
            {}
            catch
            {}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AnswerBox.Text = _sailboat.Hullspeed().ToString("F");
            AnswerBox.TextAlignment = TextAlignment.Right;
        }
    }
}
