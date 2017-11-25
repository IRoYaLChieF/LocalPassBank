using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using System.Windows.Controls;
using LocalPassBank.Views;
using System.ComponentModel;
using LocalPassBank.Data;
using System.Windows;
using MaterialDesignThemes.Wpf;

namespace LocalPassBank.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private WindowViewModel windowViewModel;
        private PasswordBox passwordInput;

        private DelegateCommand goToRegistrationPage;
        public ICommand GoToRegistrationPage => (goToRegistrationPage);
        private DelegateCommand login;
        public ICommand Login => (login);

        private String nameInput = String.Empty;
        public String NameInput
        {
            get { return (nameInput); }
            set
            {
                nameInput = value;
                OnPropertyChanged("NameInput");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel(WindowViewModel windowViewModel, PasswordBox passwordInput)
        {
            this.windowViewModel = windowViewModel;
            this.passwordInput = passwordInput;
            goToRegistrationPage = new DelegateCommand(() => this.windowViewModel.GoToRegistrationPage());
            login = new DelegateCommand(LoginCommand, LoginCanExecute);
            passwordInput.PasswordChanged += PasswordChanged;
        }

        private bool LoginCanExecute()
        {
            if (nameInput == String.Empty || passwordInput.Password == String.Empty)
                return (false);
            return (true);
        }

        private async void LoginCommand()
        {
            Accounts account = windowViewModel.Database.GetAccountByName(NameInput);
            if (account == null)
            {
                await DialogHost.Show(new SimpleDialog("Nom de compte ou mot de passe incorrect !"), "LoginPageView");
                return;
            }
            Byte[] password = Security.ComputeHash512(passwordInput.Password);
            if (!password.SequenceEqual(account.Password))
            {
                await DialogHost.Show(new SimpleDialog("Nom de compte ou mot de passe incorrect !"), "LoginPageView");
                return;
            }
            await DialogHost.Show(new SimpleDialog("Connexion réussie !"), "LoginPageView");
            windowViewModel.GoToNewPassBankPage(account.Id, Security.ComputeHash256(passwordInput.Password));
        }

        private void PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            login.RaiseCanExecuteChanged();
        }

        private void OnPropertyChanged(String info)
        {
            login.RaiseCanExecuteChanged();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
