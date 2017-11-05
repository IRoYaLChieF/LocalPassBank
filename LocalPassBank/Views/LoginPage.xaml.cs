using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LocalPassBank.ViewModels;

namespace LocalPassBank.Views
{
    /// <summary>
    /// Logique d'interaction pour LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private LoginViewModel loginViewModel;

        public LoginPage(WindowViewModel windowViewModel)
        {
            InitializeComponent();
            loginViewModel = new LoginViewModel(windowViewModel, PasswordPassBox);
            DataContext = loginViewModel;
            // Bind Command
            RegisterButton.Command = loginViewModel.GoToRegistrationPage;
            LoginButton.Command = loginViewModel.Login;
        }
    }
}
