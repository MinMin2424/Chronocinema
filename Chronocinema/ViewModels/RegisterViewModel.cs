using Chronocinema.Services;
using Chronocinema.Views;
using System.Windows.Input;

namespace Chronocinema.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private string _username;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private string _errorMessage;
        private string _successMessage;

        public RegisterViewModel()
        {
            _authService = AuthService.Instance;
            RegisterCommand = new RelayCommand(async () => await ExecuteRegister(), CanExecuteRegister);
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public string SuccessMessage
        {
            get => _successMessage;
            set => SetProperty(ref _successMessage, value);
        }

        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }

        private async Task ExecuteRegister()
        {
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;

            
            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match";
                return;
            }
            if (Password.Length < 6)
            {
                ErrorMessage = "Password must be at least 6 characters long";
                return;
            }
            var success = await _authService.RegisterAsync(Username, Email, Password);
            if (success)
            {
                SuccessMessage = "Registration successful! You can now login.";
                ClearForm();
            }
            else
            {
                ErrorMessage = "Email already exists. Please use a different email.";
            }
        }

        private bool CanExecuteRegister()
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   !string.IsNullOrWhiteSpace(ConfirmPassword);
        }

        private void ExecuteLogin()
        {
            NavigationService.Instance.NavigateTo(new LoginScreen());
        }

        private void ClearForm()
        {
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }
    }
}
