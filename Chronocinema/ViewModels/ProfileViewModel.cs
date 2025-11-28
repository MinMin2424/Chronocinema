using Chronocinema.Models;
using Chronocinema.Services;
using Chronocinema.Views;
using System.Windows.Input;

namespace Chronocinema.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private User _currentUser;

        public ProfileViewModel()
        {
            _authService = AuthService.Instance;
            _currentUser = _authService.CurrentUser;

            LogoutCommand = new RelayCommand(ExecuteLogout);
            NavigateToHomeCommand = new RelayCommand(ExecuteNavigateToHome);
            ShowAddMediaCommand = new RelayCommand(ExecuteShowAddMedia);
            NavigateToWatchlistCommand = new RelayCommand(ExecuteNavigateToWatchlist);

            _authService.AuthStateChanged += OnAuthStateChanged;
        }

        public User CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        public int AddedMediasCount => CurrentUser?.MediaItems?.Count ?? 0;
        public int PlanToWatchCount => CurrentUser?.MediaItems?.Count(m => m.Status == WatchingStatus.Planning) ?? 0;

        public ICommand LogoutCommand { get; }
        public ICommand NavigateToHomeCommand { get; }
        public ICommand ShowAddMediaCommand { get; }
        public ICommand NavigateToWatchlistCommand { get; }

        private void ExecuteLogout()
        {
            _authService.Logout();
            NavigationService.Instance.NavigateTo(new LoginScreen());
        }

        private void ExecuteNavigateToHome()
        {
            NavigationService.Instance.NavigateTo(new MainScreen());
        }

        private void ExecuteShowAddMedia()
        {
            NavigationService.Instance.NavigateTo(new AddMediaScreen());
        }

        private void ExecuteNavigateToWatchlist()
        {
            NavigationService.Instance.NavigateTo(new WatchlistScreen());
        }

        private void OnAuthStateChanged(object sender, EventArgs e)
        {
            CurrentUser = _authService.CurrentUser;
            OnPropertyChanged(nameof(AddedMediasCount));
            OnPropertyChanged(nameof(PlanToWatchCount));
        }
    }
}
