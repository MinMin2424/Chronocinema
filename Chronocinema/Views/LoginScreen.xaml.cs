using Chronocinema.ViewModels;
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
            DataContext = new LoginViewModel();
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
