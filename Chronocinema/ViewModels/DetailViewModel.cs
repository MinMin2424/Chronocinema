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
        private bool _showDeleteDialog;

        public DetailViewModel(MediaItem mediaItem)
        {
            MediaItem = mediaItem;

            GoBackCommand = new RelayCommand(ExecuteGoBack);
            EditMediaCommand = new RelayCommand(ExecuteEditMedia);
            DeleteMediaCommand = new RelayCommand(ExecuteDeleteMedia);
            ConfirmDeleteCommand = new RelayCommand(ExecuteConfirmDelete);
            CancelDeleteCommand = new RelayCommand(ExecuteCancelDelete);
            NavigateToHomeCommand = new RelayCommand(ExecuteNavigateToHome);
            ShowAddMediaCommand = new RelayCommand(ExecuteShowAddMedia);
            NavigateToWatchlistCommand = new RelayCommand(ExecuteNavigateToWatchlist);
        }

        public MediaItem MediaItem
        {
            get => _mediaItem;
            set => SetProperty(ref _mediaItem, value);
        }

        public bool ShowDeleteDialog
        {
            get => _showDeleteDialog;
            set => SetProperty(ref _showDeleteDialog, value);
        }

        public ICommand GoBackCommand { get; }
        public ICommand EditMediaCommand { get; }
        public ICommand DeleteMediaCommand { get; }
        public ICommand ConfirmDeleteCommand { get; }
        public ICommand CancelDeleteCommand { get; }
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

        private void ExecuteDeleteMedia()
        {
            ShowDeleteDialog = true;
        }

        private void ExecuteConfirmDelete()
        {
            var mainViewModel = LocatorViewModel.Instance.MainViewModel;
            var itemToRemove = mainViewModel.MediaItems.FirstOrDefault(item => item.Id == MediaItem.Id);
            if (itemToRemove != null)
            {
                mainViewModel.MediaItems.Remove(itemToRemove);
            }
            ShowDeleteDialog = false;
            NavigationService.Instance.NavigateTo(new MainScreen());
        }

        private void ExecuteCancelDelete()
        {
            ShowDeleteDialog = false;
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
