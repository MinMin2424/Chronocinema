using Chronocinema.ViewModels;
using System.Windows;
using System.Windows.Controls;

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
