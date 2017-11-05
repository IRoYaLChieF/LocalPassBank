using LocalPassBank.ViewModels;
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

namespace LocalPassBank.Views
{
    /// <summary>
    /// Logique d'interaction pour RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private RegistrationViewModel registrationViewModel;

        public RegistrationPage(WindowViewModel windowViewModel)
        {
            InitializeComponent();
            registrationViewModel = new RegistrationViewModel(windowViewModel, PasswordPassBox, RePasswordPassBox);
            DataContext = registrationViewModel;
            // Bind Command
            ReturnButton.Command = registrationViewModel.GoToLoginPage;
            RegistrationButton.Command = registrationViewModel.Register;
        }
    }
}
