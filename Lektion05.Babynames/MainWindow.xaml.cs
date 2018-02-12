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
        private readonly List<Babyname> _babynames = new List<Babyname>();
        private readonly List<Babyname>[] _topTenBabyNamesDecade = new List<Babyname>[11];
        private Babyname _searchTarget = null;
        
        public MainWindow()
        {
            InitializeComponent();
            LstDecadeTopNames.ItemsSource = _listDecadeTopNamesItems;
            Loaded += ReadBabyNames;
            Loaded += LoadDecades;
        }
        
        private void LoadDecades(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 11; i++)
            {
                LstDecades.Items.Add(((1900 + i * 10)).ToString());
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
                sr?.Close();
                fs?.Close();
            }

            LoadTop10NamesForEachDecade();
        }

        private void LoadTop10NamesForEachDecade()
        {
            for (int i = 0; i < 11; i++)
            {
                _topTenBabyNamesDecade[i] = _babynames.
                    Where(name => (name.Rank(1900 + 10 * i) > 0 && name.Rank(1900 + 10 * i) <= 10)).
                    OrderBy(name => name.Rank(1900 + 10 * i)).
                    ToList();
            }
        }

        private void LstDecades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _listDecadeTopNamesItems.Clear();

            var list = (ListBox) e.Source;
            var index = list.SelectedIndex;

            for (int i = 0; i < 20; i += 2)
            {
                _listDecadeTopNamesItems.Add($"{i/2 + 1} {_topTenBabyNamesDecade[index][i].Name} and {_topTenBabyNamesDecade[index][i+1].Name}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchTargetRankingListBox.Items.Clear();
                _searchTarget = _babynames.Find(name => (name.Name == NameSearchInputText.Text));
                AvgRankingText.Text = _searchTarget.AverageRank().ToString();
                TrendText.Text = _searchTarget.Trend() > 0 ? "More popular" : "Less popular";

                for (int i = 0; i < 11; i++)
                {
                    var decade = (1900 + 10 * i).ToString() + "   ";
                    decade += _searchTarget.Rank(1900 + 10 * i).ToString();
                    SearchTargetRankingListBox.Items.Add(decade);
                }

            }
            catch (Exception)
            {
                NameSearchInputText.Text = "Name not found";
            }
        }

    }
}
