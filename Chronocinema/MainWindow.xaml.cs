using Chronocinema.Models;
using Chronocinema.Services;
using Chronocinema.ViewModels;
using Chronocinema.Views;
using Chronocinema.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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