using Chronocinema.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Chronocinema.Services
{
    public interface IUserService
    {
        Task SaveUserMediaAsync(User user);
        Task LoadUserMediaAsync(User user);
        void AddMediaItem(User user, MediaItem mediaItem);
        void RemoveMediaItem(User user, MediaItem mediaItem);
        void UpdateMediaItem(User user, MediaItem mediaItem);
    }

    public class UserService : IUserService
    {
        private static UserService _instance;
        public static UserService Instance => _instance ?? (_instance = new UserService());
        private UserService() { }
        private void EnsureDataDirectoryExists()
        {
            var dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }
        }

        public async Task SaveUserMediaAsync(User user)
        {
            try
            {
                EnsureDataDirectoryExists();
                var userMediaPath = GetUserMediaFilePath(user.Id);
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(user.MediaItems.ToList(), options);
                await File.WriteAllTextAsync(userMediaPath, json);
            }
            catch (Exception)
            {

            }
        }

        public async Task LoadUserMediaAsync(User user)
        {
            try
            {
                var userMediaPath = GetUserMediaFilePath(user.Id);
                if (File.Exists(userMediaPath))
                {
                    var json = await File.ReadAllTextAsync(userMediaPath);
                    var mediaItems = JsonSerializer.Deserialize<List<MediaItem>>(json) ?? new List<MediaItem>();
                    user.MediaItems = new ObservableCollection<MediaItem>(mediaItems);
                }
                else
                {
                    user.MediaItems = new ObservableCollection<MediaItem>();
                }
            }
            catch (Exception)
            {
                user.MediaItems = new ObservableCollection<MediaItem>();
            }
        }

        public void AddMediaItem(User user, MediaItem mediaItem)
        {
            if (user.MediaItems.All(m => m.Id != mediaItem.Id))
            {
                user.MediaItems.Add(mediaItem);
                _ = SaveUserMediaAsync(user);
            }
        }

        public void RemoveMediaItem(User user, MediaItem mediaItem)
        {
            var itemToRemove = user.MediaItems.FirstOrDefault(m => m.Id == mediaItem.Id);
            if (itemToRemove != null)
            {
                user.MediaItems.Remove(itemToRemove);
                _ = SaveUserMediaAsync(user);
            }
        }

        public void UpdateMediaItem(User user, MediaItem mediaItem)
        {
            var existingItem = user.MediaItems.FirstOrDefault(m => m.Id == mediaItem.Id);
            if (existingItem != null)
            {
                existingItem.Status = mediaItem.Status;
                existingItem.Rating = mediaItem.Rating;
                existingItem.StartDate = mediaItem.StartDate;
                existingItem.EndDate = mediaItem.EndDate;
                existingItem.Notes = mediaItem.Notes;
                _ = SaveUserMediaAsync(user);
            }
        }

        private string GetUserMediaFilePath(int userId)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", $"user_{userId}_media.json");
        }
    }
}
