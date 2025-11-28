using Chronocinema.Services;
using Chronocinema.ViewModels;
using System.Windows;

namespace Chronocinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; }
        private readonly IAuthService _authService;

        public MainWindow()
        {
            InitializeComponent();
            
            _authService = AuthService.Instance;
            ViewModel = LocatorViewModel.Instance.MainViewModel;
            DataContext = ViewModel;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

    }
}