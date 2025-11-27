using Chronocinema.Services;
using Chronocinema.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chronocinema.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private string _email;
        private string _password;
        private string _errorMessage;

        public LoginViewModel()
        {
            _authService = AuthService.Instance;
            LoginCommand = new RelayCommand(async () => await ExecuteLogin(), CanExecuteLogin);
            RegisterCommand = new RelayCommand(ExecuteRegister);
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

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private async Task ExecuteLogin()
        {
            ErrorMessage = string.Empty;
            if (String.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter both email and password.";
                return;
            }
            var user = await _authService.LoginAsync(Email, Password);
            if (user != null)
            {
                var userService = UserService.Instance;
                await userService.LoadUserMediaAsync(user);

                LocatorViewModel.Instance.MainViewModel.LoadUserMedia(user);
                NavigationService.Instance.NavigateTo(new MainScreen());
            }
            else
            {
                ErrorMessage = "Invalid email or password";
            }
        }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteRegister()
        {
            NavigationService.Instance.NavigateTo(new RegisterScreen());
        }
    }
}
