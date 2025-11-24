using Chronocinema.Models;
using Chronocinema.ViewModels;
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

        public MainWindow()
        {
            InitializeComponent();
            
            ViewModel = new MainViewModel();
            DataContext = ViewModel;

            LoadSampleDate();

            ViewModel.NavigateHomeCommand = new RelayCommand(ExecuteNavigateHome);
            /*ViewModel.ShowAddMediaCommand = new RelayCommand(ExecuteShowAddMedia);
            ViewModel.NavigateWatchlistCommand = new RelayCommand(ExecuteNavigateWatchlist);*/
        }

        private void ExecuteNavigateHome()
        {
            // Navigate to home - already here
        }

        /*private void ExecuteShowAddMedia()
        {
            var addMediaWindow = new AddMediaWindow();
            addMediaWindow.Owner = this;
            addMediaWindow.ShowDialog();
        }

        private void ExecuteNavigateWatchlist()
        {
            var watchlistWindow = new WatchlistWindow();
            watchlistWindow.Owner = this;
            watchlistWindow.ShowDialog();
        }*/

        private void LoadSampleDate()
        {
            ViewModel.MediaItems = new ObservableCollection<MediaItem>
            {
                new MediaItem
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
                },
                new MediaItem
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
                },
                new MediaItem
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
                },
                new MediaItem
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
                },
                new MediaItem
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
                },
                new MediaItem
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
                },
                new MediaItem
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
                },
                new MediaItem
                {
                    Id = 8,
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
                },
                new MediaItem
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
                },
                new MediaItem
                {
                    Id = 10,
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
                },
                new MediaItem
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
                },
                new MediaItem
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
                }
            };
        }
    }
}