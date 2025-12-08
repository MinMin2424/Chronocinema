using Chronocinema.Models;
using Chronocinema.Services;
using Chronocinema.Views;
using System.Windows.Input;

namespace Chronocinema.ViewModels
{
    public class EditViewModel : BaseViewModel
    {
        private MediaItem _mediaItem;
        private MediaItem _originalMediaItem;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IToastService _toastService;

        public EditViewModel(MediaItem mediaItem)
        {
            _authService = AuthService.Instance;
            _userService = UserService.Instance;
            _toastService = new ToastService();

            _originalMediaItem = new MediaItem
            {
                Id = mediaItem.Id,
                Title = mediaItem.Title,
                Type = mediaItem.Type,
                PosterUrl = mediaItem.PosterUrl,
                Year = mediaItem.Year,
                Genre = mediaItem.Genre,
                Country = mediaItem.Country,
                Language = mediaItem.Language,
                Status = mediaItem.Status,
                Rating = mediaItem.Rating,
                StartDate = mediaItem.StartDate,
                EndDate = mediaItem.EndDate,
                Notes = mediaItem.Notes
            };

            MediaItem = new MediaItem
            {
                Id = mediaItem.Id,
                Title = mediaItem.Title,
                Type = mediaItem.Type,
                PosterUrl = mediaItem.PosterUrl,
                Year = mediaItem.Year,
                Genre = mediaItem.Genre,
                Country = mediaItem.Country,
                Language = mediaItem.Language,
                Status = mediaItem.Status,
                Rating = mediaItem.Rating,
                StartDate = mediaItem.StartDate,
                EndDate = mediaItem.EndDate,
                Notes = mediaItem.Notes
            }; ;

            SaveCommand = new RelayCommand(ExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel);

            StatusOptions = Enum.GetValues(typeof(WatchingStatus)).Cast<WatchingStatus>().ToList();
        }

        public MediaItem MediaItem
        {
            get => _mediaItem;
            set => SetProperty(ref _mediaItem, value);
        }

        public List<WatchingStatus> StatusOptions { get; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }


        private void ExecuteSave()
        {
            if (_authService.IsLoggedIn)
            {
                _userService.UpdateMediaItem(_authService.CurrentUser, MediaItem);
                var mainViewModel = LocatorViewModel.Instance.MainViewModel;
                var existingItem = mainViewModel.MediaItems.FirstOrDefault(item => item.Id == MediaItem.Id);
                if (existingItem != null)
                {
                    existingItem.Status = MediaItem.Status;
                    existingItem.Rating = MediaItem.Rating;
                    existingItem.StartDate = MediaItem.StartDate;
                    existingItem.EndDate = MediaItem.EndDate;
                    existingItem.Notes = MediaItem.Notes;
                }
                LocatorViewModel.Instance.WatchlistViewModel.RefreshWatchlist();
                _toastService.ShowToast($"Changes were successfully saved!");
            }
            var detailViewModel = new DetailViewModel(MediaItem);
            LocatorViewModel.Instance.DetailViewModel = detailViewModel;
            NavigationService.Instance.NavigateTo(new DetailScreen { DataContext = detailViewModel });
        }
        private void ExecuteCancel()
        {
            var detailViewModel = new DetailViewModel(_originalMediaItem);
            LocatorViewModel.Instance.DetailViewModel = detailViewModel;
            NavigationService.Instance.NavigateTo(new DetailScreen { DataContext = detailViewModel });
        }
    }
}
