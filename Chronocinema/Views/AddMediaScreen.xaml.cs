using Chronocinema.ViewModels;
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

namespace Chronocinema.Views
{
    /// <summary>
    /// Interaction logic for AddMediaScreen.xaml
    /// </summary>
    public partial class AddMediaScreen : UserControl
    {
        public AddMediaScreen()
        {
            InitializeComponent();
            DataContext = new AddMediaViewModel();
            Loaded += AddMediaScreen_Loaded;
        }

        private void AddMediaScreen_Loaded(object sender, RoutedEventArgs e)
        {
            TitleTextBox.Focus();
        }
    }
}
