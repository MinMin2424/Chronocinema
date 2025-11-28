using Chronocinema.Models;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Chronocinema.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(String username, string email, string password0);
        Task<User> LoginAsync(string email, string password);
        void Logout();
        User CurrentUser { get; }
        bool IsLoggedIn { get; }
        event EventHandler AuthStateChanged;
    }

    public class AuthService : IAuthService
    {
        private static AuthService _instance;
        public static AuthService Instance => _instance ?? (_instance = new AuthService());
        private string UsersFilPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "users.json");
        private List<User> _users;
        private User _currentUser;
        public User CurrentUser => _currentUser;
        public bool IsLoggedIn => _currentUser != null;
        public event EventHandler AuthStateChanged;

        private AuthService()
        {
            EnsureDataDirectoryExists();
            _users = LoadUsers();
            _currentUser = null;
        }

        private void EnsureDataDirectoryExists()
        {
            var dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }
        }

        public async Task<bool> RegisterAsync(string username, string email, string password)
        {
            if (_users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }
            var user = new User
            {
                Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1,
                UserName = username,
                Email = email,
                PasswordHash = HashPassword(password),
                RegistrationDate = DateTime.Now
            };
            _users.Add(user);
            await SaveUsersAsync();
            return true;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            var user = _users.FirstOrDefault(u =>
                u.Email.Equals(email,StringComparison.OrdinalIgnoreCase) &&
                VerifyPassword(password, u.PasswordHash));
            if (user != null)
            {
                _currentUser = user;
                AuthStateChanged?.Invoke(this, EventArgs.Empty);
                return user;
            }
            return null;
        }

        public void Logout()
        {
            _currentUser = null;
            AuthStateChanged?.Invoke(this, EventArgs.Empty);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(String password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }

        private List<User> LoadUsers()
        {
            try
            {
                if (File.Exists(UsersFilPath))
                {
                    var json = File.ReadAllText(UsersFilPath);
                    return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
                }
            }
            catch (Exception)
            {

            }
            return new List<User>();
        }

        private async Task SaveUsersAsync()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(_users, options);
                await File.WriteAllTextAsync(UsersFilPath, json);
            }
            catch (Exception)
            {

            }
        }
    }
}
