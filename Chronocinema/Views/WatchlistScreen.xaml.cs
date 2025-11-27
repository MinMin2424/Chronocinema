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
    /// Interaction logic for WatchlistScreen.xaml
    /// </summary>
    public partial class WatchlistScreen : UserControl
    {
        public WatchlistScreen()
        {
            InitializeComponent();
            DataContext = new WatchlistViewModel();
            Loaded += WatchlistScreen_Loaded;
        }

        private void WatchlistScreen_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateEmptyState();
        }

        private void UpdateEmptyState()
        {
            var viewModel = DataContext as WatchlistViewModel;
            if (viewModel?.WatchlistItems?.Count == 0)
            {
                EmptyStateBorder.Visibility = Visibility.Visible;
            }
            else
            {
                EmptyStateBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void MovieCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Models.MediaItem mediaItem)
            {
                var viewModel = DataContext as WatchlistViewModel;
                viewModel?.NavigateToDetailCommand.Execute(mediaItem);
            }
        }
    }
}
