using Chronocinema.Models;
using Chronocinema.Services;
using Chronocinema.Views;
using Chronocinema.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Chronocinema.ViewModels
{
    public class EditViewModel : BaseViewModel
    {
        private MediaItem _mediaItem;
        private MediaItem _originalMediaItem;

        public EditViewModel(MediaItem mediaItem)
        {
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
            var detailViewModel = new DetailViewModel(MediaItem);
            LocatorViewModel.Instance.DetailViewModel = detailViewModel;
            NavigationService.Instance.NavigateTo(new DetailScreen { DataContext = detailViewModel});
        }
        private void ExecuteCancel()
        {
            var detailViewModel = new DetailViewModel(_originalMediaItem);
            LocatorViewModel.Instance.DetailViewModel = detailViewModel;
            NavigationService.Instance.NavigateTo(new DetailScreen { DataContext = detailViewModel});
        }
    }
}
