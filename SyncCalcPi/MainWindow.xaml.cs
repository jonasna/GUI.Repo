using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using CalcPiAlgoritm;

namespace SyncCalcPi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorker _backgroundWorker;
        public MainWindow()
        {
            InitializeComponent();
            _backgroundWorker = (BackgroundWorker) FindResource("backgroundWorker");
        }
        private void miFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            // Get input
            int digits;
            if (!Int32.TryParse(tbxDigits.Text, out digits))
            {
                sbiStatus.Content = "Error...";
                return;
            }

            // Update statusbar
            sbiStatus.Content = "Calculating...";

            // Disable button and reset view
            btnCalculate.IsEnabled = false;
            tblkResults.Clear();

            // Update statusbar
            progressBar.Visibility = Visibility.Visible;
            progressBar.Value = 0;
            progressBar.Maximum = (digits + 8) / 9;

            // Get input for worker and start it
            var input = new GetPiDigitsInput(digits);
            _backgroundWorker.RunWorkerAsync(input);
        }

        private void BackGroundWorker_DoWork_CalcPi(object sender, DoWorkEventArgs e)
        {
            var digits = ((GetPiDigitsInput)e.Argument).Digits;

            StringBuilder pi = new StringBuilder("3", digits + 2);

            int numCalc = (digits + 8) / 9;
            string[] results = new string[numCalc];

            if (digits > 0)
            {
                pi.Append(".");
                
                Parallel.For(0, numCalc, i =>
                {
                    int nineDigits = NineDigitsOfPi.StartingAt(i + 1);
                    int digitCount = Math.Min(digits - i, 9);
                    string ds = string.Format("{0:D9}", nineDigits);
                    results[i] = ds.Substring(0, digitCount);
                    var lol = new GetPiDigitsInput(1);
                    _backgroundWorker.ReportProgress(1, lol);
                });

                for (int i = 0; i < numCalc; i++)
                {
                    pi.Append(results[i]);
                }
            }

            e.Result = pi.ToString();
        }

        private void BackgroundWorker_OnProgressChanged_CalcPi(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value += ((GetPiDigitsInput) e.UserState).Digits;
            //progressBar.Value += e.ProgressPercentage;
        }

        private void BackgroundWorker_OnRunWorkerCompleted_CalcPi(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                sbiStatus.Content = "Error...";
            }
            else
            {
                tblkResults.Text = (string) e.Result;
                btnCalculate.IsEnabled = true;
                progressBar.Visibility = Visibility.Hidden;
                sbiStatus.Content = "Ready!";
            }
        }
    }
}
