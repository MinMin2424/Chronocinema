using Chronocinema.Models;
using Chronocinema.Services;
using Chronocinema.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Chronocinema.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object _currentView;
        private ObservableCollection<MediaItem> _mediaItems;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public MainViewModel()
        {
            _authService = AuthService.Instance;
            _userService = UserService.Instance;

            MediaItems = new ObservableCollection<MediaItem>();

            if (_authService.IsLoggedIn)
            {
                LoadUserMedia(_authService.CurrentUser);
                var mainScreen = new MainScreen();
                mainScreen.DataContext = this;
                CurrentView = mainScreen;
            }
            else
            {
                CurrentView = new LoginScreen();
            }
                //LoadSampleData();

            NavigationService.Instance.ViewChanged += OnViewChanged;

            NavigateToDetailCommand = new RelayCommand<MediaItem>(ExecuteNavigateToDetail);
            NavigateToEditCommand = new RelayCommand<MediaItem>(ExecuteNavigateToEdit);
            NavigateToHomeCommand = new RelayCommand(ExecuteNavigateToHome);
            ShowAddMediaCommand = new RelayCommand(ExecuteShowAddMedia);
            NavigateToWatchlistCommand = new RelayCommand(ExecuteNavigateToWatchlist);
            NavigateToProfileCommand = new RelayCommand(ExecuteNavigateToProfile);
        }

        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public ObservableCollection<MediaItem> MediaItems
        {
            get => _mediaItems;
            set => SetProperty(ref _mediaItems, value);
        }

        public void LoadUserMedia(User user)
        {
            MediaItems.Clear();
            if (user?.MediaItems != null)
            {
                foreach (var item in user.MediaItems)
                {
                    MediaItems.Add(item);
                }
            }
        }

        public void RefreshMediaItems()
        {
            OnPropertyChanged(nameof(MediaItems));
        }

        public ICommand NavigateToDetailCommand { get; }
        public ICommand NavigateToEditCommand { get; }
        public ICommand NavigateToHomeCommand { get; }
        public ICommand ShowAddMediaCommand { get; }
        public ICommand NavigateToWatchlistCommand { get; }
        public ICommand NavigateToProfileCommand { get; }

        private void ExecuteNavigateToDetail(MediaItem mediaItem)
        {
            var detailViewModel = new DetailViewModel(mediaItem);
            LocatorViewModel.Instance.DetailViewModel = detailViewModel;
            NavigationService.Instance.NavigateTo(new DetailScreen { DataContext = detailViewModel});
        }

        private void ExecuteNavigateToEdit(MediaItem mediaItem)
        {
            var editViewModel = new EditViewModel(mediaItem);
            LocatorViewModel.Instance.EditViewModel = editViewModel;
            NavigationService.Instance.NavigateTo(new EditMediaScreen { DataContext = editViewModel });
        }

        private void ExecuteNavigateToHome()
        {
            var mainScreen = new MainScreen();
            mainScreen.DataContext = this;
            NavigationService.Instance.NavigateTo(mainScreen);
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
            NavigationService.Instance.NavigateTo(new ProfileScreen { DataContext = profileViewModel});
        }

        private void OnViewChanged(object view)
        {
            CurrentView = view;
        }

        private void LoadSampleData()
        {
            MediaItems.Add(new MediaItem
            {
                Id = 1,
                Title = "Alien: Romulus",
                Type = MediaType.Movie,
                Status = WatchingStatus.Completed,
                Rating = 9.5,
                Notes = "A thrilling addition to the Alien franchise.",
                StartDate = new DateTime(2024, 10, 1),
                EndDate = new DateTime(2024, 10, 1),
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMDU0NjcwOGQtNjNjOS00NzQ3LWIwM2YtYWVmODZjMzQzN2ExXkEyXkFqcGc@._V1_SX300.jpg",
                Year = "2024",
                Genre = "Horror, Sci-Fi, Thriller",
                Country = "United Kingdom, United States, Hungary, Australia, New Zealand, Canada",
                Language = "English"
            });
            MediaItems.Add(new MediaItem
            {
                Id = 2,
                Title = "Zootopia",
                Type = MediaType.Movie,
                Status = WatchingStatus.Watching,
                Rating = 10,
                Notes = "Judy is the best",
                StartDate = new DateTime(2024, 10, 1),
                EndDate = null,
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BOTMyMjEyNzIzMV5BMl5BanBnXkFtZTgwNzIyNjU0NzE@._V1_SX300.jpg",
                Year = "2016",
                Genre = "Animation, Action, Adventure",
                Country = "United States",
                Language = "English"
            });
            MediaItems.Add(new MediaItem
            {
                Id = 3,
                Title = "WALL E",
                Type = MediaType.Movie,
                Status = WatchingStatus.Completed,
                Rating = 8,
                Notes = "My childhood movie. Loving it fovever.",
                StartDate = new DateTime(2019, 6, 5),
                EndDate = new DateTime(2019, 6, 5),
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjExMTg5OTU0NF5BMl5BanBnXkFtZTcwMjMxMzMzMw@@._V1_SX300.jpg",
                Year = "2008",
                Genre = "Animation, Adventure, Family",
                Country = "United States, Japan",
                Language = "English"
            });
            MediaItems.Add(new MediaItem
            {
                Id = 4,
                Title = "Game of Thrones",
                Type = MediaType.Series,
                Status = WatchingStatus.Paused,
                Rating = 4.5,
                Notes = "Not bad, but not good.",
                StartDate = new DateTime(2019, 6, 5),
                EndDate = new DateTime(2020, 10, 12),
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTNhMDJmNmYtNDQ5OS00ODdlLWE0ZDAtZTgyYTIwNDY3OTU3XkEyXkFqcGc@._V1_SX300.jpg",
                Year = "2011–2019",
                Genre = "Action, Adventure, Drama",
                Country = "United States, United Kingdom",
                Language = "English"
            });
            MediaItems.Add(new MediaItem
            {
                Id = 5,
                Title = "The Conjuring: Last Rites",
                Type = MediaType.Movie,
                Status = WatchingStatus.Completed,
                Rating = 6.5,
                Notes = "Really funny to watch with male friend in the cinema, but it was not scary.",
                StartDate = new DateTime(2025, 10, 22),
                EndDate = new DateTime(2025, 10, 22),
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BM2VmMzRkYzgtMzg2ZC00OTFkLTkwMTYtNTMxNjM2YzI1MjgyXkEyXkFqcGc@._V1_SX300.jpg",
                Year = "2025",
                Genre = "Horror, Mystery, Thriller",
                Country = "United States, United Kingdom, Canada",
                Language = "English"
            });
            MediaItems.Add(new MediaItem
            {
                Id = 6,
                Title = "Breaking Bad",
                Type = MediaType.Series,
                Status = WatchingStatus.Dropped,
                Rating = 3,
                Notes = "No comment.",
                StartDate = new DateTime(2016, 7, 12),
                EndDate = new DateTime(2017, 9, 10),
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMzU5ZGYzNmQtMTdhYy00OGRiLTg0NmQtYjVjNzliZTg1ZGE4XkEyXkFqcGc@._V1_SX300.jpg",
                Year = "2008–2013",
                Genre = "Crime, Drama, Thriller",
                Country = "United States",
                Language = "English, Spanish"
            });
            MediaItems.Add(new MediaItem
            {
                Id = 7,
                Title = "Alien: Romulus",
                Type = MediaType.Movie,
                Status = WatchingStatus.Completed,
                Rating = 9.5,
                Notes = "A thrilling addition to the Alien franchise.",
                StartDate = new DateTime(2024, 10, 1),
                EndDate = new DateTime(2024, 10, 1),
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMDU0NjcwOGQtNjNjOS00NzQ3LWIwM2YtYWVmODZjMzQzN2ExXkEyXkFqcGc@._V1_SX300.jpg",
                Year = "2024",
                Genre = "Horror, Sci-Fi, Thriller",
                Country = "United Kingdom, United States, Hungary, Australia, New Zealand, Canada",
                Language = "English"
            });
            MediaItems.Add(new MediaItem
            {
                Id = 8,
                Title = "Zootopia",
                Type = MediaType.Movie,
                Status = WatchingStatus.Planning,
                Rating = 10,
                Notes = "Judy is the best",
                StartDate = new DateTime(2024, 10, 1),
                EndDate = null,
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BOTMyMjEyNzIzMV5BMl5BanBnXkFtZTgwNzIyNjU0NzE@._V1_SX300.jpg",
                Year = "2016",
                Genre = "Animation, Action, Adventure",
                Country = "United States",
                Language = "English"
            });
            MediaItems.Add(new MediaItem
            {
                Id = 9,
                Title = "WALL E",
                Type = MediaType.Movie,
                Status = WatchingStatus.Completed,
                Rating = 8,
                Notes = "My childhood movie. Loving it fovever.",
                StartDate = new DateTime(2019, 6, 5),
                EndDate = new DateTime(2019, 6, 5),
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjExMTg5OTU0NF5BMl5BanBnXkFtZTcwMjMxMzMzMw@@._V1_SX300.jpg",
                Year = "2008",
                Genre = "Animation, Adventure, Family",
                Country = "United States, Japan",
                Language = "English"
            });
            MediaItems.Add(new MediaItem
            {
                Id = 10,
                Title = "Game of Thrones",
                Type = MediaType.Series,
                Status = WatchingStatus.Planning,
                Rating = 4.5,
                Notes = "Not bad, but not good.",
                StartDate = new DateTime(2019, 6, 5),
                EndDate = new DateTime(2020, 10, 12),
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTNhMDJmNmYtNDQ5OS00ODdlLWE0ZDAtZTgyYTIwNDY3OTU3XkEyXkFqcGc@._V1_SX300.jpg",
                Year = "2011–2019",
                Genre = "Action, Adventure, Drama",
                Country = "United States, United Kingdom",
                Language = "English"
            });
            MediaItems.Add(new MediaItem
            {
                Id = 11,
                Title = "The Conjuring: Last Rites",
                Type = MediaType.Movie,
                Status = WatchingStatus.Completed,
                Rating = 6.5,
                Notes = "Really funny to watch with male friend in the cinema, but it was not scary.",
                StartDate = new DateTime(2025, 10, 22),
                EndDate = new DateTime(2025, 10, 22),
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BM2VmMzRkYzgtMzg2ZC00OTFkLTkwMTYtNTMxNjM2YzI1MjgyXkEyXkFqcGc@._V1_SX300.jpg",
                Year = "2025",
                Genre = "Horror, Mystery, Thriller",
                Country = "United States, United Kingdom, Canada",
                Language = "English"
            });
            MediaItems.Add(new MediaItem
            {
                Id = 12,
                Title = "Breaking Bad",
                Type = MediaType.Series,
                Status = WatchingStatus.Dropped,
                Rating = 3,
                Notes = "No comment.",
                StartDate = new DateTime(2016, 7, 12),
                EndDate = new DateTime(2017, 9, 10),
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMzU5ZGYzNmQtMTdhYy00OGRiLTg0NmQtYjVjNzliZTg1ZGE4XkEyXkFqcGc@._V1_SX300.jpg",
                Year = "2008–2013",
                Genre = "Crime, Drama, Thriller",
                Country = "United States",
                Language = "English, Spanish"
            });
        }
    }
}
