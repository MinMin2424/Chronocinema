using Chronocinema.ViewModels;
using System.Collections.ObjectModel;

namespace Chronocinema.Models
{
    public class User : BaseViewModel
    {
        private int _id;
        private string _username;
        private string _email;
        private string _passwordHash;
        private DateTime _registrationDate;
        private ObservableCollection<MediaItem> _mediaItems;

        public User()
        {
            MediaItems = new ObservableCollection<MediaItem>();
            RegistrationDate = DateTime.Now;
        }

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string UserName
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string PasswordHash
        {
            get => _passwordHash;
            set => SetProperty(ref _passwordHash, value);
        }

        public DateTime RegistrationDate
        { 
            get => _registrationDate;
            set => SetProperty(ref _registrationDate, value);
        }

        public ObservableCollection<MediaItem> MediaItems
        {
            get => _mediaItems;
            set => SetProperty(ref _mediaItems, value);
        }
    }
}
