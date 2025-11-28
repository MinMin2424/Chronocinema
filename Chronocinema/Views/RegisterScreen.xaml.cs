using Chronocinema.ViewModels;
using System.Windows.Controls;

namespace Chronocinema.Views
{
    /// <summary>
    /// Interaction logic for RegisterScreen.xaml
    /// </summary>
    public partial class RegisterScreen : UserControl
    {
        public RegisterScreen()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel();
        }

        private void PasswordBox_PasswordChanged(object sender, EventArgs e)
        {
            if (DataContext is RegisterViewModel registerViewModel)
            {
                registerViewModel.Password = ((PasswordBox)sender).Password;
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, EventArgs e)
        {
            if (DataContext is RegisterViewModel registerViewModel)
            {
                registerViewModel.ConfirmPassword = ((PasswordBox)sender).Password;
            }
        }
    }
}
