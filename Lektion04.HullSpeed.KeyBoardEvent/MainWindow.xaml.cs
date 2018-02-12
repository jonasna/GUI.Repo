using System;
using System.Collections.Generic;
using System.Diagnostics;
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


namespace Lektion04.HullSpeed.KeyBoardEvent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Sailboat _sailboat = new Sailboat();
        public MainWindow()
        {
            InitializeComponent();
            KeyDown += MainWindow_KeyDown;
            KeyDown += MainWindow_KeyDown1;
        }

        private void MainWindow_KeyDown1(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                if (Keyboard.IsKeyDown(Key.S))
                {
                    try
                    {
                        this.FontSize -= 2;
                        Debug.WriteLine("MORE");
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                if (Keyboard.IsKeyDown(Key.L))
                {
                    try
                    {
                        this.FontSize += 2;
                        Debug.WriteLine("MORE");
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _sailboat.Name = NameInput.Text;
                _sailboat.Length = double.Parse(Length.Text);

                AnswerBlock.Text = $"{(Math.Round(2 * _sailboat.Hullspeed(), MidpointRounding.AwayFromZero) / 2),6:0.0}";
                BoatList.Items.Add($"{_sailboat.Name,-5}\t{_sailboat.Length,-5}");
            }
            catch {}
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (_sailboat.Name != null)
            {
                MessageBox.Show($"The name of the ship is {_sailboat.Name}");
            }
        }
    }
}
