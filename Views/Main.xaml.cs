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
using System.Windows.Shapes;

namespace CryptoProject.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();

            Loaded += Main_Loaded;
        }
        /// <summary>
        /// Loads main page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            Frame.NavigationService.Navigate(new MainPage());
        }
    }
}
