using Chronocinema.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Chronocinema.Views
{
    /// <summary>
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class MainScreen : UserControl
    {
        public MainScreen()
        {
            InitializeComponent();
            Loaded += MainScreen_Loaded;
        }

        private void MainScreen_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateEmptyState();
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.PropertyChanged += (s, args) =>
                {
                    if (args.PropertyName == nameof(MainViewModel.MediaItems))
                    {
                        UpdateEmptyState();
                    }
                };
            }
        }

        private void UpdateEmptyState()
        {
            if (DataContext is MainViewModel viewModel)
            {
                if (viewModel.MediaItems == null || viewModel.MediaItems.Count == 0)
                {
                    EmptyStateBorder.Visibility = Visibility.Visible;
                    ContentScrollViewer.Visibility = Visibility.Collapsed;
                }
                else
                {
                    EmptyStateBorder.Visibility = Visibility.Collapsed;
                    ContentScrollViewer.Visibility = Visibility.Visible;
                }
            }
        }

        private void MovieCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Models.MediaItem mediaItem)
            {
                var viewModel = DataContext as ViewModels.MainViewModel;
                viewModel?.NavigateToDetailCommand.Execute(mediaItem);
            }
        }
    }
}
