using Chronocinema.Behaviors;
using Chronocinema.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Chronocinema.Views
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : UserControl
    {
        public LoginScreen()
        {
            InitializeComponent();
            Loaded += LoginScreen_Loaded;
        }

        private void LoginScreen_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new LoginViewModel();
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.ErrorMessage) && sender is LoginViewModel viewModel)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    SimpleShakeBehavior.Shake(EmailTextBox);
                    SimpleShakeBehavior.Shake(PasswordBox);
                    SimpleShakeBehavior.Shake(ErrorMessageText);
                }));
            } 
        }

        private void PasswordBox_PasswordChanged(object sender, System.EventArgs e)
        {
            if (DataContext is LoginViewModel loginViewModel)
            {
                loginViewModel.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
