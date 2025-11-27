using Chronocinema.Models;
using Chronocinema.Services;
using Chronocinema.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chronocinema.ViewModels
{
    public class WatchlistViewModel : BaseViewModel
    {
        private ObservableCollection<MediaItem> _watchlistItems;

        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public WatchlistViewModel()
        {
            _authService = AuthService.Instance;
            _userService = UserService.Instance;

            LoadWatchlistItems();
            NavigateToDetailCommand = new RelayCommand<MediaItem>(ExecuteNavigateToDetail);
            NavigateToHomeCommand = new RelayCommand(ExecuteNavigateToHome);
            ShowAddMediaCommand = new RelayCommand(ExecuteShowAddMedia);
            NavigateToWatchlistCommand = new RelayCommand(ExecuteNavigateToWatchlist);
            NavigateToProfileCommand = new RelayCommand(ExecuteNavigateToProfile);

            _authService.AuthStateChanged += OnAuthStateChanged;
        }

        public ObservableCollection<MediaItem> WatchlistItems
        {
            get => _watchlistItems;
            set => SetProperty(ref _watchlistItems, value);
        }

        public ICommand NavigateToDetailCommand { get; }
        public ICommand NavigateToHomeCommand { get; }
        public ICommand ShowAddMediaCommand { get; }
        public ICommand NavigateToWatchlistCommand { get; }
        public ICommand NavigateToProfileCommand { get; }

        public void RefreshWatchlist()
        {
            LoadWatchlistItems();
        }

        private void LoadWatchlistItems()
        {
            if (_authService.IsLoggedIn && _authService.CurrentUser?.MediaItems != null)
            {
                var planningItems = _authService.CurrentUser.MediaItems
                    .Where(item => item.Status == WatchingStatus.Planning)
                    .ToList();
                WatchlistItems = new ObservableCollection<MediaItem>(planningItems);
            }
            else
            {
                WatchlistItems = new ObservableCollection<MediaItem>();
            }
        }

        private void ExecuteNavigateToDetail(MediaItem item)
        {
            if (item != null)
            {
                var detailViewModel = new DetailViewModel(item);
                LocatorViewModel.Instance.DetailViewModel = detailViewModel;
                NavigationService.Instance.NavigateTo(new DetailScreen { DataContext = detailViewModel });
            }
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

        private void ExecuteNavigateToProfile()
        {
            var profileViewModel = new ProfileViewModel();
            NavigationService.Instance.NavigateTo(new ProfileScreen { DataContext = profileViewModel });
        }

        private void OnAuthStateChanged(object sender, EventArgs e)
        {
            LoadWatchlistItems();
        }
    }
}
