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
    public class DetailViewModel : BaseViewModel
    {

        private MediaItem _mediaItem;

        public DetailViewModel(MediaItem mediaItem)
        {
            MediaItem = mediaItem;

            GoBackCommand = new RelayCommand(ExecuteGoBack);
            EditMediaCommand = new RelayCommand(ExecuteEditMedia);
            NavigateToHomeCommand = new RelayCommand(ExecuteNavigateToHome);
            ShowAddMediaCommand = new RelayCommand(ExecuteShowAddMedia);
            NavigateToWatchlistCommand = new RelayCommand(ExecuteNavigateToWatchlist);
        }

        public MediaItem MediaItem
        {
            get => _mediaItem;
            set => SetProperty(ref _mediaItem, value);
        }

        public ICommand GoBackCommand { get; }
        public ICommand EditMediaCommand { get; }
        public ICommand NavigateToHomeCommand { get; }
        public ICommand ShowAddMediaCommand { get; }
        public ICommand NavigateToWatchlistCommand { get; }

        private void ExecuteGoBack()
        {
            NavigationService.Instance.NavigateTo(new MainScreen());
        }

        private void ExecuteEditMedia()
        {
            var editViewModel = new EditViewModel(MediaItem);
            LocatorViewModel.Instance.EditViewModel = editViewModel;
            NavigationService.Instance.NavigateTo(new EditMediaScreen { DataContext = editViewModel });
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
