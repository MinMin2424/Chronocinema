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

        public WatchlistViewModel()
        {
            LoadWatchlistItems();
            NavigateToDetailCommand = new RelayCommand<MediaItem>(ExecuteNavigateToDetail);
            NavigateToHomeCommand = new RelayCommand(ExecuteNavigateToHome);
            ShowAddMediaCommand = new RelayCommand(ExecuteShowAddMedia);
            NavigateToWatchlistCommand = new RelayCommand(ExecuteNavigateToWatchlist);
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

        private void LoadWatchlistItems()
        {
            var mainViewModel = LocatorViewModel.Instance.MainViewModel;
            if (mainViewModel?.MediaItems != null)
            {
                var planningItems = mainViewModel.MediaItems
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
    }
}
