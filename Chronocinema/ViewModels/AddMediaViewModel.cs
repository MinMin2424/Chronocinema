using Chronocinema.Services;
using Chronocinema.Views;
using System.Windows.Input;

namespace Chronocinema.ViewModels
{
    public class AddMediaViewModel : BaseViewModel
    {
        private string _title;
        private string _errorMessage;
        private bool _isSearching;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AddMediaViewModel()
        {
            _authService = AuthService.Instance;
            _userService = UserService.Instance;

            SearchCommand = new RelayCommand(async () => await ExecuteSearch(), () => !string.IsNullOrWhiteSpace(Title) && !IsSearching);
            GoBackCommand = new RelayCommand(ExecuteGoBack);
            NavigateToHomeCommand = new RelayCommand(ExecuteNavigateToHome);
            NavigateToWatchlistCommand = new RelayCommand(ExecuteNavigateToWatchlist);
        }

        public string Title
        {
            get => _title;
            set
            {
                SetProperty(ref _title, value);
                ClearErrorMessage();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public bool IsSearching
        { 
            get => _isSearching;
            set
            {
                SetProperty(ref _isSearching, value);
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand NavigateToHomeCommand { get; }
        public ICommand NavigateToWatchlistCommand { get; }

        private async Task ExecuteSearch()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                return;
            }
            IsSearching = true;
            ErrorMessage = string.Empty;
            try
            {
                var mediaItem = await OmdbApiService.SearchMedia(Title);
                if (mediaItem != null)
                {
                    mediaItem.Id = GetNextId();
                    _userService.AddMediaItem(_authService.CurrentUser, mediaItem);

                    LocatorViewModel.Instance.MainViewModel.MediaItems.Add(mediaItem);
                    LocatorViewModel.Instance.MainViewModel.RefreshMediaItems();
                    LocatorViewModel.Instance.WatchlistViewModel.RefreshWatchlist();
                    
                    var detailViewModel = new DetailViewModel(mediaItem);

                    LocatorViewModel.Instance.DetailViewModel = detailViewModel;
                    NavigationService.Instance.NavigateTo(new DetailScreen { DataContext = detailViewModel });
                }
                else
                {
                    ErrorMessage = "Title not found. Please check title and try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred while searching. Please try again.";
            }
            finally
            {
                IsSearching = false;
            }
        }

        private int GetNextId()
        {
            var mainViewModel = LocatorViewModel.Instance.MainViewModel;
            return mainViewModel.MediaItems.Count > 0 ? mainViewModel.MediaItems[^1].Id + 1 : 1;
        }

        private void ClearErrorMessage()
        {
            ErrorMessage = string.Empty;
        }

        private void ExecuteGoBack()
        {
            NavigationService.Instance.NavigateTo(new MainScreen());
        }

        private void ExecuteNavigateToHome()
        {
            NavigationService.Instance.NavigateTo(new MainScreen());
        }

        private void ExecuteNavigateToWatchlist()
        {
            NavigationService.Instance.NavigateTo(new WatchlistScreen());
        }
    }
}
