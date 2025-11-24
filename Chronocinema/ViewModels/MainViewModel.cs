using Chronocinema.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Chronocinema.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private ObservableCollection<MediaItem> _mediaItems;
        private MediaItem _selectedMediaItem;
        private string _searchQuery;
        private MediaItem _searchResult;
        private bool _isSearchVisible;
        private bool _isEditVisible;

        public MainViewModel()
        {
            MediaItems = new ObservableCollection<MediaItem>();
            SearchCommand = new RelayCommand(ExecuteSearch, CanExecuteSearch);
            AddMediaItemCommand = new RelayCommand(ExecuteAddMediaItem, CanExecuteAddMedia);
            EditMediaItemCommand = new RelayCommand(ExecuteEditMediaItem, CanExecuteEditMedia);
            DeleteMediaItemCommand = new RelayCommand(ExecuteDeleteMediaItem, CanExecuteDeleteMedia);
            SaveMediaItemCommand = new RelayCommand(ExecuteSaveMediaItem);
            CancelEditCommand = new RelayCommand(ExecuteCancelEdit);
            ShowSearchCommand = new RelayCommand(ExecuteShowSearch);
            CancelSearchCommand = new RelayCommand(ExecuteCancelSearch);
        }

        public ObservableCollection<MediaItem> MediaItems
        {
            get => _mediaItems;
            set => SetProperty(ref _mediaItems, value);
        }

        public MediaItem SelectedMediaItem
        {
            get => _selectedMediaItem;
            set => SetProperty(ref _selectedMediaItem, value);
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }

        public MediaItem SearchResult
        {
            get => _searchResult;
            set => SetProperty(ref _searchResult, value);
        }

        public bool IsSearchVisible
        {
            get => _isSearchVisible;
            set => SetProperty(ref _isSearchVisible, value);
        }

        public bool IsEditVisible
        {
            get => _isEditVisible;
            set => SetProperty(ref _isEditVisible, value);
        }

        public ICommand SearchCommand { get; }
        public ICommand AddMediaItemCommand { get; }
        public ICommand EditMediaItemCommand { get; }
        public ICommand DeleteMediaItemCommand { get; }
        public ICommand SaveMediaItemCommand { get; }
        public ICommand CancelEditCommand { get; }
        public ICommand ShowSearchCommand { get; }
        public ICommand CancelSearchCommand { get; }

        public ICommand NavigateHomeCommand { get; set; }
        public ICommand ShowAddMediaCommand { get; set; }
        public ICommand NavigateWatchlistCommand { get; set; }


        private bool CanExecuteSearch(object parameter) => !string.IsNullOrWhiteSpace(SearchQuery);
        private bool CanExecuteAddMedia(object parameter) => SearchResult != null;
        private bool CanExecuteEditMedia(object parameter) => SelectedMediaItem != null;
        private bool CanExecuteDeleteMedia(object parameter) => SelectedMediaItem != null;
        private async void ExecuteSearch()
        {
            try
            {
                var result = await OmdbApiService.SearchMedia(SearchQuery);
                if (result != null)
                {
                    SearchResult = result;
                }
                else
                {
                    MessageBox.Show("No results found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while searching: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ExecuteAddMediaItem()
        {
            if (SearchResult != null)
            {
                SearchResult.Id = MediaItems.Count + 1;
                MediaItems.Add(SearchResult);
                ExecuteCancelSearch();
                MessageBox.Show("Media item added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ExecuteEditMediaItem()
        {
            if (SelectedMediaItem != null)
            {
                IsEditVisible = true;
            }
        }
        private void ExecuteDeleteMediaItem()
        {
            if (SelectedMediaItem != null && MessageBox.Show("Do you really want to delete this media?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MediaItems.Remove(SelectedMediaItem);
                MessageBox.Show("Media item deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ExecuteSaveMediaItem()
        {
            IsEditVisible = false;
            MessageBox.Show("Media item updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ExecuteCancelEdit()
        {
            IsEditVisible = false;
        }
        private void ExecuteShowSearch()
        {
            IsSearchVisible = true;
            SearchQuery = string.Empty;
            SearchResult = null;
        }
        private void ExecuteCancelSearch()
        {
            IsSearchVisible = false;
            SearchQuery = string.Empty;
            SearchResult = null;
        }
    }
}
