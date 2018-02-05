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

namespace Lektion03.HullSpeed
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var name = Name.Text;
            var length = double.Parse(Length.Text);

            var boat = new Sailboat();
            boat.Name = name;
            boat.Length = length;

            AnswerBlock.Text = $"{(Math.Round(2 * boat.Hullspeed(), MidpointRounding.AwayFromZero) / 2),6:0.0}";

            BoatList.Items.Add($"{boat.Name,-5}\t{boat.Length,-5}");
        }
    }
}
