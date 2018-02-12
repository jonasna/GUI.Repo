using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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

namespace Lektion05.Babynames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<string> _listDecadeTopNamesItems = new ObservableCollection<string>();
        private readonly ObservableCollection<string> _listDecadeItems = new ObservableCollection<string>();
        private readonly List<Babyname> _babynames = new List<Babyname>();
        private readonly string[,] _topDecadeBabyNames = new string[11,10];
        
        public MainWindow()
        {
            InitializeComponent();
            LstDecadeTopNames.ItemsSource = _listDecadeTopNamesItems;
            Loaded += ReadBabyNames;
            Loaded += LoadDecades;
        }
        
        private void LoadDecades(object sender, RoutedEventArgs e)
        {
            LstDecades.ItemsSource = _listDecadeItems;
            for (int i = 0; i < 11; i++)
            {
                _listDecadeItems.Add(((1900 + i * 10)).ToString());
            }
        }

        private void ReadBabyNames(object sender, RoutedEventArgs e)
        {
            FileStream fs = null;
            StreamReader sr = null;

            try
            {
                fs = new FileStream(@"05-babynames.txt", FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, Encoding.Default);

                while (!sr.EndOfStream)
                {
                    _babynames.Add(new Babyname(sr.ReadLine()));
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            foreach (var name in _babynames)
            {
                for (int i = 0; i < 11; i++)
                {
                    if (name.Rank(1900 + i * 10) > 0 && name.Rank(1900 + i * 10) <= 10)
                    {
                        if (_topDecadeBabyNames[i, name.Rank(1900 + i * 10) - 1] == null)
                            _topDecadeBabyNames[i, name.Rank(1900 + i * 10) - 1] = name.Name;
                        else
                            _topDecadeBabyNames[i, name.Rank(1900 + i * 10) - 1] += " and " + name.Name;
                    }
                }
            }
        }

        private void LstDecades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _listDecadeTopNamesItems.Clear();
            IList decades = e.AddedItems;
            string decade = (string)decades[0];
            int index = int.Parse(decade);

            if (index == 2000)
                index = 10;
            else
            {
                index = (index / 10) % 10;
            }
                
            for (int i = 0; i < 10; i++)
            {
                _listDecadeTopNamesItems.Add(_topDecadeBabyNames[index, i]);
            }
        }
    }
}
